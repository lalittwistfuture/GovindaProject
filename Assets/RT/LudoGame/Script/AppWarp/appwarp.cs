using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using System;
using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.SimpleJSON;
namespace LudoGameTemplate
{
    public class appwarp : MonoBehaviour
    {

        // //connect to local server
        // private string appKey = "af845c3b-ae3f-4804-9";
        // private string ipAddress = "192.168.1.4";
        // private int port = 12347;



        // connect to Live Server
        private string appKey = "c22a2cf5-1fc3-4b58-b";
        private string ipAddress = "18.191.163.19";
        private  int port = 12347;


        public static string roomID = "";
        public static string username = "Lalit";
        public static int sessionID = 0;
        public bool newConnection = true;
        public bool useUDP = true;
        public Thread serverThread;
        public static int state = 0;
        protected bool sendData = false;
        public static bool InternetConnectivity = true;
        protected string data = "";
        protected Queue sendDataQueue;
        bool WarpClientStatus = true;
        Listener listen;
        public static int attempt_server = 0;
        int limit = 100;
        public GameObject ConnectionAbort;
        public GameObject OpponentConnectionAbort;
        int opponent = 50;
        int player = 50;
        int timer = 120;
        public Text TimerText;
        public GameObject BoosterPanel;
        public GameObject BoosterBtn;
        public bool GameStart = false;

        public void refreshConnection()
        {

            //WarpClient.GetInstance().RecoverConnectionWithSessioId(appwarp.sessionID,SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID));
        }



        void Start()
        {
            opponent = limit;
            player = limit;
            OpponentConnectionAbort.SetActive(false);
            ConnectionAbort.SetActive(false);
            BoosterPanel.SetActive(false);
            BoosterBtn.SetActive(false);

        }

        void onSocketConnectionChange(bool status)
        {
            // WarpClientStatus = status;
        }

        private void OnEnable()
        {
            Invoke("Connection", 1.0f);
            // Connection();
            GameDelegate.onSocketConnectionChange += onSocketConnectionChange;
            GameDelegate.onRecivedMassage += onRecivedMassage;
        }


        private void OnDisable()
        {
            GameDelegate.onSocketConnectionChange -= onSocketConnectionChange;
            GameDelegate.onRecivedMassage -= onRecivedMassage;
        }

        void onRecivedMassage(string sender, string msg)
        {
            try
            {

                JSONNode s = JSON.Parse(msg);

                switch (s[ServerTags.TAG])
                {

                    case ServerTags.ACTIVE_BOOSTER:
                        {
                            string player1 = UserController.getInstance.ID;
                            if (player1.Equals(s[ServerTags.PLAYER]))
                            {
                                BoosterBtn.SetActive(false);
                                BoosterPanel.SetActive(true);
                            }

                        }
                        break;
                    case DeviceTags.DICE_ROLL_DONE_COMPLETE:
                        {
                            BoosterBtn.SetActive(false);
                            BoosterPanel.SetActive(false);
                        }
                        break;
                    case ServerTags.TURN:
                        {
                            BoosterPanel.SetActive(false);
                        }
                        break;
                    case ServerTags.DICE_ROLL:
                        {
                            //string player_name = userName.text;
                            //try
                            //{
                            string player1 = UserController.getInstance.ID;
                            if (player1.Equals(s[ServerTags.PLAYER]))
                            {
                                if (int.Parse(s[ServerTags.USER_LIMIT]) > 2)
                                {
                                    BoosterBtn.SetActive(true);
                                }
                                else
                                {
                                    BoosterBtn.SetActive(false);
                                }
                            }
                            else
                            {
                                BoosterBtn.SetActive(false);
                            }
                            //}
                            //catch (System.Exception ex)
                            //{
                            //    // Debug.Log(ex.Message);
                            //}

                        }
                        break;
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        void FixedUpdate()
        {
            /* if (GameStart)
             {
                 if (player < 0)
                 {
                     if (!ConnectionAbort.activeSelf)
                     {
                         ConnectionAbort.SetActive(true);
                         LoadingPanel.serverConnected = false;
                     }
                     if (OpponentConnectionAbort.activeSelf)
                         OpponentConnectionAbort.SetActive(false);
                 }
                 else
                 {
                     LoadingPanel.serverConnected = true;
                     if (opponent < 0)
                     {
                         if (!OpponentConnectionAbort.activeSelf)
                         {
                             OpponentConnectionAbort.SetActive(true);
                             timer = 120;
                             CancelInvoke("StartCountDown");
                             InvokeRepeating("StartCountDown", 0.0f, 1.0f);
                         }
                     }
                     else
                     {
                         if (OpponentConnectionAbort.activeSelf)
                         {
                             CancelInvoke("StartCountDown");
                             OpponentConnectionAbort.SetActive(false);
                         }
                     }
                     if (ConnectionAbort.activeSelf)
                         ConnectionAbort.SetActive(false);
                 }
                 opponent--;
                 player--;
                 if (GameConstantData.GameType == GameConstantData.Practice)
                 {
                     opponent = limit;
                 }
             }

             */
        }

        //void StartCountDown(){
        //    timer--;
        //    TimerText.text = "" + timer;
        //}



        public static void sendAnimationDone()
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, "DONE");
            appwarp.sendChat(data.ToString());
        }

        public static void SendUserChat(string player_name, string msg)
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, ServerTags.CHATTING_START);
            data.Add(ServerTags.PLAYER_NAME, UserController.getInstance.ID);
            data.Add(ServerTags.CHAT_MSG, msg);
            appwarp.sendChat(data.ToString());
        }

        public static void SendEmoji(string player_name, string EmojiNumber)
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, ServerTags.EMOJI);
            data.Add(ServerTags.PLAYER_NAME, UserController.getInstance.ID);
            data.Add(ServerTags.EMOJI_NUMBER, EmojiNumber);
            appwarp.sendChat(data.ToString());
        }

        public static void leaveGame()
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, "LEAVE_GAME");
            appwarp.sendChat(data.ToString());
        }

        public static void joinGame()
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, ServerTags.GAME_REQUEST);
            data.Add(DeviceTags.COIN, UserController.getInstance.Coin);
            data.Add(DeviceTags.DISPLAY_NAME, "" + UserController.getInstance.Name);
            data.Add(DeviceTags.PIC, UserController.getInstance.Image);
            data.Add(ServerTags.PLAYER_ID, UserController.getInstance.ID);
            data.Add(DeviceTags.TOTAL_MATCH, "12");
            data.Add(DeviceTags.WON_MATCH, "3");
            appwarp.sendChat(data.ToString());
        }

        public static void sendTapDone(int number)
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, DeviceTags.DICE_ROLL_DONE);
            data.Add(ServerTags.PLAYER, UserController.getInstance.ID);
            data.Add("VALUE", "" + number);
            appwarp.sendChat(data.ToString());
        }

        public void sendBoosterNumber(Button number)
        {
            JSONClass data1 = new JSONClass();
            data1.Add(ServerTags.TAG, DeviceTags.DICE_ROLL_DONE);
            data1.Add(ServerTags.PLAYER, UserController.getInstance.ID);
            data1.Add("VALUE", "" + number.name);
            appwarp.sendChat(data1.ToString());
        }
        public void sendBoosterRequest()
        {
            JSONClass data1 = new JSONClass();
            data1.Add(ServerTags.TAG, "BOOSTER_REQUEST");
            data1.Add(ServerTags.PLAYER, UserController.getInstance.ID);
            appwarp.sendChat(data1.ToString());
        }
        public static void userSelectGoti(int pos, int number)
        {
            JSONClass data = new JSONClass();
            data.Add(ServerTags.TAG, DeviceTags.SELECT_GOTI);
            data.Add(ServerTags.POSITION, "" + pos);
            data.Add("STEPS", "" + number);
            data.Add(ServerTags.PLAYER, UserController.getInstance.ID);
            appwarp.sendChat(data.ToString());
        }

        static void sendChat(string msg)
        {
            try
            {
                if (WarpClient.GetInstance() != null)
                {
                    if (!msg.Equals(""))
                    {
                        WarpClient.GetInstance().SendChat(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //print (ex);
            }
        }


        public void Connection()
        {
            // Debug.Log ("working "+UserController.getInstance.ID);
            string player_name = UserController.getInstance.ID;
            WarpClient.initialize(appKey, ipAddress, port);
            WarpClient.setRecoveryAllowance(120);
            newConnection = true;
            listen = GetComponent<Listener>();
            WarpClient.GetInstance().AddConnectionRequestListener(listen);
            WarpClient.GetInstance().AddChatRequestListener(listen);
            WarpClient.GetInstance().AddLobbyRequestListener(listen);
            WarpClient.GetInstance().AddNotificationListener(listen);
            WarpClient.GetInstance().AddRoomRequestListener(listen);
            WarpClient.GetInstance().AddUpdateRequestListener(listen);
            WarpClient.GetInstance().AddZoneRequestListener(listen);
            WarpClient.GetInstance().Connect(player_name, "");

        }

        public static void Disconnect()
        {
            if (WarpClient.GetInstance() != null)
            {
                WarpClient.GetInstance().Disconnect();

            }


        }


        public void onBytes(byte[] msg)
        {
            try
            {
                int[] data_f = new int[3];
                char[] data_c = new char[(msg.Length - (sizeof(int) * 3)) / sizeof(char)];
                System.Buffer.BlockCopy(msg, 0, data_f, 0, sizeof(int) * 3);
                System.Buffer.BlockCopy(msg, sizeof(int) * 3, data_c, 0, msg.Length - (sizeof(int) * 3));
                string sender = new string(data_c);
                if (!sender.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
                {
                    //if (data_f[2] == 8)
                    //{
                    //    opponent = limit;
                    //}
                    GameDelegate.playerData(sender, data_f);
                }
                //}else{
                //    if(data_f[2]==8){
                //        player = limit; 
                //    }
                //}
            }
            catch (System.Exception ed)
            {
                // Debug.Log(ed.Message);
            }

        }

        //	public	void LeaveRoom ()
        //	{
        //
        //
        //		appwarp.Disconnect ();
        //		StartCoroutine (LeaveRoomNow ());
        //	}
        //
        //	IEnumerator LeaveRoomNow ()
        //	{
        //		yield return new WaitForSeconds (2.0f);
        //		SceneManager.LoadSceneAsync ("MainLobby");
        //	}

    }
}
