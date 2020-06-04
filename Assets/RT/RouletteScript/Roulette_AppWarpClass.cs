 using System.Collections.Generic;
 using System.Collections;
 using System.Threading;
 using System;
 using com.shephertz.app42.gaming.multiplayer.client.command;
 using com.shephertz.app42.gaming.multiplayer.client.events;
 using com.shephertz.app42.gaming.multiplayer.client.listener;
 using com.shephertz.app42.gaming.multiplayer.client.message;
 using com.shephertz.app42.gaming.multiplayer.client.transformer;
 using com.shephertz.app42.gaming.multiplayer.client;
 using SimpleJSON;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;
 using UnityEngine;
 namespace Roullet {

     public class Roulette_AppWarpClass : MonoBehaviour {

         // connect to local server

        //  private string appKey = "e2383b63-fdc1-4ec5-a";
        //  private string ipAddress = "192.168.1.9";
        //  private int port = 12346;

         // private string appKey = "c19fe67e-3e48-45f3-b";
         // private string ipAddress = "154.16.173.4";
         // private int port = 12344;

         // connect to global server deathwish.in
         // private string appKey = "8342a737-7386-4f81-b";
         // private string ipAddress = "139.59.92.98";
         // private  int port = 12389;

         // connect to global server fossilco.in  8f4f1510-0bcd-458d-9

         private string appKey = "cb0f035a-be85-4039-b";
         private string ipAddress = "18.191.163.19";
         private int port = 12346;

         public static string roomID = "";
         public static string username = "Lalit";
         public int sessionID = 0;

         public bool useUDP = true;
         public Thread serverThread;
         public static int state = 0;
         protected bool sendData = false;
         public static bool InternetConnectivity = true;
         protected string data = "";
         protected Queue sendDataQueue;
         Roulette_ListnerClass listen;
         public static int attempt_server = 0;

         public GameObject QuitGamePanel;

         void Start () {
             Screen.orientation = ScreenOrientation.Landscape;
             Application.targetFrameRate = 30;
             // QuitGamePanel = Instantiate((GameObject)Resources.Load("_prefeb/QuitGame"));
             // QuitGamePanel.transform.SetParent(transform);
             // QuitGamePanel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
             //  QuitGamePanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
             //QuitGamePanel.GetComponent<ErrorPanelScript> ().msgtext.GetComponent<Text> ().text = "Say Hello";
             //  QuitGamePanel.SetActive(false);

             try {
                 if (PlayerPrefs.GetInt ("Sound") == 0) {
                     GetComponent<AudioSource> ().Stop ();
                 }

             } catch (System.Exception ex) {
                 // Debug.Log (ex.Message);
             }

             Connection ();
         }

         void Update () {
             if (Input.GetKeyDown (KeyCode.Escape)) {
                 if (RoulleteManager.Instance.isTable) {
                     QuitGamePanel.GetComponent<LeaveGame> ().QuitGameText.GetComponent<Text> ().text = "Are you sure you want to quit the game. You may also lose coins?";
                     QuitGamePanel.SetActive (true);
                 }
             }

         }

         public void HomeAction () {

             if (RoulleteManager.Instance.isTable) {
                 QuitGamePanel.GetComponent<LeaveGame> ().QuitGameText.GetComponent<Text> ().text = "Are you sure you want to quit the game. You may also lose coins?";
                 QuitGamePanel.SetActive (true);
             }
         }

         public static void add_coin (JSONClass data) {

             Roulette_AppWarpClass.sendChat (data.ToString ());

         }
         public static void removeBet () {
             JSONClass number = new JSONClass ();
             number.Add ("TAG", "REMOVE_COIN");
             Roulette_AppWarpClass.sendChat (number.ToString ());

         }
         public static void unDoBet () {
             JSONClass number = new JSONClass ();
             number.Add ("TAG", "UNDO");
             Roulette_AppWarpClass.sendChat (number.ToString ());
         }

         static void sendChat (string msg) {
             try {
                 if (WarpClient.GetInstance () != null) {
                     if (!msg.Equals ("")) {
                         WarpClient.GetInstance ().SendChat (msg);
                     }
                 }
             } catch (Exception ex) {
                 print (ex);
             }
         }

         public void Connection () {
             string player_name = GameUser.CurrentUser.ID;
             // Debug.Log ("player_name " + player_name);
             WarpClient.initialize (appKey, ipAddress, port);
             WarpClient.setRecoveryAllowance (120);
             listen = GetComponent<Roulette_ListnerClass> ();
             WarpClient.GetInstance ().AddConnectionRequestListener (listen);
             WarpClient.GetInstance ().AddChatRequestListener (listen);
             WarpClient.GetInstance ().AddLobbyRequestListener (listen);
             WarpClient.GetInstance ().AddNotificationListener (listen);
             WarpClient.GetInstance ().AddRoomRequestListener (listen);
             WarpClient.GetInstance ().AddUpdateRequestListener (listen);
             WarpClient.GetInstance ().AddZoneRequestListener (listen);
             WarpClient.GetInstance ().Connect (player_name, "");

         }
         /// <summary>
         /// This function is called when the behaviour becomes disabled or inactive.
         /// </summary>
         void OnDisable () {
             Disconnect ();
         }
         public static void Disconnect () {
             if (WarpClient.GetInstance () != null) {
                 WarpClient.GetInstance ().Disconnect ();

             }
         }
     }
 }