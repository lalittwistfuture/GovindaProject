//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class BasePlayerTeenpatti : MonoBehaviour {

    public const int BLIND = 1;
    public const int SEEN = 2;
    public const int PACK = 3;
    public const int LEFT = 4;
    public const int WAITING = 5;
    public const int OUT_OF_COIN = 6;
    public const int TIME_OUT = 7;
    public const int Stand_Up = 8;
    // player details
    // public string name = "";
    public string player_id;
    public float coin;
    public int Betcoin;
    public string imageName;
    public string username = "";
    public int Status = BasePlayerTeenpatti.BLIND;
    public JSONNode CardValues = new JSONNode ();
    public Image StatusImage;
    // player Gameobject refrence
    public GameObject player_name;
    public GameObject player_coin;
    public GameObject player_coinHolder;
    public GameObject player_Image;
    public GameObject StartTimer;
    public GameObject PlayerStatus;
    public GameObject[] cards;
    public GameObject Rays;
    public GameObject DealSymbol;
    public Image networkImage;
    public Sprite weakSignal;
    public Sprite strongSignal;
    public ListenerTeenPatti listner;
    public Text totalCoin;

    public Sprite WaitSprite;
    public Sprite ChaalSprite;
    public Sprite BlindSprite;
    public Sprite BootSprite;
    public Sprite SeenSprite;
    public Sprite PackSprite;

    // Game Temprary Variables
    private bool isTimerOn = false;
    private float time = 0;

    //GameObject position
    public Transform Total_pos;

    public float avaivableUser = 5;
    private GameObject dealerCardPos;
    public JSONNode userStatusData;
    public bool SoundPlay = false;
    bool CardShowAnimationTime = false;
    void initializedVariables () {

        try {
            dealerCardPos = GameObject.Find ("DealerCardPos");
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }

        try {
            Total_pos = GameObject.Find ("TotalCoinPosition").transform;
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }

        try {
            // player_name.GetComponent<Text>().text = "" + name;

        } catch (System.Exception ex) {
            // Debug.Log ("Exception>>> " + ex.Message);
        }
        try {
            player_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("user");

        } catch (System.Exception ex) {
            // Debug.Log ("Exception>>> " + ex.Message);
        }
        DealSymbol.SetActive (false);
    }

    void Awake () {

        initializedVariables ();
    }

    public void Start () {
        Rays.SetActive (false);
        try {

            if (userStatusData != null) {
                //// Debug.Log ("Data is Updated " + userStatusData.ToString ());
                updateUserData (userStatusData);
            } else {
                //// Debug.Log ("Data is not find");
            }

        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
        InvokeRepeating ("startTimer", 1.0f, 1.0f);
        showFoldCard ();

    }

    void onServerSyncronization (int pl) {
        //if(pl == int.Parse(player_id)){
        //    avaivableUser = 5;

        //}
    }

    void onPlayerConnection (string playerID, bool connection) {
        if (player_id.Equals (playerID)) {
            if (connection) {
                networkImage.sprite = strongSignal;
            } else {
                networkImage.sprite = weakSignal;
            }
        }
    }

    // Update is called once per frame
    public void FixedUpdate () {
        //try
        //{
        //    int d = int.Parse(player_id);
        //    avaivableUser -= Time.fixedDeltaTime;
        // //   // Debug.Log(d +" network " + avaivableUser);
        //}
        //catch (System.Exception ex)
        //{

        //}finally{
        //    if(avaivableUser<=0){
        //        networkImage.sprite = weakSignal;
        //    }else{
        //        networkImage.sprite = strongSignal; 
        //    }
        //}

        if (isTimerOn) {
            float remain = GameControllerTeenPatti.countTimer - time;
            float percentage = remain * 100 / GameControllerTeenPatti.countTimer;
            percentage = percentage / 100;

            if (percentage * 100 >= 75) {

                // StartTimer.GetComponent<Image>().color = Color.red;
                if (player_id.Equals (GameUser.CurrentUser.ID)) {
                    SoundPlay = true;
                }
            } else {
                SoundPlay = false;
                // Color c = new Color(10.0f / 255.0f, 154.0f / 255.0f, 15.0f / 255.0f, 0.6f);
                // StartTimer.GetComponent<Image>().color = c;
            }

            StartTimer.GetComponent<Image> ().fillAmount = 1 - percentage;
            time -= Time.deltaTime;
            if (time <= 0) {
                //SoundPlay = true;
                isTimerOn = false;
                SoundPlay = false;

            }
        }

        if (Rays.activeSelf) {
            Rays.transform.Rotate (Vector3.back, 2, Space.Self);
        }

        try {
            if (player_coin)
                player_coin.GetComponent<Text> ().text = "" + Betcoin;
        } catch (System.Exception ex) {
            // Debug.Log ("UpdateCoin " + ex.Message);
        }
        if (player_name)
            player_name.GetComponent<Text> ().text = "" + this.username;
    }

    void startTimer () {
        //// Debug.Log ();
        if (SoundPlay) {
            GameDelegateTeenPatti.ShowStartClockSound ();
            //// Debug.Log ("startTimer true");
        } else {
            //// Debug.Log ("startTimer false");
        }
    }

    void OnEnable () {
        GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
        GameDelegateTeenPatti.onCollectBootAmount += onCollectBootAmount;
        GameDelegateTeenPatti.onPlayerConnection += onPlayerConnection;
        GameDelegateTeenPatti.onTotalGameUser += onTotalGameUser;
        GameDelegateTeenPatti.onServerSyncronization += onServerSyncronization;
    }

    void OnDisable () {
        GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;
        GameDelegateTeenPatti.onPlayerConnection -= onPlayerConnection;
        GameDelegateTeenPatti.onCollectBootAmount -= onCollectBootAmount;
        GameDelegateTeenPatti.onTotalGameUser -= onTotalGameUser;
        GameDelegateTeenPatti.onServerSyncronization -= onServerSyncronization;
    }

    public virtual void onTotalGameUser (int user) {

    }

    void onCollectBootAmount (int amount) {
        /*try {
			if (Status == BasePlayerTeenpatti.ACTIVE) {
				GameDelegateTeenPatti.ShowCollectBootSound ();
				SendCoin (int.Parse (amount));
			} else {
				// Debug.Log ("user not active " + player_id);
			}
		} catch (System.Exception e) {
			// Debug.Log (e.Message);
		}

		*/
    }

    void completeAnimation () {

    }

    public void updateUserData (JSONNode playerData1) {
        userStatusData = playerData1;
        try {
            this.username = playerData1[PlayerTagsTeenPatti.DISPLAY_NAME];
            player_id = playerData1[PlayerTagsTeenPatti.PLAYER_ID];
        } catch (System.Exception ex) {
            // Debug.Log ("name " + ex.Message);
        }

        try {
            string statusNew = playerData1[ServerTagsTeenpatti.PLAYER_STATUS];
            //PlayerStatus.GetComponent<Text>().text = playerData1[ServerTagsTeenpatti.LAST_ACTION];
            if (statusNew != null) {
                this.Status = int.Parse (statusNew);
            } else {

            }
        } catch (System.Exception ex) {
            // Debug.Log ("name " + ex.Message);
        }

        try {
            imageName = playerData1[PlayerTagsTeenPatti.PROFILE_PIC];
            player_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avtar/" + imageName);
            //// Debug.Log ("Image name " + imageName);
        } catch (System.Exception ex) {
            player_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avtar/1");
            // Debug.Log ("" + ex.Message);
        }

        try {
            string myID = playerData1[PlayerTagsTeenPatti.PLAYER_ID];
            if (myID.Equals (player_id)) {
                this.CardValues = playerData1[ServerTagsTeenpatti.CARD_PLAYER_ARRAY];
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }

        try {
            string coin1 = "" + float.Parse (playerData1[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
            string coin2 = playerData1[ServerTagsTeenpatti.BET_COIN];
            if ((coin1 != null) && (coin2 != null)) {
                this.coin = float.Parse (coin1);
                this.Betcoin = int.Parse (coin2);
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
        ChangePlayerStatus ();
    }

    private void CollectBootAmt (JSONNode node) {
        // collect boot amt from all active user

        string playe = node[PlayerTagsTeenPatti.PLAYER_ID];
        if (player_id.Equals (playe)) {

            if (playe.Equals (GameUser.CurrentUser.ID)) {
                float mycoins = float.Parse (GameUser.CurrentUser.Coin);
                totalCoin.text = mycoins.ToString ("F2");
            } else {
                totalCoin.text = float.Parse (node[PlayerTagsTeenPatti.TOTAL_COINS]).ToString ("F2");
            }

            // float mycoins=float.Parse(GameUser.CurrentUser.Coin);
            // float serverCoins= float.Parse(node[PlayerTagsTeenPatti.TOTAL_COINS]);
            // if(mycoins>=serverCoins){
            // totalCoin.text = ""+mycoins.ToString("F2");
            //}else{
            // totalCoin.text = ""+float.Parse(node[PlayerTagsTeenPatti.TOTAL_COINS]).ToString("F2");
            //}

            if (player_id.Equals (GameUser.CurrentUser.ID)) {
                if (float.Parse (node[ServerTagsTeenpatti.BOOT_AMOUNT]) <= this.coin) {
                    Betcoin = int.Parse (node[ServerTagsTeenpatti.BOOT_AMOUNT]);
                    SendCoin (Betcoin);
                    this.coin -= Betcoin;
                    UpdateCoin (Betcoin);
                }
            } else {
                Betcoin = int.Parse (node[ServerTagsTeenpatti.BOOT_AMOUNT]);
                SendCoin (Betcoin);
                this.coin -= Betcoin;

                UpdateCoin (Betcoin);
            }

            //DeductBootAmt (Betcoin);
        }
    }

    void onRecivedMassage (string sender, string msg) {
        try {
            JSONNode s = JSON.Parse (msg);
            switch (s[TagsTeenpatti.TAG]) {
                case ServerTagsTeenpatti.COLLECT_BOOT_AMOUNT:
                    {
                        CollectBootAmt (s);

                    }
                    break;
                case ServerTagsTeenpatti.CARDS_INFO:
                    {

                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        JSONNode node = s[ServerTagsTeenpatti.CARD_DATA];
                        dealerCardPos.SetActive (true);

                        for (int i = 0; i < node.Count; i++) {
                            JSONNode player_data = node[i];

                            string myID = player_data[PlayerTagsTeenPatti.PLAYER_ID];
                            if (myID.Equals (player_id)) {
                                this.CardValues = player_data[ServerTagsTeenpatti.CARD_PLAYER_ARRAY];
                                StartCoroutine (DistributeCard ());
                            }
                        }
                    }
                    break;

                case ServerTagsTeenpatti.ROOM_BETTING:
                    {
                        string player_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        // Debug.Log (msg);
                        StopTimer ();
                        if (player_id.Equals (player_ID)) {
                            int amount = int.Parse (s[ServerTagsTeenpatti.BET_AMOUNT]);

                            if (player_ID.Equals (GameUser.CurrentUser.ID)) {
                                GameUser.CurrentUser.Coin =  s["TOTAL_COINS"];
                                float mycoins1 = float.Parse (GameUser.CurrentUser.Coin);
                                totalCoin.text = mycoins1.ToString ("F2");
                            } else {
                                totalCoin.text = float.Parse (s[PlayerTagsTeenPatti.TOTAL_COINS]).ToString ("F2");
                            }

                            timerStart ();
                            myTrun (amount);

                        }
                    }
                    break;

                case TagsTeenpatti.HAND:
                    {
                        string player_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        // Debug.Log (s[PlayerTagsTeenPatti.CARD_PLAYER_ARRAY]);

                        CardValues = s[PlayerTagsTeenPatti.CARD_PLAYER_ARRAY];
                        // Debug.Log (CardValues);
                        if (player_id.Equals (player_ID)) {
                            try {
                                for (int i = 0; i < cards.Length; i++) {
                                    if (int.Parse (CardValues[i]) > -1) {
                                        string cardPath = CardValues[i];
                                        GameObject g = cards[i];
                                        if (g) {
                                            g.SetActive (true);
                                            g.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/" + cardPath);
                                        } else {

                                            cards[i] = transform.Find ("card_" + i).gameObject;
                                            if (cards[i]) {
                                                cards[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> (cardPath);
                                            }

                                        }
                                    }
                                }

                            } catch (System.Exception ex) {

                                // Debug.Log ("ShowAllUserCard " + ex.Message);
                            }
                        }
                    }
                    break;

                case ServerTagsTeenpatti.ROOM_INFO:
                    {

                        JSONNode playerData = s[ServerTagsTeenpatti.ROOM_DATA];
                        for (int i = 0; i < playerData.Count; i++) {
                            JSONNode playerData1 = playerData[i];
                            string myID = playerData1[PlayerTagsTeenPatti.PLAYER_ID];
                            if (myID.Equals (player_id)) {
                                Status = int.Parse (playerData1[ServerTagsTeenpatti.PLAYER_STATUS]);
                                this.totalCoin.text = float.Parse (playerData1[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
                                this.CardValues = playerData1[ServerTagsTeenpatti.CARD_PLAYER_ARRAY];

                                // float mycoins2=float.Parse(GameUser.CurrentUser.Coin);
                                //float serverCoins2= float.Parse(s[PlayerTagsTeenPatti.TOTAL_COINS]);
                                //if(mycoins2>=serverCoins2){
                                //this.totalCoin.text = ""+mycoins2.ToString("F2");
                                // }else{
                                //     this.totalCoin.text = ""+float.Parse(playerData1[ServerTagsTeenpatti.TOTAL_COINS]).ToString("F2");
                                // }

                                if (myID.Equals (GameUser.CurrentUser.ID)) {
                                    GameUser.CurrentUser.Coin =  playerData1[ServerTagsTeenpatti.TOTAL_COINS];
                                    float mycoins2 = float.Parse (GameUser.CurrentUser.Coin);
                                    totalCoin.text = mycoins2.ToString ("F2");
                                } else {
                                    totalCoin.text = float.Parse (playerData1[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
                                }

                                ChangePlayerStatus ();
                                // this.coin = int.Parse(playerData1[ServerTagsTeenpatti.TOTAL_COINS]);
                                UpdateCoin (Betcoin);
                                break;
                            }
                        }

                    }
                    break;
                case ServerTagsTeenpatti.PLAYER_RESPONDED:
                    {
                        SoundPlay = false;
                        StopTimer ();
                        try {
                            string responded_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                            bool ACCEPTANCE = s[ServerTagsTeenpatti.ACCEPTANCE].AsBool;

                            if (player_id.Equals (responded_ID)) {
                                Betcoin = int.Parse (s[ServerTagsTeenpatti.BET_COIN]);
                                // this.totalCoin.text = ""+float.Parse(s[ServerTagsTeenpatti.TOTAL_COINS]).ToString("F2");

                                // float mycoins2= float.Parse(GameUser.CurrentUser.Coin).ToString("F2");
                                // float serverCoins2= float.Parse(s[PlayerTagsTeenPatti.TOTAL_COINS]);
                                // if(mycoins2>=serverCoins2){
                                // this.totalCoin.text = ""+float.Parse(GameUser.CurrentUser.Coin).ToString("F2");
                                // }else{
                                //     this.totalCoin.text = ""+float.Parse(s[ServerTagsTeenpatti.TOTAL_COINS]).ToString("F2");
                                // }

                                if (responded_ID.Equals (GameUser.CurrentUser.ID)) {
                                    float mycoins3 = float.Parse (GameUser.CurrentUser.Coin);
                                    totalCoin.text = mycoins3.ToString ("F2");
                                } else {
                                    totalCoin.text = float.Parse (s[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
                                }

                                if (ACCEPTANCE) {
                                    SendCoin (int.Parse (s[ServerTagsTeenpatti.BET_AMOUNT]));
                                    this.coin -= int.Parse (s[ServerTagsTeenpatti.BET_AMOUNT]);
                                }
                                // this.coin = int.Parse(s[ServerTagsTeenpatti.TOTAL_COINS]);

                                UpdateCoin (Betcoin);

                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("PLAYER_RESPONDED Exception " + ex.Message);
                        }

                    }
                    break;
                case ServerTagsTeenpatti.BET_ACCEPTANCE:
                    {
                        SoundPlay = false;
                        try {
                            GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                            string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                            if (My_ID.Equals (GameUser.CurrentUser.ID)) {
                                if (s[PlayerTagsTeenPatti.SIDE_SHOW_REQUEST].AsBool) {
                                    SeeCard ();
                                }
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("PLAYER_STATUS_CHANGE Exception " + ex.Message);
                        }

                    }
                    break;

                case "TIP":
                    {
                        try {
                            string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                            if (My_ID.Equals (player_id)) {
                                GameControllerTeenPatti.TeenPatti_message = this.username + " thank you for the tip.";
                                this.SendTip ();
                                GameDelegateTeenPatti.ShowCollectBootSound ();
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("TIP " + ex.Message);
                        }
                    }
                    break;

                case ServerTagsTeenpatti.SIDE_SHOW_TRANSFER:
                    {
                        try {
                            if (player_id.Equals (GameUser.CurrentUser.ID)) {
                                string player = s[ServerTagsTeenpatti.SIDE_SHOW_TO_ID];
                                string anotherplayer = s[ServerTagsTeenpatti.SIDE_SHOW_FROM_ID];
                                if (player.Equals (GameUser.CurrentUser.ID)) {
                                    GameDelegateTeenPatti.showChaal ();
                                    timerStart ();
                                }
                                if (anotherplayer.Equals (GameUser.CurrentUser.ID)) {
                                    GameDelegateTeenPatti.showChaal ();
                                    timerStart ();
                                }
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("SIDE_SHOW_TRANSFER Exception " + ex.Message);
                        }

                    }
                    break;

                case ServerTagsTeenpatti.SIDE_SHOW_CARD_SHOW:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        string fromID = s[PlayerTagsTeenPatti.SIDE_SHOW_FROM_ID];
                        string toID = s[PlayerTagsTeenPatti.SIDE_SHOW_TO_ID];
                        if (fromID.Equals (GameUser.CurrentUser.ID) || (toID.Equals (GameUser.CurrentUser.ID))) {

                            if (fromID.Equals (player_id)) {
                                ShowUserCard ();

                                CardSizeIncrease ();
                            }
                            if (toID.Equals (player_id)) {
                                ShowUserCard ();
                                CardSizeIncrease ();
                            }
                            if (player_id.Equals (GameUser.CurrentUser.ID)) {
                                try {

                                    Status = 2;
                                    StartCoroutine (SideShowCard (GameUser.CurrentUser.ID, "2"));

                                } catch (System.Exception ex) {
                                    // Debug.Log (ex.Message);
                                }
                            } else {
                                CardShowAnimationTime = true;
                                StartCoroutine (foldUserCard ());
                            }
                        }
                    }
                    break;
                case ServerTagsTeenpatti.PLAYER_STATUS_CHANGE:
                    {
                        try {
                            GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                            string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                            string status_player = s[ServerTagsTeenpatti.PLAYER_STATUS];
                            //// Debug.Log ("status_player " + status_player);
                            if (My_ID.Equals (player_id)) {
                                Status = int.Parse (status_player);
                                ChangePlayerStatus ();
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("PLAYER_STATUS_CHANGE Exception " + ex.Message);
                        }

                    }
                    break;
                case ServerTagsTeenpatti.CARD_SEEN:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        if (player_id.Equals (s[PlayerTagsTeenPatti.PLAYER_ID])) {
                            Status = 2;
                            ChangePlayerStatus ();
                            if (player_id.Equals (GameUser.CurrentUser.ID)) {
                                SeeCard ();
                            } else {
                                // Debug.Log ("ID not Match  " + GameUser.CurrentUser.ID);
                            }
                        }
                    }
                    break;

                case ServerTagsTeenpatti.CARD_SHOW_REQUEST:
                    {
                        string CardShowRequestID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        if (player_id.Equals (CardShowRequestID)) {

                        }

                    }
                    break;

                case ServerTagsTeenpatti.LAST_ACTION:
                    {
                        string CardShowRequestID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        if (player_id.Equals (CardShowRequestID)) {
                            updatePlayerStatus (s[ServerTagsTeenpatti.VALUE]);

                        }

                    }
                    break;

                case ServerTagsTeenpatti.WINNER_PLAYER:
                    {

                        string Winner_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        string Wining_Amt = s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT];
                        if (DealSymbol.activeSelf) {
                            DealSymbol.SetActive (false);
                        }
                        SoundPlay = false;
                        //if (Status < 3)
                        //{
                        string type = s["TYPE"];
                        if (!type.Equals ("DIRECT")) {
                            ShowUserCard ();
                            CardSizeIncrease ();
                        }
                        if (player_id.Equals (Winner_ID)) {
                            StopTimer ();
                            Rays.SetActive (true);
                            try {
                                this.WinnerAnimationStrip (int.Parse (s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT]));
                            } catch (System.Exception ex) {
                                // Debug.Log ("WINNER_PLAYER " + ex.Message);
                            }
                            StartCoroutine (StopWinningAnimation ());
                            DealSymbol.SetActive (true);
                            try {
                                if (Winner_ID.Equals (GameUser.CurrentUser.ID)) {
                                    GameDelegateTeenPatti.ShowClappingSound ();
                                    this.coin += int.Parse (s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT]);
                                    this.Betcoin = 0;
                                    UpdateCoin (this.Betcoin);
                                    //GetLiveRoomInfo ();
                                }
                            } catch (System.Exception ex) {
                                // Debug.Log ("WINNER_PLAYER " + ex.Message);
                            }
                        }
                        //}
                        //else
                        //{
                        //    //// Debug.Log ("PLAYER INACTIVE");
                        //}
                    }
                    break;
                case ServerTagsTeenpatti.START_NEW_GAME:
                    {
                        hideSeeButton ();
                        ResetGame ();
                        StartCoroutine (StopWinningAnimation ());
                    }
                    break;
                case ServerTagsTeenpatti.PLAYER_LEFT_ROOM:
                    {
                        if (player_id.Equals (s[PlayerTagsTeenPatti.PLAYER_ID])) {
                            UserLeft ();
                        }
                    }
                    break;
                default:
                    break;
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    void updatePlayerStatus (string update) {
        StatusImage.gameObject.SetActive (true);
        switch (update) {
            case "BOOT":
                StatusImage.sprite = BootSprite;
                break;
            case "CHAAL":
                StatusImage.sprite = ChaalSprite;
                break;
            case "BLIND":
                StatusImage.sprite = BlindSprite;
                break;
            case "SIDE SHOW":
                StatusImage.sprite = ChaalSprite;
                break;
            case "SEEN":
                StatusImage.sprite = SeenSprite;
                break;
            case "SHOW":
                StatusImage.sprite = ChaalSprite;
                break;
            case "PACK":
                StatusImage.sprite = PackSprite;
                break;
            case "WAITING":
                StatusImage.sprite = WaitSprite;
                break;
            case "":
                StatusImage.sprite = BlindSprite;
                StatusImage.gameObject.SetActive (false);
                break;
        }
    }

    IEnumerator SideShowCard (string ID, string MyStatus) {

        yield return new WaitForSeconds (5.0f);
        ChangePlayerStatus ();

    }

    void WinnerAnimationStrip (int coin) {
        try {
            GameObject CoinStrip1 = Instantiate (player_coinHolder);
            CoinStrip1.transform.SetParent (player_coinHolder.transform.parent);
            CoinStrip1.transform.localPosition = Total_pos.position;
            CoinStrip1.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            CoinStrip1.transform.GetChild (0).gameObject.GetComponent<Text> ().text = "" + coin;
            iTween.MoveTo (CoinStrip1, player_coinHolder.transform.position, 1.5f);
            GameDelegateTeenPatti.ShowAddTotalAmount (coin);
            Destroy (CoinStrip1, 1.5f);
        } catch (System.Exception ex) {
            // Debug.Log ("SendCoin " + ex.Message);
        }
    }

    IEnumerator DistributeCard () {
        // disrtibute the user card
        if (Status == 1) {
            foreach (GameObject c in cards) {
                try {
                    GameObject currentCard = Instantiate (dealerCardPos);
                    currentCard.transform.SetParent (dealerCardPos.transform.parent);
                    currentCard.transform.localPosition = dealerCardPos.transform.localPosition;
                    currentCard.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                    Color MyColor = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
                    currentCard.GetComponent<Image> ().color = MyColor;
                    iTween.MoveTo (currentCard, c.transform.position, 0.3f);
                    GameDelegateTeenPatti.ShowoDistrubuteCardSound ();
                    c.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/cradFold_s");
                    StartCoroutine (SHowCard (c, currentCard));

                } catch (System.Exception ex) {
                    // Debug.Log ("DistributeCard Exception " + ex.Message);
                }

                yield return new WaitForSeconds (0.1f);

            }
            ShowSeeBtn ();
        }

        yield return new WaitForSeconds (1);
    }

    IEnumerator SHowCard (GameObject g, GameObject currentCard) {

        yield return new WaitForSeconds (0.3f);
        g.SetActive (true);
        Destroy (currentCard);
    }

    void CardSizeIncrease () {
        try {
            for (int i = 0; i < cards.Length; i++) {
                string cardPath = "Images_Teenpatti/cards/" + CardValues[i];
                GameObject g = cards[i];
                g.GetComponent<Image> ().sprite = Resources.Load<Sprite> (cardPath);
                if (!player_id.Equals (GameUser.CurrentUser.ID)) {
                    iTween.ScaleTo (g, new Vector3 (2.0f, 2.0f, 2.0f), 0.5f);
                }
            }

        } catch (System.Exception ex) {
            // Debug.Log ("ShowAllUserCard " + ex.Message);
        }
    }

    public void ShowUserCard () {
        // Debug.Log ("ShowUserCard working " + this.username);
        hideSeeButton ();
        try {
            for (int i = 0; i < cards.Length; i++) {
                string cardPath = "Images_Teenpatti/cards/" + CardValues[i];
                GameObject g = cards[i];
                if (g) {
                    g.GetComponent<Image> ().sprite = Resources.Load<Sprite> (cardPath);
                } else {
                    // Debug.Log ("Card is null. reinitialised card ");
                    cards[i] = transform.Find ("card_" + i).gameObject;
                    if (cards[i]) {
                        cards[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> (cardPath);
                    }

                }
            }

        } catch (System.Exception ex) {

            // Debug.Log ("ShowAllUserCard " + ex.Message);
        }
    }

    IEnumerator foldUserCard () {
        yield return new WaitForSeconds (10.0f);
        showFoldCard ();
    }

    IEnumerator StopWinningAnimation () {
        yield return new WaitForSeconds (5.0f);
        try {
            Rays.SetActive (false);

        } catch (System.Exception ex) {
            // Debug.Log ("HideColinStrip " + ex.Message);
        }
    }

    public virtual void ShowSeeBtn () {
        // to enable see btn

    }

    public virtual void AddWinnerAmount () {

    }

    public virtual void myTrun (int amount) {

    }

    public virtual void ShowAnimation (int amount) {

    }

    public virtual void SeeCard () {

    }

    public virtual void StatusMsg (string msg) {

    }

    public void clearCards () {
        for (int i = 0; i < CardValues.Count; i++) {
            CardValues[i] = "-1";
        }
    }

    void showFoldCard () {

        bool flag = true;
        try {
            for (int i = 0; i < CardValues.Count; i++) {

                if (int.Parse (CardValues[i]) == -1) {
                    flag = false;

                }
            }
        } catch (System.Exception ex) {
            // Debug.Log ("2 " + ex.Message);
        }
        if (flag) {
            foreach (GameObject cc in cards) {
                try {
                    cc.SetActive (true);
                    cc.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/cradFold_s");
                    iTween.ScaleTo (cc, new Vector3 (1.0f, 1.0f, 1.0f), 0.5f);
                } catch (System.Exception ex) {
                    // Debug.Log ("backcard not found " + ex.Message);
                }

            }
            ShowSeeBtn ();
        } else {
            foreach (GameObject cc in cards) {
                try {
                    cc.SetActive (false);
                    iTween.ScaleTo (cc, new Vector3 (1.0f, 1.0f, 1.0f), 0.5f);
                } catch (System.Exception ex) {
                    // Debug.Log ("backcard not found " + ex.Message);
                }

            }
        }

    }

    void HideCards () {
        //		if (CardShowAnimationTime) {
        //			StartCoroutine (hideCardsssss());
        //		} else {
        foreach (GameObject g in cards) {
            iTween.ScaleTo (g, new Vector3 (1.0f, 1.0f, 1.0f), 0.5f);
            g.SetActive (false);
        }
        hideSeeButton ();
        //		}

    }

    //	IEnumerator hideCardsssss(){
    //		yield return new WaitForSeconds (5.0f);
    //		CardShowAnimationTime = false;
    //		HideCards ();
    //	}

    public virtual void hideSeeButton () { }

    public virtual void UserInactive (string msg) {

    }

    public virtual void UserActive (string msg) {

    }

    public virtual void UserLeft () {

    }

    public void ChangePlayerStatus () {
        HidejoinBtn ();
        //// Debug.Log ("ChangePlayerStatus " + this.Status);

        switch (this.Status) {
            case 1:
                {
                    StatusMsg ("");
                    //if (PlayerStatus)
                    showFoldCard ();
                    UserActive ("Blind");

                    // StatusImage.sprite = Resources.Load<Sprite>("Images_Teenpatti/blind");
                    ShowSeeBtn ();
                }
                break;
            case 2:
                {

                    StatusMsg ("");
                    //if (PlayerStatus)
                    UserActive ("Seen");

                    // StatusImage.sprite = Resources.Load<Sprite>("Images_Teenpatti/Seen");
                    if (GameUser.CurrentUser.ID.Equals (player_id)) {
                        ShowUserCard ();
                    } else {
                        showFoldCard ();
                    }
                    hideSeeButton ();
                }
                break;
            case 4:
                {

                    StatusMsg ("You Are Currently Packed");
                    UserInactive ("Pack");

                    StatusImage.sprite = PackSprite;
                    HideCards ();

                    hideSeeButton ();
                }
                break;
            case 5:
                {
                    StatusMsg ("You Are Currently Left  From Room");
                    UserInactive ("Left");

                    // StatusImage.sprite = Resources.Load<Sprite>("Images_Teenpatti/Seen");
                    UserLeft ();

                    hideSeeButton ();
                }
                break;
            case 6:
                {
                    UserInactive ("Waiting");

                    StatusImage.sprite = WaitSprite;
                    HideCards ();
                    hideSeeButton ();
                }
                break;

            case 3:
                {
                    StatusMsg ("You are packed.");
                    UserInactive ("Pack");

                    StatusImage.sprite = PackSprite;
                    //HideCards();
                    // hideSeeButton();
                }
                break;

            case 7:
                {

                    StatusMsg ("Your Time Out");
                    UserInactive ("Pack");

                    StatusImage.sprite = PackSprite;
                    HideCards ();
                    hideSeeButton ();
                }
                break;

            case 8:
                {

                    StatusMsg ("Your are watching the  game");
                    UserInactive ("Watching");
                    //  StatusImage.sprite = Resources.Load<Sprite>("Images_Teenpatti/wait");
                    HideCards ();
                    ShowjoinBtn ();
                    hideSeeButton ();
                }
                break;

            default:
                break;
        }

    }

    public virtual void ShowjoinBtn () {

    }

    public virtual void HidejoinBtn () {

    }

    public virtual void GetLiveRoomInfo () {

    }

    public virtual void ResetGame () {

    }

    public void SendCoin (int coin) {
        try {
            GameDelegateTeenPatti.ShowCollectBootSound ();
            GameObject CoinStrip1 = Instantiate (player_coinHolder);
            CoinStrip1.transform.SetParent (player_coinHolder.transform.parent);
            CoinStrip1.transform.localPosition = player_coinHolder.transform.localPosition;
            CoinStrip1.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            CoinStrip1.transform.GetChild (0).gameObject.GetComponent<Text> ().text = "" + coin;
            iTween.MoveTo (CoinStrip1, Total_pos.position, 0.15f);
            GameDelegateTeenPatti.ShowAddTotalAmount (coin);
            Destroy (CoinStrip1, 0.2f);
            //UpdateCoin (coin);
        } catch (System.Exception ex) {
            // Debug.Log ("SendCoin " + ex.Message);
        }
        //StopTimer ();
    }

    public void SendTip () {
        try {
            GameObject CoinStrip1 = Instantiate (player_coinHolder);
            CoinStrip1.transform.SetParent (player_coinHolder.transform.parent);
            CoinStrip1.transform.localPosition = player_Image.transform.localPosition;
            CoinStrip1.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            CoinStrip1.transform.GetChild (0).gameObject.GetComponent<Text> ().text = "10";
            iTween.MoveTo (CoinStrip1, dealerCardPos.transform.position, 0.15f);
            //GameDelegateTeenPatti.ShowAddTotalAmount (coin);
            Destroy (CoinStrip1, 0.2f);
            //UpdateCoin (coin);
        } catch (System.Exception ex) {
            // Debug.Log ("SendCoin " + ex.Message);
        }

    }

    public virtual void UpdateCoin (float TotalBetAmt) {

    }

    public virtual void DeductBootAmt (float TotalBetAmt) {

    }

    public virtual void UpdateRoomInfo (float TotalCoin) {

    }

    public void timerStart () {
        try {
            StartTimer.GetComponent<Image> ().fillAmount = 1;
            isTimerOn = true;
            time = GameControllerTeenPatti.countTimer;
        } catch (System.Exception ex) {
            // Debug.Log ("timerStart Exception " + ex.Message);
        }

    }

    public void StopTimer () {
        try {
            isTimerOn = false;
            StartTimer.GetComponent<Image> ().fillAmount = 0.0f;
            time = 0.0f;
        } catch (System.Exception ex) {
            // Debug.Log ("StopTimer Exception " + ex.Message);
        }
    }
    /*
	void PackUser ()
	{
		Color c = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 0.5f);
		player_Image.GetComponent<Image> ().color = c;
		player_coin.GetComponent<Text> ().text = "";
		foreach (GameObject g in cards) {
			g.SetActive (false);
		}
	}
*/

}