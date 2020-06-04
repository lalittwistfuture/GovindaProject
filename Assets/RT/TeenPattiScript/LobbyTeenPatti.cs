using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class LobbyTeenPatti : MonoBehaviour
{
	void Start ()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetInt (Game.Type, Game.TeenPatti);
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PressBackBututon ();
		}
	}


	public void PressBackBututon ()
	{
		SceneManager.LoadSceneAsync ("MainLobby");
	}
	
	public void PlayNowAction ()
	{
		GameControllerTeenPatti.isChallenge = false;
		GameControllerTeenPatti.GameType = TagsTeenpatti.PUBLIC;
		GameControllerTeenPatti.PrivateGameType = "";
      	SceneManager.LoadSceneAsync ("AmountSelectionTeenpatti");
		
	}

}
