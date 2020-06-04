using com.shephertz.app42.gaming.multiplayer.client;
using SimpleJSON;
using UnityEngine;

public class appwrapTeenpatti : MonoBehaviour {
   
    private string appKey = "1fd4a535-c1d4-4807-9";
    private string ipAddress = "18.191.163.19";
    private int port = 12348;

    public bool useUDP = true;
    ListenerTeenPatti listen;
    public static string roomID = "";
    public int sessionID = 0;

    // Use this for initialization
    void Start () {
        GameControllerTeenPatti.isConnected = false;
        PlayerPrefs.SetString (TagsTeenpatti.ROOM_ID, "");
        Connection ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void onBytes (byte[] msg) {

    }

    void OnDisable () {
        appwrapTeenpatti.LeftRoom ();
        appwrapTeenpatti.Disconnect ();
    }

    public static void gameRequest () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, TagsTeenpatti.GAME_REQUEST);
        dic.Add (PlayerTagsTeenPatti.DISPLAY_NAME, GameUser.CurrentUser.Name);
        dic.Add (PlayerTagsTeenPatti.PROFILE_PIC, GameUser.CurrentUser.Pic);
        dic.Add (PlayerTagsTeenPatti.TOTAL_COINS, GameUser.CurrentUser.Coin);
        dic.Add (PlayerTagsTeenPatti.IS_GUEST, "" + GameControllerTeenPatti.isGuest);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void sendCardInHand (JSONNode card) {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, "MY_CARD");
        dic.Add (PlayerTagsTeenPatti.CARD_PLAYER_ARRAY, card);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void getMyProfitAndLoss () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, "MY_PROFIT");
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void addCoinToGame (int coin) {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, TagsTeenpatti.ADD_COIN);
        dic.Add (PlayerTagsTeenPatti.TOTAL_COINS, "" + coin);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void sideShowAcceptance (string to, bool accept) {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.SIDE_SHOW_ACCEPTANCE);
        dic.Add (ServerTagsTeenpatti.ACCEPTANCE, "" + accept);
        dic.Add (ServerTagsTeenpatti.SIDE_SHOW_TO_ID, GameUser.CurrentUser.ID);
        dic.Add (ServerTagsTeenpatti.SIDE_SHOW_FROM_ID, "" + to);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void sendGameType (int value) {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.VARIATION_TYPE);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        dic.Add (ServerTagsTeenpatti.VARATION, "" + value);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void SendChaalAmount () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.BET_ACCEPTANCE);
        dic.Add (ServerTagsTeenpatti.ACCEPTANCE, "" + true);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        dic.Add (ServerTagsTeenpatti.BET_AMOUNT, "" + GameControllerTeenPatti.BetAmount);
        dic.Add (PlayerTagsTeenPatti.SIDE_SHOW_REQUEST, "" + false);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void SendShowRequest () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.BET_ACCEPTANCE);
        dic.Add (ServerTagsTeenpatti.ACCEPTANCE, "" + true);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        dic.Add (ServerTagsTeenpatti.BET_AMOUNT, "" + GameControllerTeenPatti.BetAmount);
        dic.Add (PlayerTagsTeenPatti.SIDE_SHOW_REQUEST, "" + true);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void SendPackAmount () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.BET_ACCEPTANCE);
        dic.Add (ServerTagsTeenpatti.ACCEPTANCE, "" + false);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        dic.Add (ServerTagsTeenpatti.BET_AMOUNT, "" + GameControllerTeenPatti.BetAmount);
        dic.Add (PlayerTagsTeenPatti.SIDE_SHOW_REQUEST, "" + false);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void CardSeeRequest () {
        JSONClass dic = new JSONClass ();
        dic.Add (TagsTeenpatti.TAG, TagsTeenpatti.CARD_SHOW_REQUEST);
        dic.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (dic.ToString ());
    }

    public static void joinGame () {
        JSONClass data = new JSONClass ();
        data.Add (TagsTeenpatti.TAG, TagsTeenpatti.GAME_REQUEST);
        data.Add (PlayerTagsTeenPatti.DISPLAY_NAME, PlayerPrefs.GetString (PlayerDetails.RealName));
        appwrapTeenpatti.sendChat (data.ToString ());
    }

    public static void StartPrivateGame () {
        JSONClass data = new JSONClass ();
        data.Add (TagsTeenpatti.TAG, TagsTeenpatti.START_GAME);
        appwrapTeenpatti.sendChat (data.ToString ());
    }

    public static void LeftRoom () {
        JSONClass data = new JSONClass ();
        data.Add (TagsTeenpatti.TAG, ServerTagsTeenpatti.PLAYER_LEFT_ROOM);
        data.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (data.ToString ());
    }

    public static void SendTip () {
        JSONClass data = new JSONClass ();
        data.Add (TagsTeenpatti.TAG, TagsTeenpatti.TIP);
        data.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        data.Add (TagsTeenpatti.COIN, "" + GameControllerTeenPatti.TipAmt);
        appwrapTeenpatti.sendChat (data.ToString ());
    }

    public static void SendStandUp () {
        JSONClass data = new JSONClass ();
        data.Add (TagsTeenpatti.TAG, TagsTeenpatti.STAND_UP);
        data.Add (PlayerTagsTeenPatti.PLAYER_ID, GameUser.CurrentUser.ID);
        appwrapTeenpatti.sendChat (data.ToString ());
    }

    public void Connection () {
        try {

            // Debug.Log ("Check for new connection");

            WarpClient.initialize (appKey, ipAddress, port);
            WarpClient.setRecoveryAllowance (100);
            listen = GetComponent<ListenerTeenPatti> ();
            WarpClient.GetInstance ().AddConnectionRequestListener (listen);
            WarpClient.GetInstance ().AddChatRequestListener (listen);
            WarpClient.GetInstance ().AddLobbyRequestListener (listen);
            WarpClient.GetInstance ().AddNotificationListener (listen);
            WarpClient.GetInstance ().AddRoomRequestListener (listen);
            WarpClient.GetInstance ().AddUpdateRequestListener (listen);
            WarpClient.GetInstance ().AddZoneRequestListener (listen);
            WarpClient.GetInstance ().Connect (GameUser.CurrentUser.ID, "");
            //WarpClient.GetInstance ().Connect (GameControllerTeenPatti.Player_ID, "");
        } catch (System.Exception ex) {
            // Debug.Log ("Connection Exception " + ex.Message);
        }

    }

    public static void Disconnect () {
        if (WarpClient.GetInstance () != null) {
            //// Debug.Log ("Disconnect Server!");
            WarpClient.GetInstance ().Disconnect ();

        }

    }

    static void sendChat (string msg) {
        try {
            if (WarpClient.GetInstance () != null) {
                if (!msg.Equals ("")) {
                    WarpClient.GetInstance ().SendChat (msg);
                }
            }
        } catch (System.Exception ex) {
            // Debug.Log ("sendChat " + ex.Message);
        }
    }
}