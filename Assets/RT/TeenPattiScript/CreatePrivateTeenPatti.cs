/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatePrivateTeenPatti : MonoBehaviour
{

	public Text TableCode;
	public Text Title;
	public GameObject QuitGamePanel;
	// Use this for initialization
	string CurrentCode = "";

	void Start ()
	{
		Title.text = MessageScriptTeenPatti.CREATE_TABLE_TITLE;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnEnable ()
	{

		GameDelegateTeenPatti.onPrivateTableCode += onPrivateTableCode;
	}

	void OnDisable ()
	{

		GameDelegateTeenPatti.onPrivateTableCode -= onPrivateTableCode;
	}

	void onPrivateTableCode (string code)
	{
		this.CurrentCode = code;
		TableCode.text = this.CurrentCode;
		// Debug.Log ("CurrentCode " + this.CurrentCode);
	}

	public void ClosePanel ()
	{
		QuitGamePanel.SetActive (true);
//		transform.gameObject.SetActive (false);
//		SceneManager.LoadSceneAsync ("Lobby_Teenpatti");
	}

	public void StartGame ()
	{
		transform.gameObject.SetActive (false);
	}

	public void LocalShareCode ()
	{
		string msg = "Your friend " + GameControllerTeenPatti.Player_Display_Name + " have invited you to play teenpatti in a private room . Please enter this code " + this.CurrentCode + " to join your friend.";
		GameControllerTeenPatti.shareText (msg);
	}

	public void FbShareCode ()
	{
		GameControllerTeenPatti.showToast ("working ");
	}
}
*/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class CreatePrivateTeenPatti : MonoBehaviour
{

	public Text TableCode;
	public Text Title;
	public GameObject QuitGamePanel;
	public GameObject StartBtn;
	// Use this for initialization
	string CurrentCode = "";

	void Start ()
	{
		Title.text = MessageScriptTeenPatti.CREATE_TABLE_TITLE;
		StartBtn.GetComponent<Button> ().interactable = false;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnEnable ()
	{

		GameDelegateTeenPatti.onPrivateTableCode += onPrivateTableCode;
		GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
	}

	void OnDisable ()
	{
		GameDelegateTeenPatti.onPrivateTableCode -= onPrivateTableCode;
		GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;

	}

	void onPrivateTableCode (string code)
	{
		this.CurrentCode = code;
		TableCode.text = this.CurrentCode;
		// Debug.Log ("CurrentCode " + this.CurrentCode);
	}

	public void ClosePanel ()
	{
		QuitGamePanel.SetActive (true);
		//		transform.gameObject.SetActive (false);
		//		SceneManager.LoadSceneAsync ("Lobby_Teenpatti");
	}

	public void StartGame ()
	{

		appwrapTeenpatti.StartPrivateGame ();
		transform.gameObject.SetActive (false);
	}

	public void LocalShareCode ()
	{
		string msg = "Your friend " + PlayerPrefs.GetString (PlayerDetails.Name) + " have invited you to play teenpatti in a private room . Please enter this code " + this.CurrentCode + " to join your friend.";
		// Debug.Log("msg = "+msg);
		GameControllerTeenPatti.shareText (msg);
	}



	void onRecivedMassage (string sender, string msg)
	{
        try
        {
            JSONNode s = JSON.Parse(msg);
            //// Debug.Log ("onRecivedMassage Total "+s.ToString());
            switch (s[TagsTeenpatti.TAG])
            {

                case TagsTeenpatti.GAME_REQUEST:
                    {
                        string ss = s[PlayerTagsTeenPatti.DISPLAY_NAME];
                        if (!ss.Equals(PlayerPrefs.GetString(PlayerDetails.Name)))
                        {
                            StartBtn.GetComponent<Button>().interactable = true;
                        }

                    }
                    break;

            }
        }catch(System.Exception es){
            // Debug.Log(es.Message);
        }
	}

}
