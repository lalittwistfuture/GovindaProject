using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Roullet {
    public class RoulleteManager : MonoBehaviour {
        private static RoulleteManager _instance;
        public Text betAmount;
        public Text winAmount;
        public Text TotalBetCounter;
        public AudioClip ChipSound;
        public AudioClip WinSound;
        public AudioClip ClockSound;
        private List<Bet> BetList;
        private List<Bet> TempBetList;
        private List<Bet> BetListHistory;
        private List<int> CurrentBettingNumber;
        private int winingNumber = -1;
        private float totalWin = 0;
        private float ActualWin = 0;
        private float totalLoss = 0;
        private Text Timer;
        private Text Player_coin;
        private Text Invitecoins;
        private AudioSource Player;
        public bool isTable = true;
        private string GameID = "";
        private GameObject[] LastCell;
        private GameObject LastNumber;
        public GameObject RouletteWheelLocation;
        public GameObject RouletteTableLocation;
        private GameObject HistoryPanel;
        private GameObject chipSample;
        private GameObject BetPanel;
        int[] blackNumberSet = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        int[] redNumberSet = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        public JSONNode Numbers;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake () {
            _instance = this;
            Application.targetFrameRate = 30;
            // Debug.Log ("Start Manager");
            chipSample = GameObject.Find ("CoinSample");
            BetPanel = GameObject.Find ("BetPanel");
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            HistoryPanel = GameObject.Find ("PreviousBetPanel");
            LastNumber = GameObject.Find ("LastCell");
            LastCell = new GameObject[6];
            BetList = new List<Bet> ();
            TempBetList = new List<Bet> ();
            BetListHistory = new List<Bet> ();
            CurrentBettingNumber = new List<int> ();
            Timer = GameObject.Find ("Timer").GetComponent<Text> ();
            Player = transform.GetComponent<AudioSource> ();
            Player_coin = GameObject.Find ("Player_Coin").GetComponent<Text> ();
            Player_coin.text = "Coin:" + float.Parse (GameUser.CurrentUser.Coin).ToString ("F2");
            Invitecoins = GameObject.Find ("minBet").transform.GetChild (0).GetComponent<Text> ();
            Invitecoins.text = "" + GameUser.CurrentUser.Name;
        }
        public static RoulleteManager Instance {
            get { return _instance; }
        }
        void Start () {

            //ManageHistoryPanels (this.Numbers);
        }

        public List<Bet> getBetList () {
            return BetList;
        }

        public void showHistoryPanel () {
            if (HistoryPanel.transform.GetSiblingIndex () == 0) {
                HistoryPanel.transform.SetSiblingIndex (HistoryPanel.transform.parent.childCount - 1);
                HistoryPanel.GetComponent<Roulette_HistoryScript> ().ManageHistoryPanels (Numbers);
            } else {
                HistoryPanel.transform.SetSiblingIndex (0);
            }
        }

        void OnEnable () {
            for (int i = 0; i < LastCell.Length; i++) {
                LastCell[i] = GameObject.Find ("Lastcell (" + i + ")");
            }
            ManageHistoryPanels (Numbers);

            RouletteDelegate.onNumberSelected += onNumberSelected;
            RouletteDelegate.onOptionSelected += onOptionSelected;
            RouletteDelegate.onClearBet += onClearBet;
            RouletteDelegate.onMoveToTable += onMoveToTable;
            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
        }

        void OnDisable () {
            RouletteDelegate.onNumberSelected -= onNumberSelected;
            RouletteDelegate.onOptionSelected -= onOptionSelected;
            RouletteDelegate.onClearBet -= onClearBet;
            RouletteDelegate.onMoveToTable -= onMoveToTable;
            RouletteDelegate.onWarpChatRecived -= onWarpChatRecived;
        }

        void onWarpChatRecived (string sender, string message) {
            JSONNode s = JSON.Parse (message);

            switch (s[RouletteTag.TAG]) {

                case RouletteTag.MOVE_TO_TABLE:
                    {

                        onMoveToTable ();
                        Numbers = JSON.Parse (s["PREVIOUS_NUMBER"]);
                        GameID = s["GAME_ID"];
                        ManageHistoryPanels (Numbers);
                    }
                    break;
                case RouletteTag.START_WHEEL:
                    {
                        this.winingNumber = int.Parse (s[RouletteTag.VALUE]);
                        GameID = s["GAME_ID"];
                    }
                    break;
                case RouletteTag.NOT_ENOUGH_COIN:
                    {
                        string playerName = s[RouletteTag.PLAYER];
                        if (playerName.Equals (GameUser.CurrentUser.ID)) {
                            GameController.showToast ("Alert! you don't have sufficient coins");
                        }
                    }
                    break;
                case RouletteTag.START_DEAL:
                    {
                        this.winingNumber = -1;
                    }
                    break;
                case RouletteTag.BET_ADDED:
                    {
                        string betID = s["BET_ID"];
                        addBetOnTable (betID);

                    }
                    break;
                case RouletteTag.REMOVE_DONE:
                    {
                        string playerName = s[RouletteTag.PLAYER];
                        if (playerName.Equals (GameUser.CurrentUser.ID)) {
                            // Debug.Log ("Clear Table ");
                            BetListHistory.Clear ();
                            foreach (Bet b in BetList) {
                                // Debug.Log ("user coins" + b.coin);

                                GameUser.CurrentUser.Coin = "" + (float.Parse (GameUser.CurrentUser.Coin) + b.coin);
                                PlayerPrefs.SetFloat (RouletteTag.TOTAL_BET, PlayerPrefs.GetInt (RouletteTag.TOTAL_BET) - b.coin);
                                Player_coin.text = "Coin:" + GameUser.CurrentUser.Coin;
                                BetListHistory.Add (b);
                            }
                            this.BetList.Clear ();
                            RouletteDelegate.removeAllBet ();
                            refreshTable ();
                        }

                    }
                    break;

                case RouletteTag.UNDO_DONE:
                    {
                        string playerName = s[RouletteTag.PLAYER];
                        if (playerName.Equals (GameUser.CurrentUser.ID)) {
                            string betID = s["BET_ID"];
                            Bet b = getBetbyID (betID);
                            if (b != null) {
                                Destroy (b.coinSample);
                                GameUser.CurrentUser.Coin = "" + (float.Parse (GameUser.CurrentUser.Coin) + b.coin);
                                PlayerPrefs.SetFloat (RouletteTag.TOTAL_BET, PlayerPrefs.GetFloat (RouletteTag.TOTAL_BET) - b.coin);
                                Player_coin.text = "Coin:" + GameUser.CurrentUser.Coin;
                                foreach (int n in b.number) {
                                    if (n > 0) {
                                        GameObject btn = GameObject.Find ("" + n);
                                        Color c = btn.transform.GetChild (0).GetComponent<Image> ().color;
                                        c.a = 0.0f;
                                        btn.transform.GetChild (0).GetComponent<Image> ().color = c;
                                    }
                                }
                                BetList.Remove (b);

                            }
                            refreshTable ();

                        }
                    }
                    break;

                case RouletteTag.MOVE_TO_WHEEL:
                    {
                        moveToWheel ();
                        try {
                            HistoryPanel.transform.SetSiblingIndex (0);
                            GameID = s["GAME_ID"];
                        } catch (System.Exception ex) {

                        }
                        isTable = false;
                    }
                    break;

                case RouletteTag.BETTING_STOP:
                    {
                        // sendBetttingToServer ();
                        GameID = s["GAME_ID"];
                    }
                    break;
                case RouletteTag.WIN_COIN:
                    {
                        // Debug.Log (message);
                        string playerName = s[RouletteTag.PLAYER];
                        if (playerName.Equals (GameUser.CurrentUser.ID)) {
                            if (PlayerPrefs.GetInt ("Sound") == 1) {
                                Player.PlayOneShot (WinSound);
                            }
                            winAmount.text = "Win: " + s[RouletteTag.VALUE];
                            RouletteDelegate.winnerFound ();

                        } 
                        ServerRequest ();
                        // Debug.Log ("win found");

                    }
                    break;
                case RouletteTag.LOSS_COIN:
                    {
                        ServerRequest ();
                        // Debug.Log ("loss found");

                    }
                    break;
                case RouletteTag.BETTING_START:
                    {
                        GameID = s["GAME_ID"];
                        winAmount.text = "Win: 0";
                        clearTableAfterWin ();
                    }
                    break;

                case RouletteTag.TIME:
                    {
                        try {
                            Timer.text = int.Parse (s[RouletteTag.VALUE]) + ":00";
                            if (PlayerPrefs.GetInt ("Sound") == 1) {
                                Player.PlayOneShot (ClockSound);
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log (ex.Message);
                        }
                    }
                    break;

            }
        }

        Bet getBetbyID (string betId) {
            foreach (Bet b in BetList) {
                if (b.betID.Equals (betId)) {
                    return b;
                }
            }
            return null;
        }

        public void ManageHistoryPanels (JSONNode Number) {
            setLastPanelNumber (Number);
            try {
                int number = int.Parse ("" + Number[Number.Count - 1]);
                LastNumber.transform.GetChild (0).GetComponent<Text> ().text = "" + number;
                LastNumber.GetComponent<Image> ().color = getColor (number);
            } catch (System.Exception ex) {
                // Debug.Log (ex.Message);
            }
        }

        void setLastPanelNumber (JSONNode Number) {
            for (int i = 0; i < LastCell.Length; i++) {
                try {
                    if (Number.Count - (i + 2) >= 0) {
                        if (LastCell[i] != null) {
                            int number = int.Parse ("" + Number[Number.Count - (i + 2)]);
                            if (LastCell[i].transform.childCount > 0) {
                                LastCell[i].transform.GetChild (0).GetComponent<Text> ().text = "" + number;
                            }
                            LastCell[i].GetComponent<Image> ().color = getColor (number);
                        }
                    }
                } catch (System.Exception ex) {
                    // Debug.Log (ex.Message);
                }
            }

        }

        Color getColor (int number) {

            for (int i = 0; i < blackNumberSet.Length; i++) {
                if (blackNumberSet[i] == number) {
                    return Color.black;
                }
            }
            for (int i = 0; i < redNumberSet.Length; i++) {
                if (redNumberSet[i] == number) {
                    return Color.red;
                }
            }
            return new Color (0.02f, 0.5f, 0.14f, 1.0f);
        }

        void onMoveToTable () {
            iTween.MoveTo (transform.gameObject, RouletteTableLocation.transform.position, 2.0f);
            if (!isTable) {
                try {
                    isTable = true;
                    // Debug.Log ("Move to table");
                    iTween.MoveTo (transform.gameObject, GameObject.Find ("RouletteTableLocation").transform.position, 2.0f);
                    //Invoke ("winningAnimation", 0.5f);

                } catch (System.Exception ex) {
                    // Debug.Log (ex.Message);
                }
            }

        }

        void getResponse (string response) {
            try {
                JSONNode node = JSON.Parse (response);

                if (node != null) {
                    string result = node["status"];

                    if (result.Equals ("OK")) {

                        try {
                            JSONNode data1 = node["data"];
                            JSONNode data = data1[0];
                            string coin = data["balance"];

                            GameUser.CurrentUser.Coin = coin;
                            float coinString = float.Parse (GameUser.CurrentUser.Coin);
                            Player_coin.text = "Coin:" + coinString.ToString ("F2");
                            GameController.coin = float.Parse (coin);

                        } catch {
                            // Debug.Log ("Message");
                        }
                    }

                }
            } catch (System.Exception ex) {
                // Debug.Log (ex.Message);
            }
        }
        private void ServerRequest () {
            WWWForm form = new WWWForm ();
            form.AddField ("TAG", "GET_COIN");
            form.AddField ("user_id", GameUser.CurrentUser.ID);
            WebManager.Instance.StartRequest (form, getResponse, "ROULETTE COIN");

        }

        void clearTableAfterWin () {
            BetListHistory.Clear ();
            foreach (Bet b in BetList) {
                BetListHistory.Add (b);
              //  // Debug.Log ("Add coin to hstory " + b.coin);
            }
            this.BetList.Clear ();
            RouletteDelegate.removeAllBet ();
        }
        public void clearTable () {
            Roulette_AppWarpClass.removeBet ();
        }

        void setCoinImage (GameObject btn, int number) {
            if (number >= float.Parse (GameUser.CurrentUser.Coin)) {
                switch (number) {
                    case 5000:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c7");
                        }
                        break;
                    case 2000:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c6");
                        }
                        break;
                    case 1000:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c5");
                        }
                        break;
                    case 500:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c4");
                        }
                        break;
                    case 100:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c3");
                        }
                        break;
                    case 50:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c2");
                        }
                        break;
                    case 20:
                        {
                            PlayerPrefs.SetString (RouletteTag.RouletteCoinImage, "c1");
                        }
                        break;
                        /*case 10:
                            {
                                PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettec2");
                            }
                            break;
                        case 5:
                            {
                                PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettec1");
                            }
                            break;
                        case 1:
                            {
                                PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettechip");
                            }
                            break;*/
                }
            } else {
                // GameController.showToast("Alert! you don't have sufficient coins");
                // Debug.Log ("Alert! you don't have sufficient coins");
            }

        }
        void checkNumberSelection () {

        }

        public void unDoBet () {
            Roulette_AppWarpClass.unDoBet ();
        }

        void onClearBet () {

            betAmount.text = "Play:0";

            foreach (Bet b in BetList) {
                PlayerPrefs.SetString (PlayerDetails.Coin, "" + (float.Parse (GameUser.CurrentUser.Coin) + b.coin));
                Player_coin.text = "Coin:" + GameUser.CurrentUser.Coin;
            }
            PlayerPrefs.SetFloat (RouletteTag.TOTAL_BET, 0);
            this.BetList.Clear ();

        }

        bool isBetAllow (List<int> number) {
            if (number.Count > 6) {
                // Limit 50000
                return checkLimit (number, 50000);
            } else {
                // Limit 5000
                return checkLimit (number, 5000);
            }

        }

        bool checkLimit (List<int> number, int limit) {
            float totalBet = 0;
            foreach (Bet b in this.BetList) {

                if (b.number.Count == number.Count) {
                    bool flag = true;
                    for (int j = 0; j < b.number.Count; j++) {
                        if (b.number[j] != number[j]) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        totalBet += b.coin;
                    }
                }

            }
            if ((totalBet + PlayerPrefs.GetFloat (RouletteTag.RouletteSelectedCoin)) > limit) {
                return false;
            } else {
                return true;
            }

        }

        float getsamebetAmount (Bet b) {
            float coin = 0;
            List<int> num = b.number;
            foreach (Bet b1 in this.BetList) {
                List<int> num1 = b1.number;
                bool flag = true;
                if (num1.Count == num.Count) {
                    for (int i = 0; i < num.Count; i++) {
                        if (num[i] != num1[i]) {
                            flag = false;
                        }
                    }
                } else {
                    flag = false;
                }
                if (flag) {
                    coin += b1.coin;
                }
            }
            return coin;
        }

        void addBetOnTable (string betID) {
            foreach (Bet b in this.TempBetList) {
                if (betID.Equals (b.betID)) {
                    b.coinSample.transform.SetParent (BetPanel.transform);
                    b.coinSample.transform.position = b.coinPosition;
                    List<int> num1 = b.number;
                    if (num1.Count > 1 || num1[0] == 0) {
                        b.coinSample.transform.localScale = new Vector3 (0.8f, 0.8f, 1.0f);
                        // Debug.Log ("small size");
                    } else {
                        b.coinSample.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                        // Debug.Log ("actual size");
                    }
                    //b.coinSample.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                    b.coinSample.GetComponent<Image> ().raycastTarget = false;
                    b.coinSample.transform.GetComponent<Image> ().sprite = Resources.Load<Sprite> (b.coinImageName);
                    float coin = getsamebetAmount (b) + b.coin;
                    b.coinSample.transform.GetChild (0).GetComponent<Text> ().text = "" + coin;
                    //b.coinSample.GetComponent<coinsample> ().setCoinValue("" + coin+"+");
                    GameUser.CurrentUser.Coin = "" + (float.Parse (GameUser.CurrentUser.Coin) - b.coin);
                    Player_coin.text = "Coin:" + GameUser.CurrentUser.Coin;
                    foreach (int n in b.number) {
                        if (n > 0) {
                            GameObject btn = GameObject.Find ("" + n);
                            Color c = btn.transform.GetChild (0).GetComponent<Image> ().color;
                            c.a = 1.0f;
                            btn.transform.GetChild (0).GetComponent<Image> ().color = c;

                        }
                    }
                    if (!isBetExist (betID)) {
                        this.BetList.Add (b);
                    }

                }
            }
            refreshTable ();
        }

        bool isBetExist (string betID) {
            foreach (Bet b in this.BetList) {
                if (betID.Equals (b.betID)) {
                    return true;
                }
            }
            return false;
        }
        void refreshTable () {
            foreach (Bet b in this.BetList) {
                foreach (int n in b.number) {
                    if (n > 0) {
                        GameObject btn = GameObject.Find ("" + n);
                        Color c = btn.transform.GetChild (0).GetComponent<Image> ().color;
                        c.a = 1.0f;
                        btn.transform.GetChild (0).GetComponent<Image> ().color = c;
                    }
                }

            }
            getBetAmount ();
        }

        void onNumberSelected (List<int> number, GameObject obj, string imageName) {
            winAmount.text = "Win: 0";
            if (PlayerPrefs.GetInt ("Sound") == 1) {
                Player.PlayOneShot (ChipSound);
                Player.Play ();
            }
            if (isBetAllow (number)) {
                Bet b = new Bet ();
                b.number = new List<int> (number);
                b.coinSample = obj;
                b.coinImageName = imageName;
                b.betID = GameUser.CurrentUser.ID + "" + Time.time * 1000;
                b.coinPosition = obj.transform.position;
                b.coin = PlayerPrefs.GetFloat (RouletteTag.RouletteSelectedCoin);
                TempBetList.Add (b);
                sendBetttingToServer (b);
            } else {
                Destroy (obj);
            }
        }

        void getBetAmount () {
            float bet = 0;
            foreach (Bet b in this.BetList) {
                bet += b.coin;
            }
            PlayerPrefs.SetFloat (RouletteTag.TOTAL_BET, bet);
            betAmount.text = "Play: " + bet;

        }

        void sendBetttingToServer (Bet b) {
            try {
                JSONClass number = new JSONClass ();
                int time = (36 / b.number.Count) - 1;
                number.Add ("TIMES", "" + time);
                number.Add ("TAG", "ADD_COIN");
                number.Add ("COIN", "" + b.coin);
                number.Add ("BET_ID", "" + b.betID);
                number.Add ("x", "" + b.coinPosition.x);
                number.Add ("y", "" + b.coinPosition.y);
                number.Add ("z", "" + b.coinPosition.z);
                number.Add ("IMAGE", "" + b.coinImageName);
                JSONArray num = new JSONArray ();
                foreach (int n in b.number) {
                    num.Add ("" + n);
                }
                number.Add ("NUMBER", num.ToString ());
                Roulette_AppWarpClass.add_coin (number);
            } catch (System.Exception ex) {
                // Debug.Log (ex.Message);
            }
        }

        public void moveToWheel () {

            //iTween.MoveTo (transform.gameObject, RouletteWheelLocation.transform.position, 2.0f);
            try {
                iTween.MoveTo (transform.gameObject, GameObject.Find ("RouletteWheelLocation").transform.position, 2.0f);
            } catch (System.Exception ex) {
                // Debug.Log (ex.Message);
            }
        }

        void onOptionSelected (string tag, GameObject obj, string imageName) {
            CurrentBettingNumber.Clear ();
            switch (tag) {
                case "19-36":
                    {

                        for (int i = 19; i <= 36; i++) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "1-18":
                    {
                        for (int i = 1; i <= 18; i++) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "0":
                    {
                        CurrentBettingNumber.Add (0);
                    }
                    break;
                case "1-12":
                    {
                        for (int i = 1; i <= 12; i++) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "13-24":
                    {
                        for (int i = 13; i <= 24; i++) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "25-36":
                    {
                        for (int i = 25; i <= 36; i++) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "Even":
                    {
                        for (int i = 2; i <= 36; i = i + 2) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "Odd":
                    {
                        for (int i = 1; i <= 36; i = i + 2) {
                            CurrentBettingNumber.Add (i);
                        }
                    }
                    break;
                case "Black":
                    {
                        int[] number = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
                        for (int i = 0; i < number.Length; i++) {
                            CurrentBettingNumber.Add (number[i]);
                        }
                    }
                    break;
                case "Red":
                    {
                        int[] number = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
                        for (int i = 0; i < number.Length; i++) {
                            CurrentBettingNumber.Add (number[i]);
                        }
                    }
                    break;
                case "3rd":
                    {
                        int[] number = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
                        for (int i = 0; i < number.Length; i++) {
                            CurrentBettingNumber.Add (number[i]);
                        }
                    }
                    break;
                case "2nd":
                    {
                        int[] number = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
                        for (int i = 0; i < number.Length; i++) {
                            CurrentBettingNumber.Add (number[i]);
                        }
                    }
                    break;
                case "1st":
                    {
                        // Debug.Log ("first button click");
                        int[] number = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
                        for (int i = 0; i < number.Length; i++) {
                            CurrentBettingNumber.Add (number[i]);
                        }
                    }
                    break;
            }
            onNumberSelected (CurrentBettingNumber, obj, imageName);

        }
        void Update () {
            TotalBetCounter.text = "" + BetList.Count;
        }

    }

    public class Bet {
        public List<int> number;
        public float coin;
        public GameObject coinSample;
        public Vector3 coinPosition;
        public string betID;
        public string coinImageName;
    }
}