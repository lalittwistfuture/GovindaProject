using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class QuitApplication : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void yesAction()
        {
            Application.Quit();
        }

        public void noAction()
        {
            gameObject.SetActive(false);

        }
    }
}