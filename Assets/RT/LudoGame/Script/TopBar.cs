using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class TopBar : MonoBehaviour
    {
        public Image Pic;
        public Text Name;
        public Text Coin;
        public Text PromoCoin;
        void Start() { }
        void Update() { }
      
        void updateUserInfo()
        {
            string name_upper = UppercaseFirst(UserController.getInstance.Name);
            Name.text = name_upper;
            Coin.text = "" + UserController.getInstance.Coin;
            string playerImage = UserController.getInstance.Image;

            if (playerImage.Length != 0)
            {
                // Debug.Log ("Download " + playerImage);
                StartCoroutine(loadImage(playerImage));
            }
            else
            {
                // Debug.Log ("profilePic null");
                Pic.sprite = Resources.Load<Sprite>("images/user-default");

            }
        }
        void OnEnable()
        {
            //UpdateUserCoin ();
            updateUserInfo();
        }

        IEnumerator loadImage(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.error == null)
            {
                // Debug.Log ("Downloading start " + url);
                Pic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
                // Debug.Log ("Downloading complited");
            }
            else
            {
                // Debug.Log ("Error occur while downloading ");
                Pic.sprite = Resources.Load<Sprite>("images/user-default");

            }
        }

        public string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

      

      
       
    }
}