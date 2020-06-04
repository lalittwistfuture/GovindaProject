using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Roullet
{

    public class RoulletLobby : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backAction();
            }
        }

        public void backAction()
        {
            SceneManager.LoadSceneAsync("MainLobby");
        }

        public void PlanyNow()
        {
            SceneManager.LoadSceneAsync("Roulette_GameScene");
        }
    }
}