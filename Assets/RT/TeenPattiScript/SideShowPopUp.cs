using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideShowPopUp : MonoBehaviour
{

	public Text messageText;
	private string anotherPlayerID = "";


	void OnEnable ()
	{

		GameDelegateTeenPatti.onHideSideShowPanel += onHideSideShowPanel;
		//GameDelegateTeenPatti.onPrivateTableCode += onPrivateTableCode;
	}

	void OnDisable ()
	{
		GameDelegateTeenPatti.onHideSideShowPanel -= onHideSideShowPanel;
		//GameDelegateTeenPatti.onPrivateTableCode -= onPrivateTableCode;
	}


	void onHideSideShowPanel(){
		appwrapTeenpatti.sideShowAcceptance (this.anotherPlayerID, false);
		transform.gameObject.SetActive (false);
	}
	public void CancelRequest ()
	{
		
		appwrapTeenpatti.sideShowAcceptance (this.anotherPlayerID, false);
		transform.gameObject.SetActive (false);
	}

	public void AccceptRequest ()
	{
		// Debug.Log ("showData  anotherPlayerId " + this.anotherPlayerID);
		appwrapTeenpatti.sideShowAcceptance (this.anotherPlayerID, true);
		transform.gameObject.SetActive (false);
	}

	public void showData (string message, string FrndID)
	{
		
		this.anotherPlayerID = FrndID;
		messageText.text = message;
		// Debug.Log ("showData  anotherPlayerId " + message);
	}

}
