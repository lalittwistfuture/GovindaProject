using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using SimpleJSON;
using UnityEngine.UI;



public class JoinPrivateTableTeenpatti : MonoBehaviour
{

	public InputField TableCode;
	public Text JoinTableTitle;
	public GameObject QuitGamePanel;
	// Use this for initialization
	void Start ()
	{
		//QuitGamePanel.SetActive (false);
		JoinTableTitle.text = MessageScriptTeenPatti.JOIN_TABLE_TITLE;
	}

	// Update is called once per frame
	void Update ()
	{

	}


	public void ClosePanel ()
	{
		QuitGamePanel.SetActive (true);
		//transform.gameObject.SetActive (false);
		//SceneManager.LoadSceneAsync ("Lobby_Teenpatti");
	}

	public void JoinGame ()
	{
		if (ValidateField ()) {
			
			if (WarpClient.GetInstance () != null) {
				WarpClient.GetInstance ().JoinRoom (TableCode.text);
				transform.gameObject.SetActive (false);
			}
		}
	}

	private bool ValidateField ()
	{

		if (TableCode.text.Length >= 1) {

			// Debug.Log ("Code " + TableCode.text);
		} else {

			GameControllerTeenPatti.showToast ("Please enter table code to join game?");
			return false;

		}

		return true;
	}
}
