using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class gameTypeScript : MonoBehaviour
{


//	public GameObject leftButton;
//	public GameObject rightButton;
	public GameObject MenuBarPanel;
	public GameObject ClosePanel;
	void Start ()
	{
		ClosePanel.SetActive (false);
		MenuBarPanel.SetActive (false);

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadSceneAsync ("MainLobby");
		}
	}

	public void leftAction ()
	{
		//print ("leftAction working");
	}

	public void rightAction ()
	{

		//print ("rightAction working");
	}

	public void PlayNowAction ()
	{

		//print ("PlayNowAction working");
	}

	public void ClosePanelAction(){

		ClosePanel.SetActive (false);
		MenuBarPanel.SetActive (false);

	}

}
}