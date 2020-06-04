using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LudoGameTemplate
{
    public class NewVersionPopUp : MonoBehaviour
    {
        public string newURL;

        // Use this for initialization
        void Start()
        {

        }

        public void okay()
        {
            // Debug.Log(" new url" + this.newURL);
            Application.OpenURL(newURL);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}