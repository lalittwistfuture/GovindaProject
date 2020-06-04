using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class PopUpScript : MonoBehaviour
    {

        public Text msgText;

        // Use this for initialization
        void Start()
        {

        }

        public void removePopUp()
        {
            Destroy(transform.gameObject);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}