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
namespace Roullet {
    public class Roulette_ListnerClass : MonoBehaviour, ConnectionRequestListener, LobbyRequestListener, ZoneRequestListener, RoomRequestListener, ChatRequestListener, UpdateRequestListener, NotifyListener {

        void Start () {
           // StartCoroutine (ConnectionRecover ());
        }
        IEnumerator ConnectionRecover () {
            while (true) {
                if (WarpClient.GetInstance ().GetConnectionState () > 0) {
                    WarpClient.GetInstance ().RecoverConnection ();
                }
                yield return new WaitForSeconds (5.0f);
            }
        }

        public void onConnectDone (ConnectEvent eventObj) {
            // Debug.Log ("Connection " + eventObj.getResult ());
            switch (eventObj.getResult ()) {
                case WarpResponseResultCode.SUCCESS:
                    {

                        RouletteDelegate.onErrorOccure (ErrorType.RecoverConnection);
                        WarpClient.GetInstance ().JoinRoomInRange (0, 4, true);
                    }
                    break;
                case WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE:
                    {
                       
                        RouletteDelegate.onErrorOccure (ErrorType.ConnectionTempraryError);
                        WarpClient.GetInstance ().RecoverConnection ();
                    }
                    break;
                case WarpResponseResultCode.SUCCESS_RECOVERED:
                    {
                        RouletteDelegate.onErrorOccure (ErrorType.RecoverConnection);
                    }
                    break;
                case WarpResponseResultCode.CONNECTION_ERR:
                    {
                        RouletteDelegate.onErrorOccure (ErrorType.ConnectionNotFound);
                    }
                    break;
                case WarpResponseResultCode.AUTH_ERROR:
                    {
                        RouletteDelegate.onErrorOccure (ErrorType.ConnectionNotFound);
                    }
                    break;
                case WarpResponseResultCode.BAD_REQUEST:
                    {
                        RouletteDelegate.onErrorOccure (ErrorType.ConnectionNotFound);
                    }
                    break;

                default:

                    break;
            }

        }

        IEnumerator recover () {
            yield return new WaitForSeconds (4);
            if (WarpClient.GetInstance () != null) {
                WarpClient.GetInstance ().RecoverConnection ();
            }

        }

        public void onInitUDPDone (byte res) {

        }

        public void onLog (String message) {

        }

        public void onDisconnectDone (ConnectEvent eventObj) {
            // Debug.Log ("disconnect " + eventObj.getResult ());
        }

        public void onJoinLobbyDone (LobbyEvent eventObj) {

            if (eventObj.getResult () == 0) {
                WarpClient.GetInstance ().SubscribeLobby ();

            }
        }

        public void onLeaveLobbyDone (LobbyEvent eventObj) { }

        public void onSubscribeLobbyDone (LobbyEvent eventObj) { }

        public void onUnSubscribeLobbyDone (LobbyEvent eventObj) {

        }

        public void onGetLiveLobbyInfoDone (LiveRoomInfoEvent eventObj) {

        }

        public void onDeleteRoomDone (RoomEvent eventObj) {

        }

        public void onGetAllRoomsDone (AllRoomsEvent eventObj) { }

        public void onCreateRoomDone (RoomEvent eventObj) {
            if (eventObj.getResult () == 0) {
                if (WarpClient.GetInstance () != null) {
                    // Debug.Log ("Created");
                    WarpClient.GetInstance ().JoinRoom (eventObj.getData ().getId ());
                }
            }
        }

        public void onGetOnlineUsersDone (AllUsersEvent eventObj) { }

        public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj) {

        }

        public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj) {

        }

        public void onGetMatchedRoomsDone (MatchedRoomsEvent eventObj) {

        }

        public void onSubscribeRoomDone (RoomEvent eventObj) {
            // Debug.Log ("Subscribe Room" + eventObj.getResult ());
        }

        public void onUnSubscribeRoomDone (RoomEvent eventObj) {

        }
        public void onJoinRoomDone (RoomEvent eventObj) {

            if (eventObj.getResult () == 0) {
                if (WarpClient.GetInstance () != null) {
                    WarpClient.GetInstance ().SubscribeRoom (eventObj.getData ().getId ());
                    // Debug.Log ("Subscribe");
                }

            } else {
                // Debug.Log ("Join fail");
                if (WarpClient.GetInstance () != null) {
                    WarpClient.GetInstance ().CreateRoom ("rummy", "game", 5, null);
                }
            }
        }

        public void onLockPropertiesDone (byte result) { }

        public void onUnlockPropertiesDone (byte result) { }

        public void onLeaveRoomDone (RoomEvent eventObj) {

        }

        public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj) {

        }

        public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj) {

        }

        public void onUpdatePropertyDone (LiveRoomInfoEvent eventObj) {

            if (WarpResponseResultCode.SUCCESS == eventObj.getResult ()) {

            } else {

            }
        }

        public void onSendChatDone (byte result) { }

        public void onSendPrivateChatDone (byte result) { }

        public void onSendUpdateDone (byte result) { }

        public void onRoomCreated (RoomData eventObj) { }

        public void onRoomDestroyed (RoomData eventObj) { }

        public void onUserLeftRoom (RoomData eventObj, string username) { }

        public void onUserJoinedRoom (RoomData eventObj, string username) { }

        public void onUserLeftLobby (LobbyData eventObj, string username) { }

        public void onUserJoinedLobby (LobbyData eventObj, string username) {

        }

        public void onUserChangeRoomProperty (RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable) { }

        public void onPrivateChatReceived (string sender, string message) { }

        public void onMoveCompleted (MoveEvent move) { }

        public void onChatReceived (ChatEvent eventObj) {
            // Debug.Log(eventObj.getSender() + "  :  " + eventObj.getMessage());
            if (WarpClient.GetInstance () != null) {
                RouletteDelegate.chatRecived (eventObj.getSender (), eventObj.getMessage ());
            }
        }

        public void onUpdatePeersReceived (UpdateEvent eventObj) { }

        public void onUserChangeRoomProperty (RoomData roomData, string sender, Dictionary<String, System.Object> properties) { }

        public void onUserPaused (string a, bool b, string c) { }

        public void onUserResumed (string a, bool b, string c) { }

        public void onGameStarted (string a, string b, string c) { }

        public void onGameStopped (string a, string b) { }

        public void onInvokeZoneRPCDone (RPCEvent evnt) { }

        public void onInvokeRoomRPCDone (RPCEvent evnt) { }

        public void sendMsg (string msg) {
           // WarpClient.GetInstance ().SendChat (msg);

        }

        public void sendBytes (byte[] msg, bool useUDP) {
            // if (useUDP == true)
            //     if (WarpClient.GetInstance () != null) {
            //         WarpClient.GetInstance ().SendUDPUpdatePeers (msg);
            //     }
            // else if (WarpClient.GetInstance () != null) {
            //     WarpClient.GetInstance ().SendUpdatePeers (msg);
            // }

        }
    }
}