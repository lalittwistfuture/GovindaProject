using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class ImageController : MonoBehaviour
    {
        private static ImageController _instance;
        public Sprite MissImage;
        public Sprite[] DiceRotation;
        public Sprite[] Dice;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        public static ImageController getInstance
        {
            get { return _instance; }
        }
    }
}