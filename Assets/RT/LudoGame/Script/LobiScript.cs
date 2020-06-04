using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using Firebase;
namespace LudoGameTemplate
{
    public class LobiScript : MonoBehaviour
    {

        public GameObject CreatePrivateTablePanel;
        public GameObject QuitApplication;
        public GameObject SettingPanel;
        public GameObject ErrorMsgPanel;
        public GameObject ClosePanel;
        private IDictionary<string, string> dict;

        public void showSetting()
        {
            SettingPanel.SetActive(true);
        }

        void Start()
        {

            CreatePrivateTablePanel.SetActive(false);
            QuitApplication.SetActive(false);
            SettingPanel.SetActive(false);
            ErrorMsgPanel.SetActive(false);
            ClosePanel.SetActive(false);

            GetUserID();

            SecurePlayerPrefs.SetString(GameTags.FACEBOOK_FRIEND, "");
            SecurePlayerPrefs.SetString(GameTags.CREATE_TABLE, "");
            SecurePlayerPrefs.SetString(GameTags.CHALLENGE_FRIEND, "");
            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, "");
            SecurePlayerPrefs.SetString(GameTags.OFFLINE, "");
            SecurePlayerPrefs.SetString(GameTags.CHALLENGE_FRIEND, "");
            SecurePlayerPrefs.SetString(GetPlayerDetailsTags.ROOM_ID, "");
            GameController.Message = "";
            GameController.Message1 = "";

            SecurePlayerPrefs.SetInt(GameTime.LUDO_GAME, GameTime.ON);
            SecurePlayerPrefs.SetInt(GameTime.LUDO_GOLD, GameTime.ON);

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HidePopUp();
                if (CreatePrivateTablePanel.activeSelf)
                {
                    SecurePlayerPrefs.SetString(GameTags.GAME_TYPE, "");
                    CreatePrivateTablePanel.SetActive(false);
                }
                else if (SettingPanel.activeSelf)
                {
                    SettingPanel.SetActive(false);
                }
                else if (ErrorMsgPanel.activeSelf)
                {
                    ErrorMsgPanel.SetActive(false);
                }
                else
                {
                    QuitApplication.SetActive(true);
                }

            }
        }

        void HidePopUp()
        {
            GameObject pop = GameObject.FindGameObjectWithTag("PopUp");
            if (pop)
                pop.SetActive(false);
        }


        public void one_on_one()
        {
            /*// Debug.Log ("one_on_one");



            */

            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, "ghjghg");
            GameConstantData.GameType = GameConstantData.OneToOne;
            GameConstantData.UserLimit = 2;
            GameConstantData.TokenLimit = 4;
            SecurePlayerPrefs.SetString(GameTags.GAME_TYPE, GameTags.PUBLIC);
            //SecurePlayerPrefs.SetString (GameTags.PRIVATE_TABLE_TYPE, GameTags.CREATE_TABLE);

            CreatePrivateTablePanel.SetActive(true);
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().ShowPopup("One on one");
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().Earning = false;

            //		SelectAmountPanel.SetActive (true);
            //		SelectAmountPanel.GetComponent<SelectAmoutPanel> ().Showpopup ("One on one");

        }

        public void one_on_Four()
        {
            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, "ghjghg");
            GameConstantData.GameType = GameConstantData.OneToFour;
            GameConstantData.UserLimit = 2;
            GameConstantData.TokenLimit = 2;
            //SecurePlayerPrefs.SetString (GameTags.PRIVATE_TABLE_TYPE, GameTags.CREATE_TABLE);
            SecurePlayerPrefs.SetString(GameTags.GAME_TYPE, GameTags.PUBLIC);

            CreatePrivateTablePanel.SetActive(true);
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().ShowPopup("One on four");
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().Earning = true;
            //OneOnOnePanel.SetActive (true);
            //SceneManager.LoadSceneAsync ("GameScene");
        }

        public void PracticeGame()
        {
            // Debug.Log ("PracticeGame");
            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, "ghjghg");
            GameConstantData.GameType = GameConstantData.Practice;
            GameConstantData.entryFee = 0;
            GameConstantData.winingAmount = 0;
            GameConstantData.TokenLimit = 4;
            GameConstantData.UserLimit = 2;

            SecurePlayerPrefs.SetString(GameTags.OFFLINE, GameTags.OFFLINE);

            //		CreatePrivateTablePanel.SetActive (true);
            //		CreatePrivateTablePanel.GetComponent<CreatePrivateTable> ().ShowPopup ("Practice Game");
            SceneManager.LoadSceneAsync("GameScene");
        }

        public void PlayWithFrnds()
        {
            // Debug.Log ("PlayWithFrnds ");
            //print ("PlayWithFrnds working");

            GameConstantData.GameType = GameConstantData.Private;
            GameConstantData.UserLimit = 2;
            GameConstantData.TokenLimit = 4;
            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, GameTags.CREATE_TABLE);
            SecurePlayerPrefs.SetString(GameTags.GAME_TYPE, GameTags.PRIVATE);
            //		CreatePrivateTablePanel.SetActive (true);
            //		CreatePrivateTablePanel.GetComponent<CreatePrivateTablePanel> ().sh

            CreatePrivateTablePanel.SetActive(true);
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().ShowPopup("Play With Friends");
            CreatePrivateTablePanel.GetComponent<CreatePrivateTable>().Earning = false;

        }

        public void ClosePanelAction()
        {
            ClosePanel.SetActive(false);

        }

        public void closenewpopup()
        {

        }

        public void GetUserID()
        {

        }

        void OnEnable()
        {
            SecurePlayerPrefs.SetInt(GameTime.LUDO_GAME, 0);
            SecurePlayerPrefs.SetInt(GameTime.LUDO_GOLD, 0);
            SecurePlayerPrefs.SetInt(GameTime.LUDO_PRIME_GAME, 0);

            WWWForm form = new WWWForm();
            form.AddField("TAG", "TIME");
            WebManager.Instance.StartRequest(form, ServerRequest, "TIME");
        }

        void OnDisable()
        {

        }

        public void leaveLudoGame()
        {
            SceneManager.LoadSceneAsync("MainLobby");
        }

        public void ShareAction()
        {
            string msg = "your friend invited you to play Ludo First ever real chip ludo game. Download Now: " + SecurePlayerPrefs.GetString(Tags.APP_DOWNLOAD_URL);
            GameConstantData.shareText(msg);
        }
        private void ServerRequest(string response)
        {



            Debug.Log("response" + response);
            try
            {
                JSONNode node = JSON.Parse(response);

                if (node != null)
                {
                    string result = node["status"];

                    if (result.Equals("OK"))
                    {

                        try
                        {
                            JSONNode data1 = node["data"];
                            SecurePlayerPrefs.SetInt(GameTime.LUDO_GAME, int.Parse("" + data1["LUDO_GAME"]));
                            SecurePlayerPrefs.SetInt(GameTime.LUDO_GOLD, int.Parse("" + data1["LUDO_GOLD"]));
                            SecurePlayerPrefs.SetInt(GameTime.LUDO_PRIME_GAME, int.Parse("" + data1["LUDO_PRIME_GAME"]));


                            SecurePlayerPrefs.SetString(GameTime.LUDO_TEXT, "" + data1["LUDO_TEXT"]);
                            SecurePlayerPrefs.SetString(GameTime.LUDO_GOLD_TEXT, "" + data1["LUDO_GOLD_TEXT"]);
                            SecurePlayerPrefs.SetString(GameTime.LUDO_PRIME_TEXT, "" + data1["LUDO_PRIME_TEXT"]);


                        }
                        catch
                        {
                            Debug.Log("Message");
                        }
                    }


                }
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }


        }

    }

    public class GameTime
    {
        public const string OFFLINE_TEXT = "OFFLINE_TEXT";
        public const string ONLINE1ON4 = "ONLINE1ON4";
        public const string ONLINE1ON1 = "ONLINE1ON1";
        public const string FRIENDS = "FRIENDS";
        public const string LUDO_GAME = "LUDO_GAME";
        public const string LUDO_GOLD = "LUDO_GOLD";
        public const string LUDO_TEXT = "LUDO_TEXT";
        public const string LUDO_GOLD_TEXT = "LUDO_GOLD_TEXT";
        public const string LUDO_PRIME_TEXT = "LUDO_PRIME_TEXT";
        public const string LUDO_PRIME_GAME = "LUDO_PRIME_GAME";
        public const int ON = 1;
        public const int OFF = 0;
    }
}