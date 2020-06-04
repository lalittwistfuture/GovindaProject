using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class CoinValdationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void YesAction()
    {

        appwarp.leaveGame();
        transform.gameObject.SetActive(false);
        appwarp.Disconnect();
        SceneManager.LoadSceneAsync("MainLobby");
    }

}
}