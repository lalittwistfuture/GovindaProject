using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class QuitGameScript : MonoBehaviour
    {

        public GameObject msgText;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void YesAction()
        {

            appwarp.leaveGame();

            appwarp.Disconnect();
            SceneManager.LoadSceneAsync("MainLobby");
            //transform.gameObject.SetActive (false);
        }
        public void NoAction()
        {
            transform.gameObject.SetActive(false);
        }

    }
}
