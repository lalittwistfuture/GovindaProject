using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class CreatePrivateTable : MonoBehaviour
    {

        public Text Title;
        public Text midTitle;
        public GameObject BottomPanel;
        public Image[] buttons;
        public Image TableImage;
        public Text Invest;

        public Text Earn;
        int coin = 0;
        public Text ludoText;
        public Text ludoGoldText;
        public Text ludoPrimeText;
        float currentCoin = 0;

        public Sprite OffImage;
        public Sprite OnImage;
        public bool Earning;
        // Use this for initialization
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            Invest.text = "";
            Earn.text = "";

            string GAME_TYPE = SecurePlayerPrefs.GetString(GameTags.GAME_TYPE);
            DeSelectAll();
            closeAllbutton();
            if (GAME_TYPE.Equals(GameTags.PUBLIC))
            {
                ludoText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_TEXT);
                ludoGoldText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_GOLD_TEXT);
                ludoPrimeText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_PRIME_TEXT);
                BottomPanel.SetActive(false);
                midTitle.text = "Play Ludo with Real Players \n*Select Amount and Press Play Now";

                if (SecurePlayerPrefs.GetInt(GameTime.LUDO_GAME) == 1)
                {
                    openbutton("100");
                    openbutton("50");
                    ludoText.text = "OPEN";
                }
                else
                {
                    ludoText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_TEXT);
                }
                if (SecurePlayerPrefs.GetInt(GameTime.LUDO_GOLD) == 1)
                {
                    openbutton("500");
                    openbutton("250");
                    ludoGoldText.text = "OPEN";
                }
                else
                {
                    ludoGoldText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_GOLD_TEXT);
                }
                if (SecurePlayerPrefs.GetInt(GameTime.LUDO_PRIME_GAME) == 1)
                {
                    openbutton("1000");
                    ludoPrimeText.text = "OPEN";
                }
                else
                {
                    ludoPrimeText.text = SecurePlayerPrefs.GetString(GameTime.LUDO_PRIME_TEXT);
                }
            }
            else if (GAME_TYPE.Equals(GameTags.PRIVATE))
            {
                BottomPanel.SetActive(true);
                ludoText.text = "Select Table";
                ludoGoldText.text = "Select Table";
                ludoPrimeText.text = "Select Table";
                midTitle.text = "Play Ludo with Friends \n*Select Amount and Press Play Now";
                openbutton("1000");
                openbutton("500");
                openbutton("250");
                openbutton("100");
                openbutton("50");
                openbutton("20");
                openbutton("10");
            }

        }

        public void ShowPopup(string msg)
        {
            
            currentCoin = float.Parse(UserController.getInstance.Coin);

        }
        public void JoinTableAction()
        {
            GameConstantData.entryFee = coin;
            SecurePlayerPrefs.SetString(GameTags.PRIVATE_TABLE_TYPE, GameTags.JOIN_TABLE);
            SceneManager.LoadSceneAsync("GameScene");
        }
        public void ClosedPanelAction()
        {
            coin = 0;
            transform.gameObject.SetActive(false);
            SecurePlayerPrefs.SetString(GameTags.GAME_TYPE, "");
        }

        public void DeSelectAll()
        {
            foreach (Image obj in buttons)
            {
                obj.sprite = OffImage;
                // Debug.Log ("unselect coin");
            }
        }
        public void selectPrice(Image img)
        {
            DeSelectAll();
            img.sprite = OnImage;
            int value = int.Parse(img.transform.name);
            coin = value;
            Invest.text = "Entry : " + coin;
            //Earn.text = "Earn:"+((coin*2) - (coin*2*0.01*10));
            // Debug.Log ("Selected Image " + img.transform.name);
            if (Earning)
            {
                Earn.text = "Win : " + ((coin * 4) - (coin * 4 * 0.01 * 5));
            }
            else
            {
                Earn.text = "Win : " + ((coin * 2) - (coin * 2 * 0.01 * 5));
            }


        }

        public void play()
        {
            if (coin > 0)
            {
                Debug.Log("USe Coin " + UserController.getInstance.Coin);
                if (coin <= float.Parse(UserController.getInstance.Coin))
                {
                    GameConstantData.entryFee = coin;
                    SceneManager.LoadSceneAsync("GameScene");
                }
                else
                {
                    GameConstantData.showToast(transform, "You do not have sufficient coin.");
                }
            }
            else
            {
                GameConstantData.showToast(transform, "Please select bet amount.");
            }
        }
        private IEnumerator ShowPopup()
        {
            yield return new WaitForSeconds(1.0f);
            transform.gameObject.SetActive(false);
        }

        public void closeAllbutton()
        {
            foreach (Image img in buttons)
            {
                img.transform.parent.GetComponent<Button>().interactable = false;
            }
        }

        public void openbutton(string price)
        {
            foreach (Image img in buttons)
            {
                if (img.transform.name.Equals(price))
                {
                    img.transform.parent.GetComponent<Button>().interactable = true;
                }
            }
        }

    }
}