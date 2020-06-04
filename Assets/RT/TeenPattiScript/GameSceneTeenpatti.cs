using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneTeenpatti : MonoBehaviour {
    public GameObject MessageText;
    public GameObject SideShowPopUp;
    public GameObject[] OpponentPlayers;
    public Text RoomDetail;
    public GameObject CreateTablePanel;
    public GameObject JoinTablePanel;
    public GameObject QuitGamePanel;
    
    public GameObject GameMenu;
    public GameObject WaitingPanel;
    public GameObject RoomStrip;
    public GameObject leftPanel;
    public Text gameTypeText;
   
    public GameObject infoPanel;
    public BasePlayerTeenpatti currentPlayer;
    public GameObject historyPanel;
    public Text profitText;
    // Use this for initialization

    Vector3 initPosProfitText;

    public void showAddCoinPanel () {

    }

    public void showHistory () {
        historyPanel.SetActive (true);
    }

    void Start () {
        Screen.orientation = ScreenOrientation.Portrait;

        infoPanel.SetActive (false);
        leftPanel.SetActive (false);
        historyPanel.SetActive (false);
        GameMenuTeenPattti.MoveToMainLobby = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        initPosProfitText = profitText.transform.position;
        try {
            SideShowPopUp.SetActive (false);
            CreateTablePanel.SetActive (false);
            JoinTablePanel.SetActive (false);
            QuitGamePanel.SetActive (false);
           
            GameMenu.SetActive (false);
            WaitingPanel.SetActive (false);
            //gameTypePanel.SetActive(false);
        } catch (System.Exception ex) {
            // Debug.Log ("Start Excption " + ex.Message);
        }
        InstantiateGameobject ();
        // finding private table type
        if (!GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PRIVATE)) {
            if (RoomStrip)
                RoomStrip.SetActive (false);
        }
        

    }

    // Update is called once per frame
    void Update () {

    }
    public void Info () {
        infoPanel.SetActive (true);
    }
    void FixedUpdate () {
        if (GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PRIVATE)) {
            if (RoomDetail)
                RoomDetail.text = "ROOM ID : " + PlayerPrefs.GetString (TagsTeenpatti.ROOM_ID);
        }

        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (GameMenu.activeSelf) {
                GameMenu.SetActive (false);
               
            } else {
                QuitGamePanel.SetActive (true);
            }

        }

        try {
            MessageText.GetComponent<Text> ().text = GameControllerTeenPatti.TeenPatti_message;
            //GameController.Message = GameControllerTeenPatti.TeenPatti_message;
        } catch (System.Exception ex) {
            // Debug.Log ("MessageText finding Exception " + ex.Message);
        }
    }

    public void CloseButtonAction () {
        GameMenu.SetActive (false);
       
    }

    void InstantiateGameobject () {
        // finding opponent 
        try {

            //	OpponentPlayers = GameObject.FindGameObjectsWithTag ("opponent");
            //	GameControllerTeenPatti.showToast ("Total Opponent " + OpponentPlayers.Length);
            foreach (GameObject g in OpponentPlayers) {
                g.SetActive (false);
            }
        } catch (System.Exception ex) {
            // Debug.Log ("Exception Finding opponent " + ex.Message);
        }
        // game type
        try {
            //// Debug.Log (" InstantiateGameobject PrivateGameType " + GameControllerTeenPatti.PrivateGameType);
            //// Debug.Log (" InstantiateGameobject GameType " + GameControllerTeenPatti.GameType);

            if (GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PRIVATE)) {
                if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.CREATE_PRIVATE_TABLE)) {
                    CreateTablePanel.SetActive (true);
                } else if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.JOIN_PRIVATE_TABLE)) {
                    // Debug.Log ("call join");
                    WaitingPanel.SetActive (false);
                    JoinTablePanel.SetActive (true);

                }
            }
        } catch (System.Exception ex) {
            // Debug.Log ("privateType finding " + ex.Message);
        }

    }

    void OnEnable () {

        GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
        //GameDelegateTeenPatti.onPrivateTableCode += onPrivateTableCode;
    }

    void OnDisable () {
        GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;
        //GameDelegateTeenPatti.onPrivateTableCode -= onPrivateTableCode;
    }

    bool isOpponentExist (string player_id) {
        try {
            foreach (GameObject g in this.OpponentPlayers) {
                if (g.activeSelf) {
                    if (g.GetComponent<OpponentPlayerTeenpatti> ().player_id.Equals (player_id)) {
                        // Debug.Log ("Opponent is exist " + player_id);
                        return true;
                    }
                }
            }
        } catch (System.Exception ex) {
            // Debug.Log ("isOpponentExist Exception " + ex.Message);
        }
        return false;
    }

    void AddOpponent (JSONNode node) {
        // Debug.Log ("AddOpponent working " + node[PlayerTagsTeenPatti.DISPLAY_NAME]);
        string TABLE_POS = node[PlayerTagsTeenPatti.TABLE_POS];
        string TOTAL_COINS = node[PlayerTagsTeenPatti.TOTAL_COINS];
        string ON_SIDE_SHOW = node[PlayerTagsTeenPatti.ON_SIDE_SHOW];
        string DISPLAY_NAME = node[PlayerTagsTeenPatti.DISPLAY_NAME];
        string PLAYER_ID = node[PlayerTagsTeenPatti.PLAYER_ID];
        string PLAYER_STATUS = node[PlayerTagsTeenPatti.PLAYER_STATUS];

        // Debug.Log ("table pos = " + TABLE_POS);
        int pos = int.Parse (TABLE_POS);
        int dif = PlayerPrefs.GetInt ("MY_POSITION") - pos;
        int opponent_pos = 0;
        if (dif > 0) {
            opponent_pos = 6 - dif;
        } else {
            opponent_pos = -1 * dif;
        }
        string Mypos = "Opponent_" + opponent_pos;
        // Debug.Log ("Seat Name " + Mypos);
        foreach (GameObject g in this.OpponentPlayers) {
            if (g.name.Equals (Mypos)) {
                try {

                    g.SetActive (true);
                    g.GetComponent<BasePlayerTeenpatti> ().updateUserData (node);
                } catch (System.Exception ex) {
                    // Debug.Log ("AddOpponent Exception " + ex.Message);
                }

                break;
            } else {
                // Debug.Log ("opponent seat does not found ");
            }

        }
    }

    void onRecivedMassage (string sender, string msg) {
        try {
            JSONNode s = JSON.Parse (msg);
            //// Debug.Log ("onRecivedMassage " + s.ToString ());
            switch (s[TagsTeenpatti.TAG]) {
                case ServerTagsTeenpatti.ROOM_INFO:
                    {
                        //// Debug.Log ("Room Info  in game scene"); 
                        WaitingPanel.SetActive (false);

                        // Debug.Log (s);

                        JSONNode roomData = s[ServerTagsTeenpatti.ROOM_DATA];
                        try {
                            for (int i = 0; i < roomData.Count; i++) {
                                JSONNode node = roomData[i];
                                string PLAYER_ID = node[PlayerTagsTeenPatti.PLAYER_ID];
                                if (PLAYER_ID.Equals (GameUser.CurrentUser.ID)) {
                                    currentPlayer.totalCoin.text = "" + float.Parse (node[PlayerTagsTeenPatti.TOTAL_COINS]).ToString ("F2");
                                    PlayerPrefs.SetInt ("MY_POSITION", int.Parse (node[PlayerTagsTeenPatti.TABLE_POS]));
                                    break;
                                }
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("ROOM_INFO Exception " + ex.Message);

                        }
                        try {
                            for (int i = 0; i < roomData.Count; i++) {
                                JSONNode node = roomData[i];
                                string PLAYER_ID = node[PlayerTagsTeenPatti.PLAYER_ID];
                                if (!PLAYER_ID.Equals (GameUser.CurrentUser.ID)) {

                                    if (!isOpponentExist (PLAYER_ID)) {

                                        AddOpponent (node);
                                    }

                                }
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("ROOM_INFO Exception " + ex.Message);

                        }

                    }
                    break;

                case ServerTagsTeenpatti.PLAYER_ASKED_FOR_SHOW:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];

                        GameDelegateTeenPatti.showChaal ();
                    }
                    break;

                case ServerTagsTeenpatti.SIDE_SHOW_TRANSFER:
                    {
                        try {
                            string player = s[ServerTagsTeenpatti.SIDE_SHOW_TO_ID];
                            string anotherplayer = s[ServerTagsTeenpatti.SIDE_SHOW_FROM_ID];
                            string message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                            if (player.Equals (GameUser.CurrentUser.ID)) {
                                SideShowPopUp.SetActive (true);
                                SideShowPopUp.GetComponent<SideShowPopUp> ().showData (message, anotherplayer);

                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("SIDE_SHOW_TRANSFER Exception " + ex.Message);
                        }

                    }
                    break;
                case ServerTagsTeenpatti.ROOM_BETTING:
                    {
                        GameDelegateTeenPatti.hideShowPanel ();
                        string player_ID = s[PlayerTagsTeenPatti.PLAYER_ID];

                        if (GameUser.CurrentUser.ID.Equals (player_ID)) {
                            GameDelegateTeenPatti.turnChange ();

                        }

                    }
                    break;

                case "PROFIT_LOSS":
                    {
                        try {
                            string player = s[PlayerTagsTeenPatti.PLAYER_ID];
                            // Debug.Log ("player_id = " + player);
                            if (player.Equals (GameUser.CurrentUser.ID)) {
                                int tes = int.Parse (s["VALUE"]);
                                profitText.text = "" + tes;
                                profitText.transform.position = initPosProfitText;
                                if (tes >= 0) {
                                    StartCoroutine (startProfitTextAnimation (true));

                                } else {
                                    StartCoroutine (startProfitTextAnimation (false));

                                }

                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("PROFIT_LOSS Exception " + ex.Message);
                        }
                    }
                    break;

                case ServerTagsTeenpatti.GAME_VARIATION:
                    {
                        try {
                            int player = int.Parse (s["VALUE"]);
                            //updateGameType(player);
                            //gameTypePanel.SetActive(false);
                        } catch (System.Exception ex) {
                            // Debug.Log ("SIDE_SHOW_TRANSFER Exception " + ex.Message);
                        }

                    }
                    break;

                case ServerTagsTeenpatti.ASK_VARIATION:
                    {
                        try {
                            string player = s[PlayerTagsTeenPatti.PLAYER_ID];
                            if (player.Equals (GameUser.CurrentUser.ID)) {
                                appwrapTeenpatti.sendGameType (1);
                                //gameTypePanel.SetActive(true);
                            }
                        } catch (System.Exception ex) {
                            // Debug.Log ("SIDE_SHOW_TRANSFER Exception " + ex.Message);
                        }

                    }
                    break;

                case "PLAYER_DECLINED_SIDE_SHOW":
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                    }
                    break;

                case ServerTagsTeenpatti.COLLECT_BOOT_AMOUNT:
                    {
                        try {
                            CreateTablePanel.SetActive (false);
                            JoinTablePanel.SetActive (false);
                        } catch (System.Exception ex) {
                            // Debug.Log ("COLLECT_BOOT_AMOUNT exception " + ex.Message);
                        }
                        int BOOT_AMOUNT = int.Parse (s[ServerTagsTeenpatti.BOOT_AMOUNT]);
                        PlayerPrefs.SetInt (PrefebTagsTeenpatti.COLLECT_BOOT_AMOUNT, BOOT_AMOUNT);

                        GameDelegateTeenPatti.collectAmount (BOOT_AMOUNT);
                    }
                    break;
                case ServerTagsTeenpatti.POT_LIMIT_REACHED:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                    }
                    break;
                default:
                    break;
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    void updateGameType (int value) {
        switch (value) {
            case 1:
                gameTypeText.text = "Classic";
                break;
            case 2:
                gameTypeText.text = "Muflis";
                break;
            case 3:
                gameTypeText.text = "Royal";
                break;
            case 4:
                gameTypeText.text = "Joker";
                break;
            case 5:
                gameTypeText.text = "Hukum";
                break;
        }
    }

    IEnumerator startProfitTextAnimation (bool up) {

        float fadeOutTime = 2.0f;
        if (up) {
            profitText.color = Color.green;
            iTween.MoveTo (profitText.gameObject, new Vector3 (profitText.transform.position.x, profitText.transform.position.y + 100, profitText.transform.position.z), 2.0f);
        } else {
            profitText.color = Color.red;
            iTween.MoveTo (profitText.gameObject, new Vector3 (profitText.transform.position.x, profitText.transform.position.y - 100, profitText.transform.position.z), 2.0f);
        }
        Color originalColor = profitText.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime) {
            profitText.color = Color.Lerp (originalColor, Color.clear, Mathf.Min (1, t / fadeOutTime));
            yield return null;
        }

    }

    public void Left () {
        leftPanel.SetActive (true);
    }

    public void showMyProfit () {
        appwrapTeenpatti.getMyProfitAndLoss ();
        profitText.text = "";
    }

}