using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuTeenPattti : MonoBehaviour
{
	// Use this for initialization
	public GameObject ClosePanel;
	public GameObject QuitePanel; 
	public static bool MoveToMainLobby = false;
	void Start ()
	{
		GameMenuTeenPattti.MoveToMainLobby = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnEnable ()
	{

		ClosePanel.SetActive (true);
	}

	public void StandUpMode ()
	{
		appwrapTeenpatti.SendStandUp ();
		ClosePanel.SetActive (false);
		transform.gameObject.SetActive (false);
	}

	public void MoveLobby ()
	{
		GameMenuTeenPattti.MoveToMainLobby = false;
		QuitePanel.SetActive (true);
		ClosePanel.SetActive (false);
		transform.gameObject.SetActive (false);

		//SceneManager.LoadSceneAsync ("Lobby_Teenpatti");
	}

	public void MoveMainLobby ()
	{
		GameMenuTeenPattti.MoveToMainLobby = true;
		QuitePanel.SetActive (true);
		ClosePanel.SetActive (false);
		transform.gameObject.SetActive (false);
		//SceneManager.LoadSceneAsync ("MainLobby");
	}

}
