using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class PrivateTable : MonoBehaviour
    {


        public GameObject TableCode;
        public GameObject QuitGamePanel;

        // Use this for initialization
        void Start()
        {

            //		StartBtn.AddComponent<Button> ();
            //		StartBtn.GetComponent<Button> ().onClick.AddListener (StartGame);
            //		CloseBtn.AddComponent<Button> ();
            //		CloseBtn.GetComponent<Button> ().onClick.AddListener (ClosePanel);
            //StartBtn.GetComponent<Button> ().interactable = false;
        }
        // Update is called once per frame
        void Update()
        {

        }

        //void OnApplicationPause (bool pauseStatus)
        //{
        //	// Check the pauseStatus to see if we are in the foreground
        //	// or background
        //	if (!pauseStatus) {
        //		//app resume
        //		if (FB.IsInitialized) {
        //			FB.ActivateApp ();
        //		} else {
        //			//Handle FB.Init
        //			FB.Init (() => {
        //				FB.ActivateApp ();
        //			});
        //		}
        //	}
        //}

        //void Awake ()
        //{
        //	if (FB.IsInitialized) {
        //		FB.ActivateApp ();
        //	} else {
        //		//Handle FB.Init
        //		FB.Init (() => {
        //			FB.ActivateApp ();
        //		});
        //	}
        //}


        public void OnEnable()
        {
            GameDelegate.onPlayerjoinTable += onPlayerjoinTable;
            //GameDelegate.onSendTableCode += onSendTableCode;
        }

        public void OnDisable()
        {
            GameDelegate.onPlayerjoinTable -= onPlayerjoinTable;
            //GameDelegate.onSendTableCode -= onSendTableCode;
        }

        public void ClosePanel()
        {
            //transform.gameObject.SetActive (false);
            //SceneManager.LoadSceneAsync ("LobiScene");
            QuitGamePanel.SetActive(true);
            QuitGamePanel.GetComponent<QuitGameScript>().msgText.GetComponent<Text>().text = "Do you want to drop the Game?";

        }

        public void StartGame()
        {

            //		GameDelegate.showStartPrivateTable ();
            //transform.gameObject.SetActive (false);
        }

        //public void InviteFaceookFrnd ()
        //{

        //	string fbid = SecurePlayerPrefs.GetString (GetPlayerDetailsTags.PLAYER_FBID);
        //	if (fbid.Length > 1) {

        //		//print ("AddFriendAction working " + fbid);
        //		FB.Mobile.AppInvite (
        //			new Uri ("https://fb.me/" + fbid),
        //			new Uri ("https://play.google.com/store/apps/details?id=com.twist.Timepass"),
        //			delegate(IAppInviteResult result) {
        //				// Debug.Log (result.RawResult);
        //			}
        //		);
        //	}
        //}

        public void ShareCode()
        {
            int entryFee = GameConstantData.entryFee;
            string tableCode = TableCode.GetComponent<Text>().text;
            //// Debug.Log ("Entry fee "+entryFee);
            //Your friend "name" have invited you to play Ludo in a private room on Ludo Money. Please enter this code "code" to join your friend.
            //string s = "table joining fee " + entryFee + " Table code " + tableCode
            string s = "Your friend " + UserController.getInstance.Name + " have invited you to play Ludo in Rs." + GameConstantData.entryFee + " private room on Ludo Money. Please enter this code " + tableCode + " to join your friend at " + SecurePlayerPrefs.GetString(Tags.APP_DOWNLOAD_URL);
            GameConstantData.shareText(s);
        }

        void onPlayerjoinTable()
        {
            //StartBtn.GetComponent<Button> ().interactable = true;
        }

        void onSendTableCode(string code)
        {
            TableCode.GetComponent<Text>().text = code;
        }

    }
}
