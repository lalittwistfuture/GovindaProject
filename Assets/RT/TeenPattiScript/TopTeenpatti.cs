using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopTeenpatti : MonoBehaviour {
	public GameObject GameMenu;
	public GameObject ClosePanel;

	public void SoundAction () {
		if (GameControllerTeenPatti.isSoundOn) {
			GameControllerTeenPatti.isSoundOn = false;
		} else {
			GameControllerTeenPatti.isSoundOn = true;
		}
	}

	public void MenuBar () {
		ClosePanel.SetActive (true);
		GameMenu.SetActive (true);
	}

}