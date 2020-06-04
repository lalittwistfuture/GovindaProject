using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class ShareRefrelCode : MonoBehaviour
    {

        public Text refrelCode;

        // Use this for initialization
        void Start()
        {
            refrelCode.text = SecurePlayerPrefs.GetString(GetPlayerDetailsTags.REFREL_CODE);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync("MainLobby");

            }
        }

        public void BackbuttonAction()
        {
            SceneManager.LoadSceneAsync("MainLobby");
        }

        public void ShareAction()
        {
            ///string msg = "Enter this code to earn mony " + SecurePlayerPrefs.GetString (GetPlayerDetailsTags.REFREL_CODE);
            /// 
            /// Sumit Kumar invited you to play Ludo Money. Please enter referral code 0975345and get free 50 Rs worth of coins. Please download the game from: 
            string msg = UserController.getInstance.Name + " invited you to play Ludo Money-First ever real money ludo game.Enter referral code '" + SecurePlayerPrefs.GetString(GetPlayerDetailsTags.REFREL_CODE) + "' & get coin worth Rs." + SecurePlayerPrefs.GetString(Tags.REFERRAL_COIN_FOR_OLD_PLAYER) + ". Download Now: " + SecurePlayerPrefs.GetString(Tags.APP_DOWNLOAD_URL);
            GameConstantData.shareText(msg);
        }
    }
}