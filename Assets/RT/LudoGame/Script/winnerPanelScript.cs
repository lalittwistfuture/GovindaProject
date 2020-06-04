using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class winnerPanelScript : MonoBehaviour
    {

        public GameObject WinnerName;
        public GameObject WinnerCoin;
        public GameObject WinnerImage;

        public GameObject LosserImage;
        //	public GameObject Container;
        //	public GameObject loserCell;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void updateName(string PlayerName, string myCoin, bool isWin, string image)
        {
            // Debug.Log("my coin"+myCoin);
            WinnerName.GetComponent<Text>().text = PlayerName;
            if (image.Length != 0)
            {
                if (image.Contains("AVTAR"))
                {
                    string[] img = image.Split(new string[] { "-" }, System.StringSplitOptions.None);
                    WinnerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Avtar/" + img[1]);
                }
                else
                {
                    StartCoroutine(loadImage(image));
                }
            }
            else
            {
                // Debug.Log ("profilePic null");
                WinnerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/user-default");
            }
            if (GameConstantData.GameType != GameConstantData.Practice)
            {
                WinnerCoin.GetComponent<Text>().text = myCoin;
            }
            if (isWin)
            {
                LosserImage.SetActive(false);
            }
            else
            {
                LosserImage.SetActive(true);
            }

        }
        IEnumerator loadImage(string url)
        {
            // Debug.Log ("loadImage url "+url);
            WWW www = new WWW(url);
            yield return www;
            if (www.error == null)
            {

                // Debug.Log ("loadImage url12775675 "+url);
                WinnerImage.GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
                // Debug.Log (" download done  "+url);
            }
            else
            {
                // Debug.Log ("Error occur while downloading");
                WinnerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/user-default");

            }

        }
        public void LobbyAction()
        {

            SceneManager.LoadSceneAsync("MainLobby");

        }
    }
}