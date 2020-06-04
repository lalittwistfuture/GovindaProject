using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Firebase;
namespace LudoGameTemplate
{
    public class LoginScript : MonoBehaviour
    {

        public InputField userID;
        public Text log;
        public GameObject loading;
        // Use this for initialization

        void Awake()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        void Start()
        {
            UserController.getInstance.ID = GameUser.CurrentUser.ID;
            UserController.getInstance.Name = GameUser.CurrentUser.Name;
            UserController.getInstance.Image = GameUser.CurrentUser.Pic;
            UserController.getInstance.Coin = GameUser.CurrentUser.Coin;
            SecurePlayerPrefs.SetString(GetPlayerDetailsTags.PLAYER_COIN, GameUser.CurrentUser.Coin);
            SceneManager.LoadSceneAsync("LobiScene");
        }

        public void player1Action()
        {
            //userID.text = "5e7cc6fb790ad37fddfb2b03";
            UserController.getInstance.ID = "1";
            UserController.getInstance.Name = "A";
            UserController.getInstance.Image = "Avtar-0";
            UserController.getInstance.Coin = "1000";
            SecurePlayerPrefs.SetString(GetPlayerDetailsTags.PLAYER_COIN, "1000");
            SceneManager.LoadSceneAsync("LobiScene");
        }
        public void player2Action()
        {
            //userID.text = "5e5fe93757e0f24bf131476d";
            UserController.getInstance.ID = "2";
            UserController.getInstance.Name = "B";
            UserController.getInstance.Image = "Avtar-0";
            UserController.getInstance.Coin = "1000";
            SecurePlayerPrefs.SetString(GetPlayerDetailsTags.PLAYER_COIN, "1000");
            SceneManager.LoadSceneAsync("LobiScene");
        }
        public void player3Action()
        {
            //userID.text = "5e610c8b57e0f24bf1314be9";
            /*	UserController.getInstance.ID = "3";
                UserController.getInstance.Name = "C";
                UserController.getInstance.Coin = "1000";
                SecurePlayerPrefs.SetString (GetPlayerDetailsTags.PLAYER_COIN, "1000");
                UserController.getInstance.Image = "Avtar-0";
                SceneManager.LoadSceneAsync ("LobiScene");*/
        }
        public void player4Action()
        {
            //userID.text = "5e5f9dd857e0f24bf13145c3";
            /*UserController.getInstance.ID = "4";
            UserController.getInstance.Coin = "1000";
            UserController.getInstance.Name = "D";
            SecurePlayerPrefs.SetString (GetPlayerDetailsTags.PLAYER_COIN, "1000");
            UserController.getInstance.Image = "Avtar-0";
            SceneManager.LoadSceneAsync ("LobiScene");*/

        }

        public void OnEnable()
        {

        }

        public void OnDisable()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }

        public void EmailBtn()
        {

        }

        public void PasswordBtn()
        {

        }

        public void GoogleLoginAction()
        {

        }


        private IEnumerator ServerRequest(UnityWebRequest www)
        {
            yield return www.SendWebRequest();
            if (www.error == null)
            {
                try
                {
                    string response = www.downloadHandler.text;

                    log.text = response;
                    JSONNode node = JSON.Parse(response);
                    if (node != null)
                    {
                        try
                        {
                            JSONNode player = node["player"];
                            JSONNode wallet = node["playerWallet"];
                            string playerId = player["_id"];
                            string username = player["username"];
                            string avatr = player["avatar_url"];
                            string walletPlayerID = wallet["playerId"];
                            if (walletPlayerID.Equals(playerId))
                            {
                                if (wallet["walletStatus"].AsBool)
                                {
                                    string coin = wallet["currentChipsAmount"];
                                    UserController.getInstance.Coin = coin;
                                }
                                else
                                {
                                    // Debug.Log ("Wallet is not trtue");
                                }
                            }
                            else
                            {
                                // Debug.Log ("WalletID is not correct");
                            }
                            UserController.getInstance.ID = playerId;
                            UserController.getInstance.Name = username;
                            UserController.getInstance.Image = avatr;
                            SceneManager.LoadSceneAsync("LobiScene");

                        }
                        catch (System.Exception ex)
                        {
                            JSONNode error = node["error"];
                            JSONNode context = error["context"];
                            // Debug.Log ("Error : " + context["message"]);
                            log.text = "Error Custom " + context["message"];
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    // Debug.Log (ex.Message);
                    log.text = "Error try Custom " + ex.Message;
                }
            }
            else
            {
                log.text = "Error Null Custom " + www.error;
            }
        }

    }
}