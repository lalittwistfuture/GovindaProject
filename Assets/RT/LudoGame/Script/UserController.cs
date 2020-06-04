using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LudoGameTemplate
{
    public class UserController : MonoBehaviour
    {
        private static UserController _instance;
        public string ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Coin { get; set; }

        public string PromoCoin { get; set; }
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
        public static UserController getInstance
        {
            get { return _instance; }
        }
    }
}