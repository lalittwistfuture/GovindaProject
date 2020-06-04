using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class ErrorMessagePanel : MonoBehaviour
{

	public GameObject MsgText;
	public GameObject CloseBtn;
	// Use this for initialization
	void Start ()
	{

		//CloseBtn.AddComponent<Button> ();
		CloseBtn.GetComponent<Button> ().onClick.AddListener (ClosePanel);
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (transform.gameObject.activeSelf) {
				transform.gameObject.SetActive (false);
			}
		}
	}

	public void ShowMessage (string msg)
	{
		
		MsgText.GetComponent<Text> ().text = msg;
	}

	public void ClosePanel ()
	{
		
		transform.gameObject.SetActive (false);
	}
}
}