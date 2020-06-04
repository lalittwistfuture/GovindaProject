using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmountSelectionTeenpatti : MonoBehaviour {
	private bool amountSelected = false;
	private Text currentValue;
	// Use this for initialization
	void Start () {
		amountSelected = false;
		//Screen.autorotateToPortrait = true;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	// Update is called once per frame

	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PressBackBututon ();
		}
	}
	public void PressBackBututon () {
		SceneManager.LoadSceneAsync ("TeenpattiLobby");
	}
	public void PlayNow () {
		// Debug.Log ("MaxBetAmt = " + GameControllerTeenPatti.MaxBetAmt + "   " + float.Parse (GameUser.CurrentUser.Coin));
		if (float.Parse (GameUser.CurrentUser.Coin) >= GameControllerTeenPatti.MaxBetAmt) {
			
			SceneManager.LoadScene ("GameScene_Teenpatti");
		} else {
			GameController.showToast ("You do not have sufficient coins to join this table.");
		}
	}

	public void MoveRoller (Transform pos, string value) {
		int count = int.Parse (value);
		float selectedPrice = float.Parse (value);
		GameControllerTeenPatti.MaxBetAmt = count * 64;
		GameControllerTeenPatti.BootAmount = count;
		GameControllerTeenPatti.PortLimit = count * 512;
	}

}