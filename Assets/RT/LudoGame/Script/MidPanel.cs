using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class MidPanel : MonoBehaviour
    {
        public GameObject LudoPanel;
        public GameObject SnakePanel;
        // public GameObject Coming;

        void Start()
        {

        }


        void Update()
        {

        }
        // void OnEnable(){
        //     LudoPanel.SetActive(false);
        // }
        public void comingSoon()
        {

        }
        public void LudoLobby()
        {
            LudoPanel.SetActive(true);
        }
        public void SnakeLobby()
        {
            SnakePanel.SetActive(true);
        }
    }
}