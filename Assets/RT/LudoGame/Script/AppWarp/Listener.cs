using UnityEngine;

using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using System;
using System.Collections.Generic;
namespace LudoGameTemplate
{
    public class Listener : MonoBehaviour, ConnectionRequestListener, LobbyRequestListener, ZoneRequestListener, RoomRequestListener, ChatRequestListener, UpdateRequestListener, NotifyListener
    {
        public appwarp m_apppwarp;
        bool tempConnectionError = false;
        //public GameObject SessionExpire;
        public GameObject GameIDBg;
        public Text GameID;
        private void Start()
        {
            GameIDBg.SetActive(false);
            //  GameID.text = "";
            tempConnectionError = false;
            //  SessionExpire.SetActive(false);
            StartCoroutine(ConnectionRecover());

        }


        void sessionExpire()
        {
            // Debug.Log("Session Expire");
            // SessionExpire.SetActive(true);
        }




        IEnumerator ConnectionRecover()
        {
            while (true)
            {
                if (tempConnectionError)
                {
                    Debug.Log("Try to connected");
                    if (!m_apppwarp.ConnectionAbort.activeSelf)
                    {
                        m_apppwarp.ConnectionAbort.SetActive(true);
                        m_apppwarp.OpponentConnectionAbort.SetActive(false);
                        Invoke("sessionExpire", 120.0f);
                    }
                    WarpClient.GetInstance().RecoverConnection();
                    yield return new WaitForSeconds(5.0f);
                }
                else
                {
                    CancelInvoke("sessionExpire");
                    m_apppwarp.ConnectionAbort.SetActive(false);
                }
                yield return new WaitForSeconds(1.0f);
            }
        }

        public void onConnectDone(ConnectEvent eventObj)
        {
            //WarpClient.GetInstance().initUDP();
            Debug.Log("Connection " + eventObj.getResult());

            switch (eventObj.getResult())
            {
                case WarpResponseResultCode.SUCCESS:
                    {
                        m_apppwarp.GameStart = true;
                        appwarp.sessionID = WarpClient.GetInstance().GetSessionId();
                        // Debug.Log("Player join appwarp");
                        m_apppwarp.newConnection = false;
                        GameController.Message = "Waiting for opponent ";
                        // Debug.Log("GameConstantData.GameType " + GameConstantData.GameType);

                        if (GameConstantData.GameType == GameConstantData.Practice)
                        {
                            // Debug.Log("Create Practice Game");
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add(Tags.GameType, GameConstantData.GameType);
                            dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                            dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                            dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                             dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                            dic.Add(Tags.DOMAIN, Tags.URL);
                            WarpClient.GetInstance().CreateRoom("Practice", "Ludo", 1, dic);
                        }
                        if (GameConstantData.GameType == GameConstantData.OneToOne)
                        {
                            // Debug.Log("Join One2One Game");
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add(Tags.GameType, GameConstantData.GameType);
                            dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                            dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                             dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                            dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                            dic.Add(Tags.DOMAIN, Tags.URL);
                            WarpClient.GetInstance().JoinRoomWithProperties(dic);
                        }

                        if (GameConstantData.GameType == GameConstantData.OneToFour)
                        {
                            // Debug.Log("Join One2Four Game");
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add(Tags.GameType, GameConstantData.GameType);
                            dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                            dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                             dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                            dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                            dic.Add(Tags.DOMAIN, Tags.URL);
                            WarpClient.GetInstance().JoinRoomWithProperties(dic);
                        }
                        string playerType = SecurePlayerPrefs.GetString(GameTags.PRIVATE_TABLE_TYPE);
                        if (GameConstantData.GameType == GameConstantData.Private)
                        {


                            if (playerType.Equals(GameTags.CREATE_TABLE))
                            {
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add(Tags.GameType, GameConstantData.GameType);
                                dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                                dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                                 dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                                dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                                dic.Add(Tags.DOMAIN, Tags.URL);
                                // Debug.Log("Create Private Game");
                                WarpClient.GetInstance().CreateRoom("private", "ludo", 2, dic);
                            }
                            if ((playerType.Equals(GameTags.JOIN_TABLE)))
                            {
                                // Debug.Log("Waiting for Join ");
                            }
                        }


                        // Debug.Log ("Player join appwarp " + playerType);
                        if ((playerType.Equals(GameTags.JOIN_TABLE)))
                        {
                            // Debug.Log ("join table  ");
                        }
                        else if ((playerType.Equals(GameTags.FB_FRIEND_ONLINE)))
                        {
                            // Debug.Log ("join FB_FRIEND_ONLINE  ");
                            if (WarpClient.GetInstance() != null)
                            {
                                WarpClient.GetInstance().JoinRoom(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.ROOM_ID));
                            }
                        }
                    }
                    break;
                case WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE:
                    {
                        GameDelegate.changeSocketConnection(false);
                        tempConnectionError = true;
                        //// Debug.Log ("Weak connection");
                        // ("Weak connection");
                        // GameController.Message = "Recovering Connection";
                        //GameController.Message1 = "Recovering Connection ";
                        //if (WarpClient.GetInstance() != null)
                        //{
                        //    WarpClient.GetInstance().RecoverConnection();
                        //}
                    }
                    break;
                case WarpResponseResultCode.SUCCESS_RECOVERED:
                    {
                        // ("Connection recover");
                        GameController.Message1 = "";
                        // GameController.Message = "Recovering Connection success ";
                        GameDelegate.changeSocketConnection(true);
                        tempConnectionError = false;
                        //// Debug.Log ("Recover connection");

                    }
                    break;
                case WarpResponseResultCode.CONNECTION_ERR:
                    {
                        // Debug.Log ("CONNECTION_ERR  has been occur");
                        //GameController.showErrorMsgl ();
                        GameController.Message = " Connection error ";
                        GameDelegate.changeSocketConnection(false);
                    }
                    break;
                case WarpResponseResultCode.AUTH_ERROR:
                    {
                        // Debug.Log ("AUTH_ERROR has been occur");	
                        //GameController.showErrorMsgl ();
                        //ErroeMsg ();
                        GameDelegate.changeSocketConnection(false);
                        //UnityMainThreadDispatcher.Instance().Enqueue(ErroeMsg());
                        //				GetComponent<GameScene> ().reconnectPanel.SetActive (true);

                        GameController.Message = "Server not responding. try again";
                    }
                    break;
                case WarpResponseResultCode.BAD_REQUEST:
                    {
                        GameController.Message = "Server not responding. try again";

                        // Debug.Log ("BAD_REQUEST has been occur");
                        if (m_apppwarp.newConnection)
                        {
                            // Debug.Log("refresh connection");
                            string player_name = UserController.getInstance.ID;
                            WarpClient.GetInstance().Disconnect();
                            WarpClient.GetInstance().Connect(player_name, "");
                        }
                        else
                        {
                            GameDelegate.changeSocketConnection(false);
                        }
                        //GameController.showErrorMsgl ();
                        //				GetComponent<GameScene> ().reconnectPanel.SetActive (true);
                        //ErroeMsg();

                        //UnityMainThreadDispatcher.Instance().Enqueue(ErroeMsg());
                    }
                    break;

                default:


                    break;
            }


        }



        public void onInitUDPDone(byte res)
        {

        }

        public void onLog(String message)
        {

        }

        public void onDisconnectDone(ConnectEvent eventObj)
        {
            // Debug.Log("disconnect " + eventObj.getResult());
        }

        public void onJoinLobbyDone(LobbyEvent eventObj)
        {

            if (eventObj.getResult() == 0)
            {
                // Debug.Log("Lobby is Join");
                WarpClient.GetInstance().SubscribeLobby();
                //WarpClient.GetInstance ().GetOnlineUsers ();

            }
        }

        public void onLeaveLobbyDone(LobbyEvent eventObj)
        {
            //Twist.AppController.showToast ("onLeaveLobbyDone");
        }

        public void onSubscribeLobbyDone(LobbyEvent eventObj)
        {

            if (eventObj.getResult() == 0)
            {

                // Debug.Log("Lobby is subscribe");


            }
        }

        public void onUnSubscribeLobbyDone(LobbyEvent eventObj)
        {
            //Twist.AppController.showToast ("onUnSubscribeLobbyDone");
        }

        public void onGetLiveLobbyInfoDone(LiveRoomInfoEvent eventObj)
        {
            //Twist.AppController.showToast ("onGetLiveLobbyInfoDone");
        }


        public void onDeleteRoomDone(RoomEvent eventObj)
        {
            //Twist.AppController.showToast ("onDeleteRoomDone");
            if (eventObj.getResult() == 0)
            {
            }

        }

        public void onGetAllRoomsDone(AllRoomsEvent eventObj)
        {
            //Twist.AppController.showToast ("onGetAllRoomsDone");

        }

        public void onCreateRoomDone(RoomEvent eventObj)
        {
            // Debug.Log("Room created " + eventObj.getResult());
            if (eventObj.getResult() == 0)
            {

                if (WarpClient.GetInstance() != null)
                {
                    WarpClient.GetInstance().JoinRoom(eventObj.getData().getId());
                }
            }

        }

        public void onGetOnlineUsersDone(AllUsersEvent eventObj)
        {
            //Twist.AppController.showToast ("onGetOnlineUsersDone");

            //		if (eventObj.getResult () == 0) {
            //			String[] playersName = eventObj.getUserNames ();
            //			foreach (String n in playersName) {
            //				print ("player name  is "+n);
            //				GameController.newPlayer (n);
            //			}
            //
            //		}
        }

        public void onGetLiveUserInfoDone(LiveUserInfoEvent eventObj)
        {
            //Twist.AppController.showToast ("onGetLiveUserInfoDone");

        }

        public void onSetCustomUserDataDone(LiveUserInfoEvent eventObj)
        {
            //Twist.AppController.showToast ("onSetCustomUserDataDone");
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {


            }
        }

        public void onGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
        {
            //Twist.AppController.showToast ("onGetMatchedRoomsDone");
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {

                foreach (var roomData in eventObj.getRoomsData())
                {

                }
            }
        }

        public void onSubscribeRoomDone(RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
                // set room id in prefeb
                //SecurePlayerPrefs.SetString (GameTags.ROOM_ID, eventObj.getData ().getId ());
                // (eventObj.getData ().getId ());

                string roomID = eventObj.getData().getId();

                if (WarpClient.GetInstance() != null)
                {
                    //WarpClient.GetInstance ().GetLiveRoomInfo (eventObj.getData ().getId ());
                    //("onSubscribeRoomDone successful");
                    appwarp.joinGame();
                }
                if (GameConstantData.GameType == GameConstantData.Private)
                {
                    string playerType = SecurePlayerPrefs.GetString(GameTags.PRIVATE_TABLE_TYPE);
                    if (playerType.Equals(GameTags.CREATE_TABLE))
                    {
                        GameDelegate.showSendTableCode(roomID);
                    }


                }
                //			string tableType = SecurePlayerPrefs.GetString (GameTags.PRIVATE_TABLE_TYPE);
                //			if ((tableType.Equals (GameTags.CREATE_TABLE))) {
                //				// show create table popup
                //				GameDelegate.showSendTableCode (roomID);
                //				//SecurePlayerPrefs.SetString (GameTags.PRIVATE_TABLE_TYPE, "");
                //			} 

            }


        }

        public void onUnSubscribeRoomDone(RoomEvent eventObj)
        {

        }

        public void onJoinRoomDone(RoomEvent eventObj)
        {

            if (eventObj.getResult() == 0)
            {
                // Debug.Log("Join Success fully");
                if (WarpClient.GetInstance() != null)
                {
                    WarpClient.GetInstance().SubscribeRoom(eventObj.getData().getId());
                    if (GameConstantData.GameType == GameConstantData.OneToOne)
                    {
                        GameIDBg.SetActive(true);
                        GameID.text = "Game ID : " + eventObj.getData().getId();
                    }
                    else if (GameConstantData.GameType == GameConstantData.OneToFour)
                    {
                        GameIDBg.SetActive(true);
                        GameID.text = "Game ID : " + eventObj.getData().getId();
                    }
                    else if (GameConstantData.GameType == GameConstantData.Private)
                    {
                        GameIDBg.SetActive(true);
                        GameID.text = "Game ID : " + eventObj.getData().getId();
                    }
                    else
                    {
                        GameIDBg.SetActive(false);
                        GameID.text = " ";
                    }
                }

            }
            else
            {


                if (GameConstantData.GameType == GameConstantData.Practice)
                {
                    // Debug.Log("Join Fail Create Practice Game Again");
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(Tags.GameType, GameConstantData.GameType);
                    dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                    dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                    dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                     dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                    dic.Add(Tags.DOMAIN, Tags.URL);
                    WarpClient.GetInstance().CreateRoom("Practice", "Ludo", 1, dic);


                }
                if (GameConstantData.GameType == GameConstantData.OneToOne)
                {
                    // Debug.Log("Join Fail Create one2one Game Again");
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(Tags.GameType, GameConstantData.GameType);
                    dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                    dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                     dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                    dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                    dic.Add(Tags.DOMAIN, Tags.URL);
                    WarpClient.GetInstance().CreateRoom("One2One", "ludo", 2, dic);
                }
                if (GameConstantData.GameType == GameConstantData.OneToFour)
                {
                    // Debug.Log("Join Fail Create one2Four Game Again");
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(Tags.GameType, GameConstantData.GameType);
                    dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                    dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                     dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                    dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                    dic.Add(Tags.DOMAIN, Tags.URL);
                    WarpClient.GetInstance().CreateRoom("One2Four", "ludo", 4, dic);
                }

                if (GameConstantData.GameType == GameConstantData.Private)
                {
                    string playerType = SecurePlayerPrefs.GetString(GameTags.GAME_TYPE);
                    if (playerType.Equals(GameTags.CREATE_TABLE))
                    {
                        // Debug.Log("Join Fail Create private Game Again");
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(Tags.GameType, GameConstantData.GameType);
                        dic.Add(Tags.GAME_ENTRY, GameConstantData.entryFee);
                        dic.Add(Tags.GAME_PRICE, GameConstantData.winingAmount);
                         dic.Add("TOKEN_LIMIT", GameConstantData.TokenLimit);
                        dic.Add(Tags.UserLimit, GameConstantData.UserLimit);
                        dic.Add(Tags.DOMAIN, Tags.URL);
                        WarpClient.GetInstance().CreateRoom("private", "ludo", 2, dic);
                    }
                    if ((playerType.Equals(GameTags.JOIN_TABLE)))
                    {
                        // Debug.Log("Waiting for Join Again");
                    }

                }

                /*

                String tableType = SecurePlayerPrefs.GetString (GameTags.PRIVATE_TABLE_TYPE);
                //	GameController.showToast ("Try to create a room");
                if (WarpClient.GetInstance () != null) {

                    if ((tableType.Equals (GameTags.CREATE_TABLE)) || (tableType.Equals (GameTags.JOIN_TABLE) || (tableType.Equals (GameTags.CHALLENGE_FRIEND)))) {

                        GameConstantData.showToast (transform,"Invalid room id unable to join game!");
                    } else {

                        Dictionary<string,object> dic = new Dictionary <string,object> ();
                        dic.Add (Tags.GameType, GameConstantData.GameType);
                        dic.Add (Tags.GAME_ENTRY, GameConstantData.entryFee);
                        dic.Add (Tags.GAME_PRICE, GameConstantData.winingAmount);
                        dic.Add (Tags.UserLimit, GameConstantData.UserLimit);
                        WarpClient.GetInstance ().CreateRoom ("rummy", "game", GameConstantData.UserLimit, dic);
                    }
                }*/

            }
        }

        public void onLockPropertiesDone(byte result)
        {
            //Twist.AppController.showToast ("onLockPropertiesDone");
        }

        public void onUnlockPropertiesDone(byte result)
        {
            //Twist.AppController.showToast ("onUnlockPropertiesDone");	
        }

        public void onLeaveRoomDone(RoomEvent eventObj)
        {

        }

        public void onGetLiveRoomInfoDone(LiveRoomInfoEvent eventObj)
        {

        }

        public void onSetCustomRoomDataDone(LiveRoomInfoEvent eventObj)
        {

        }

        public void onUpdatePropertyDone(LiveRoomInfoEvent eventObj)
        {

            if (WarpResponseResultCode.SUCCESS == eventObj.getResult())
            {

            }
            else
            {

            }
        }




        public void onSendChatDone(byte result)
        {
            //Twist.AppController.showToast ("onSendChatDone");

        }

        public void onSendPrivateChatDone(byte result)
        {
            //Twist.AppController.showToast ("onSendPrivateChatDone");
        }


        public void onSendUpdateDone(byte result)
        {
            //Twist.AppController.showToast ("onSendUpdateDone");
        }


        public void onRoomCreated(RoomData eventObj)
        {
            //Twist.AppController.showToast ("onRoomCreated");
        }

        public void onRoomDestroyed(RoomData eventObj)
        {
            //Twist.AppController.showToast ("onRoomDestroyed");
        }

        public void onUserLeftRoom(RoomData eventObj, string username)
        {


            //		GameController.removePlayer(username);

        }

        public void onUserJoinedRoom(RoomData eventObj, string username)
        {

        }

        public void onUserLeftLobby(LobbyData eventObj, string username)
        {

        }

        public void onUserJoinedLobby(LobbyData eventObj, string username)
        {
            //		string name = SecurePlayerPrefs.GetString ("name");
            //		if (name ==username) {
            //			print ("player name and opponent name is same ");
            //		} else {
            //			GameController.newPlayer (username);
            //
            //		}

            //Twist.AppController.showToast ("onUserJoinedLobby");
        }

        public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
        {
            //Twist.AppController.showToast ("onUserChangeRoomProperty");
        }

        public void onPrivateChatReceived(string sender, string message)
        {
            //	Twist.AppController.showToast ("onPrivateChatReceived");	

        }



        public void onMoveCompleted(MoveEvent move)
        {
            //Twist.AppController.showToast ("onMoveCompleted");	
        }

        public void onChatReceived(ChatEvent eventObj)
        {

            // Debug.Log(eventObj.getSender() + "  :  " + eventObj.getMessage());
            if (WarpClient.GetInstance() != null)
            {
                GameDelegate.chatRecived(eventObj.getSender(), eventObj.getMessage());
            }

        }

        public void onUpdatePeersReceived(UpdateEvent eventObj)
        {
            try
            {
                m_apppwarp.onBytes(eventObj.getUpdate());
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
        }


        public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<String, System.Object> properties)
        {
            //Twist.AppController.showToast ("onUserChangeRoomProperty");

        }

        public void onUserPaused(string a, bool b, string c)
        {
            if (c.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
            {
                m_apppwarp.ConnectionAbort.SetActive(true);
                m_apppwarp.OpponentConnectionAbort.SetActive(false);
            }
            else
            {
                m_apppwarp.OpponentConnectionAbort.SetActive(true);
            }
        }

        public void onUserResumed(string a, bool b, string c)
        {

            if (c.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
            {
                m_apppwarp.ConnectionAbort.SetActive(false);
                m_apppwarp.OpponentConnectionAbort.SetActive(false);
            }
            else
            {
                m_apppwarp.OpponentConnectionAbort.SetActive(false);
            }

        }

        public void onGameStarted(string a, string b, string c)
        {
            //Twist.AppController.showToast ("onGameStarted");
        }

        public void onGameStopped(string a, string b)
        {
            //Twist.AppController.showToast ("onGameStopped");
        }



        public void onInvokeZoneRPCDone(RPCEvent evnt)
        {
            //Twist.AppController.showToast ("onInvokeZoneRPCDone");
        }

        public void onInvokeRoomRPCDone(RPCEvent evnt)
        {
            //Twist.AppController.showToast ("onInvokeRoomRPCDone");
        }

        public void sendMsg(string msg)
        {
            //Twist.AppController.showToast ("sendMsg");
            WarpClient.GetInstance().SendChat(msg);

        }

        public void sendBytes(byte[] msg, bool useUDP)
        {

            // try
            // {
            if (useUDP == true)
                WarpClient.GetInstance().SendUDPUpdatePeers(msg);
            else
                WarpClient.GetInstance().SendUpdatePeers(msg);
            // }catch(System.Exception ex){
            //     // Debug.Log("Data finding "+ex);
            // }

        }
    }
}