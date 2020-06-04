//using UnityEngine
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListenerTeenPatti : MonoBehaviour, ConnectionRequestListener, LobbyRequestListener, ZoneRequestListener, RoomRequestListener, ChatRequestListener, UpdateRequestListener, NotifyListener {

    bool tempFail = false;

    private void Start () {

        StartCoroutine (recover ());
        tempFail = false;
    }
    public void onConnectDone (ConnectEvent eventObj) {
        // Debug.Log ("onConnectDone Result " + eventObj.getResult ());

        switch (eventObj.getResult ()) {

            case WarpResponseResultCode.SUCCESS:
                {
                    // Debug.Log ("Appwrap join success");

                    GameControllerTeenPatti.TeenPatti_message = MessageScriptTeenPatti.APPWRAP_SUCCESS;
                    // GameController.Message = MessageScriptTeenPatti.APPWRAP_SUCCESS;
                    PlayerPrefs.SetInt ("SESSION_ID", WarpClient.GetInstance ().GetSessionId ());
                    // Debug.Log ("session id");
                    // RouletteDelegate.onErrorOccure(ErrorType.RecoverConnection);
                    GameControllerTeenPatti.NetworkStatus = NetworkTagsTeenPatti.CONNECTED;
                    // Debug.Log (NetworkTagsTeenPatti.CONNECTED);
                    GameControllerTeenPatti.isConnected = true;
                    // Debug.Log ("private outside");
                    if (GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PRIVATE)) {
                        // Debug.Log ("private inside" + TagsTeenpatti.PRIVATE);
                        if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.CREATE_PRIVATE_TABLE)) {
                            Dictionary<string, object> dic = new Dictionary<string, object> ();
                            dic.Add (TagsTeenpatti.GAME_TYPE, "" + TagsTeenpatti.PRIVATE_GAME);
                            dic.Add (TagsTeenpatti.BOOT_AMOUNT, "" + GameControllerTeenPatti.BootAmount);
                            dic.Add (TagsTeenpatti.PORT_LIMIT, "" + GameControllerTeenPatti.PortLimit);
                            dic.Add (TagsTeenpatti.MAX_BET_AMOUNT, "" + GameControllerTeenPatti.MaxBetAmt);
                            dic.Add (TagsTeenpatti.DOMAIN, "" + WebManager.Instance.getURL());
                            dic.Add (TagsTeenpatti.MAX_BLIND, "4");
                            WarpClient.GetInstance ().CreateRoom ("private", "teenpatti", 6, dic);

                        } else if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.JOIN_PRIVATE_TABLE)) {

                        }

                    } else if (GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PUBLIC)) {

                        //// Debug.Log ("GameType PUBLIC");
                        Dictionary<string, object> dic = new Dictionary<string, object> ();
                        dic.Add (TagsTeenpatti.GAME_TYPE, "" + TagsTeenpatti.PUBLIC_GAME);
                        dic.Add (TagsTeenpatti.BOOT_AMOUNT, "" + GameControllerTeenPatti.BootAmount);
                        dic.Add (TagsTeenpatti.PORT_LIMIT, "" + GameControllerTeenPatti.PortLimit);
                        dic.Add (TagsTeenpatti.MAX_BET_AMOUNT, "" + GameControllerTeenPatti.MaxBetAmt);
                        dic.Add (TagsTeenpatti.DOMAIN, "" + WebManager.Instance.getURL());
                        dic.Add (TagsTeenpatti.MAX_BLIND, "4");
                        WarpClient.GetInstance ().JoinRoomWithProperties (dic);
                        //WarpClient.GetInstance ().JoinRoomInRange (1, 2, true);
                    }

                }
                break;
            case WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE:
                {
                 
                    GameControllerTeenPatti.NetworkStatus = NetworkTagsTeenPatti.RECOVER_CONNECTION;
                   
                    GameControllerTeenPatti.isConnected = false;
                    tempFail = true;
                    
                }
                break;
            case WarpResponseResultCode.SUCCESS_RECOVERED:
                {
                  
                    GameControllerTeenPatti.isConnected = true;
                    tempFail = false;
                   
                }
                break;
            case WarpResponseResultCode.CONNECTION_ERR:
                {
                    GameControllerTeenPatti.TeenPatti_message = MessageScriptTeenPatti.APPWRAP_CONNECTION_ERROR;
                    GameControllerTeenPatti.NetworkStatus = NetworkTagsTeenPatti.NOT_CONNECTED;
                    GameControllerTeenPatti.isConnected = false;

                    
                }
                break;
            case WarpResponseResultCode.AUTH_ERROR:
                {
                    GameControllerTeenPatti.NetworkStatus = NetworkTagsTeenPatti.NOT_CONNECTED;
                    GameControllerTeenPatti.TeenPatti_message = MessageScriptTeenPatti.APPWRAP_AUTH_ERROR;
                    WarpClient.GetInstance ().RecoverConnectionWithSessioId (PlayerPrefs.GetInt ("SESSION_ID"), GameUser.CurrentUser.ID);
                    GameControllerTeenPatti.isConnected = false;
                    tempFail = false;
                }
                break;
            case WarpResponseResultCode.BAD_REQUEST:
                {
                    GameControllerTeenPatti.NetworkStatus = NetworkTagsTeenPatti.NOT_CONNECTED;
                    GameControllerTeenPatti.TeenPatti_message = MessageScriptTeenPatti.APPWRAP_AUTH_ERROR;
                    GameControllerTeenPatti.isConnected = false;
                }
                break;

            default:

                break;
        }

    }

    IEnumerator recover () {
        while (true) {
            if (tempFail) {
                try {
                    GameDelegateTeenPatti.connctionChange (GameUser.CurrentUser.ID, false);
                    // Debug.Log ("Attempt server");
                    WarpClient.GetInstance ().RecoverConnection ();
                } catch (System.Exception ex) {
                    // Debug.Log (ex.Message);
                }
                yield return new WaitForSeconds (8);
            }
            yield return new WaitForSeconds (1);

        }
    }

    public void onInitUDPDone (byte res) {
        //Twist.AppController.showToast ("onInitUDPDone");
    }

    public void onLog (String message) {

    }

    public void onDisconnectDone (ConnectEvent eventObj) {
        //appwarp.isConnected = false;

    }

    public void onJoinLobbyDone (LobbyEvent eventObj) {
        //Twist.AppController.showToast ("onJoinLobbyDone");
        if (eventObj.getResult () == 0) {

            WarpClient.GetInstance ().SubscribeLobby ();
            WarpClient.GetInstance ().GetOnlineUsers ();

        }
    }

    public void onLeaveLobbyDone (LobbyEvent eventObj) {
        //Twist.AppController.showToast ("onLeaveLobbyDone");
    }

    public void onSubscribeLobbyDone (LobbyEvent eventObj) {

        //Twist.AppController.showToast ("onSubscribeLobbyDone");
        if (eventObj.getResult () == 0) { }
    }

    public void onUnSubscribeLobbyDone (LobbyEvent eventObj) {
        //Twist.AppController.showToast ("onUnSubscribeLobbyDone");
    }

    public void onGetLiveLobbyInfoDone (LiveRoomInfoEvent eventObj) {
        //Twist.AppController.showToast ("onGetLiveLobbyInfoDone");
    }

    public void onDeleteRoomDone (RoomEvent eventObj) {
        //Twist.AppController.showToast ("onDeleteRoomDone");
        if (eventObj.getResult () == 0) { }

    }

    public void onGetAllRoomsDone (AllRoomsEvent eventObj) {
        //Twist.AppController.showToast ("onGetAllRoomsDone");

    }

    public void onCreateRoomDone (RoomEvent eventObj) {
        if (eventObj.getResult () == 0) {

            WarpClient.GetInstance ().JoinRoom (eventObj.getData ().getId ());
        }

    }

    public void onGetOnlineUsersDone (AllUsersEvent eventObj) {

    }

    public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj) {
        //Twist.AppController.showToast ("onGetLiveUserInfoDone");

    }

    public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj) {
        //Twist.AppController.showToast ("onSetCustomUserDataDone");
        if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {

        }
    }

    public void onGetMatchedRoomsDone (MatchedRoomsEvent eventObj) {
        //Twist.AppController.showToast ("onGetMatchedRoomsDone");
        if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {

            foreach (var roomData in eventObj.getRoomsData ()) {

            }
        }
    }

    public void onSubscribeRoomDone (RoomEvent eventObj) {
        if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
            appwrapTeenpatti.roomID = eventObj.getData ().getId ();
            GameControllerTeenPatti.isConnected = true;
            PlayerPrefs.SetString (TagsTeenpatti.ROOM_ID, eventObj.getData ().getId ());

            
            if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.JOIN_PRIVATE_TABLE)) {
                WarpClient.GetInstance ().GetLiveRoomInfo (eventObj.getData ().getId ());
            } else {
                appwrapTeenpatti.gameRequest ();
            }
           
            if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.CREATE_PRIVATE_TABLE)) {
                GameDelegateTeenPatti.ShowPrivateTableCode (eventObj.getData ().getId ());

            }
        }

      

    }

    public void onUnSubscribeRoomDone (RoomEvent eventObj) {

    }

    public void onJoinRoomDone (RoomEvent eventObj) {

        if (eventObj.getResult () == 0) {
            appwrapTeenpatti.roomID = eventObj.getData ().getId ();
            WarpClient.GetInstance ().SubscribeRoom (eventObj.getData ().getId ());
        } else {

            if (GameControllerTeenPatti.GameType.Equals (TagsTeenpatti.PRIVATE)) {

                GameControllerTeenPatti.showToast (MessageScriptTeenPatti.INVALID_ROOM_MESSAGE);

            } else {

                Dictionary<string, object> dic = new Dictionary<string, object> ();
                dic.Add (TagsTeenpatti.GAME_TYPE, "" + TagsTeenpatti.PUBLIC_GAME);
                dic.Add (TagsTeenpatti.BOOT_AMOUNT, "" + GameControllerTeenPatti.BootAmount);
                dic.Add (TagsTeenpatti.PORT_LIMIT, "" + GameControllerTeenPatti.PortLimit);
                dic.Add (TagsTeenpatti.MAX_BET_AMOUNT, "" + GameControllerTeenPatti.MaxBetAmt);
                dic.Add (TagsTeenpatti.DOMAIN, "" + WebManager.Instance.getURL());
                dic.Add (TagsTeenpatti.MAX_BLIND, "4");
                WarpClient.GetInstance ().CreateRoom ("teenPatti", "game", 6, dic);

            }
        }

    }

    public void onLockPropertiesDone (byte result) {
        //Twist.AppController.showToast ("onLockPropertiesDone");
    }

    public void onUnlockPropertiesDone (byte result) {
        //Twist.AppController.showToast ("onUnlockPropertiesDone");	
    }

    public void onLeaveRoomDone (RoomEvent eventObj) {

    }

    public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj) {
        if (eventObj.getResult () == 0) {

            if (GameControllerTeenPatti.PrivateGameType.Equals (TagsTeenpatti.JOIN_PRIVATE_TABLE)) {
                Dictionary<string, object> dic = eventObj.getProperties ();
                string boolAmount = "" + dic[TagsTeenpatti.MAX_BET_AMOUNT];
                // Debug.Log ("boolAmount " + boolAmount);
                PlayerPrefs.SetString ("InHand", boolAmount);
                appwrapTeenpatti.gameRequest ();
            }

            try {

                int playersName = eventObj.getJoinedUsers ().Length;
                // Debug.Log ("onGetLiveRoomInfoDone " + playersName);
                GameDelegateTeenPatti.ShowTotalGameUser (playersName);
            } catch (System.Exception ex) {
                // Debug.Log ("Working new");
            }
            //			foreach (String n in playersName) {
            //				print ("player name  is " + n);
            //				GameController.newPlayer (n);
            //			}

        }

    }

    public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj) {
        //Twist.AppController.showToast ("onSetCustomRoomDataDone");
    }

    public void onUpdatePropertyDone (LiveRoomInfoEvent eventObj) {
        //Twist.AppController.showToast ("onUpdatePropertyDone");

        if (WarpResponseResultCode.SUCCESS == eventObj.getResult ()) {

        } else {

        }
    }

    public void onSendChatDone (byte result) {
        //Twist.AppController.showToast ("onSendChatDone");

    }

    public void onSendPrivateChatDone (byte result) {
        //Twist.AppController.showToast ("onSendPrivateChatDone");
    }

    public void onSendUpdateDone (byte result) {
        //Twist.AppController.showToast ("onSendUpdateDone");
    }

    public void onRoomCreated (RoomData eventObj) {
        //Twist.AppController.showToast ("onRoomCreated");
    }

    public void onRoomDestroyed (RoomData eventObj) {
        //Twist.AppController.showToast ("onRoomDestroyed");
    }

    public void onUserLeftRoom (RoomData eventObj, string username) {

        //GameController.Message = username+" has removed.";
        //GameController.removePlayer(username);

    }

    public void onUserJoinedRoom (RoomData eventObj, string username) {
        /*GameController.showToast ("A new user has join room.");		
		string name = PlayerPrefs.GetString ("name");
				if (name ==username) {
					print ("player name and opponent name is same ");
					
				} else {
				//	GameController.newPlayer (username);
		
				}*/
    }

    public void onUserLeftLobby (LobbyData eventObj, string username) {

    }

    public void onUserJoinedLobby (LobbyData eventObj, string username) {
        //		string name = PlayerPrefs.GetString ("name");
        //		if (name ==username) {
        //			print ("player name and opponent name is same ");
        //		} else {
        //			GameController.newPlayer (username);
        //
        //		}

        //Twist.AppController.showToast ("onUserJoinedLobby");
    }

    public void onUserChangeRoomProperty (RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable) {
        //Twist.AppController.showToast ("onUserChangeRoomProperty");
    }

    public void onPrivateChatReceived (string sender, string message) {

    }

    public void onMoveCompleted (MoveEvent move) {
        //Twist.AppController.showToast ("onMoveCompleted");	
    }

    public void onChatReceived (ChatEvent eventObj) {

        // // Debug.Log(eventObj.getSender() + "  : message   " + eventObj.getMessage());
        //string player_name = PlayerPrefs.GetString ("name");	
        //if (!eventObj.getSender ().Equals (player_name)) {
        GameDelegateTeenPatti.chatRecived (eventObj.getSender (), eventObj.getMessage ());
        //}
    }

    public void onUpdatePeersReceived (UpdateEvent eventObj) {

    }

    public void onUserChangeRoomProperty (RoomData roomData, string sender, Dictionary<String, System.Object> properties) {
        //Twist.AppController.showToast ("onUserChangeRoomProperty");

    }

    public void onUserPaused (string a, bool b, string c) {
        // Debug.Log (a + "Pause User " + c + " " + b);
        GameDelegateTeenPatti.connctionChange (c, false);
    }

    public void onUserResumed (string a, bool b, string c) {
        GameDelegateTeenPatti.connctionChange (c, true);
        // Debug.Log (a + "Resume User " + c + " " + b);
        if (c.Equals (GameUser.CurrentUser.ID)) {
            // appwarp.sendGameRequest();
        }
    }

    public void onGameStarted (string a, string b, string c) {
        //Twist.AppController.showToast ("onGameStarted");
    }

    public void onGameStopped (string a, string b) {
        //Twist.AppController.showToast ("onGameStopped");
    }

    public void onInvokeZoneRPCDone (RPCEvent evnt) {
        //Twist.AppController.showToast ("onInvokeZoneRPCDone");
    }

    public void onInvokeRoomRPCDone (RPCEvent evnt) {
        //Twist.AppController.showToast ("onInvokeRoomRPCDone");
    }

    public void sendMsg (string msg) {
        //Twist.AppController.showToast ("sendMsg");
        WarpClient.GetInstance ().SendChat (msg);

    }

    public void sendBytes (byte[] msg, bool useUDP) {

        if (useUDP == true)
            WarpClient.GetInstance ().SendUDPUpdatePeers (msg);
        else
            WarpClient.GetInstance ().SendUpdatePeers (msg);

    }

}