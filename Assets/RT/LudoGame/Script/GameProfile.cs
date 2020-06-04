using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate{
public class GameProfile : MonoBehaviour
{

	public GameObject playerName;
	public GameObject playerImage;
	public GameObject playerCoin;
	public GameObject GameWin;
	public GameObject WinRate;
	public GameObject twoPlayerWin;
	public GameObject FoourPlayerWin;

	// Use this for initialization
	void Start ()
	{

//		updatePopup ();
	}
	// Update is called once per frame
	void Update ()
	{
		
	}


	public void showData (string json)
	{
		JSONNode node = JSON.Parse (json);
		// Debug.Log ("showData is "+node);
		string name = node ["NAME"];
		string PIC = node ["PIC"];
		string COIN = node ["COIN"];
		int TOTAL_GAME = int.Parse(node ["TOTAL_GAME"]);
		int WON_GAME = int.Parse(node ["WON_GAME"]);
		string TWO_PLAYER_WON = node ["TWO_PLAYER_WON"];
		string FOUR_PLAYER_WON = node ["FOUR_PLAYER_WON"];
		float rate = 0.0f;
		try{
			 rate = WON_GAME / TOTAL_GAME;
		}catch(System.Exception){
			rate = 0;
			// Debug.Log ("divided by zero "+rate);
		}

		playerName.GetComponent<Text> ().text = name;
		playerCoin.GetComponent<Text> ().text = COIN;
		GameWin.GetComponent<Text> ().text = ""+WON_GAME+" of "+TOTAL_GAME;
		WinRate.GetComponent<Text> ().text = "" + rate + "%";
		twoPlayerWin.GetComponent<Text> ().text = ""+twoPlayerWin;
		FoourPlayerWin.GetComponent<Text> ().text = ""+FOUR_PLAYER_WON;
		//string name1=data ["NAME"];

//		playerName.GetComponent<Text> ().text = json [""];
//		playerCoin.GetComponent<Text> ().text = json [GameProfileTags.COIN];
//		string profilePic = json [GameProfileTags.PIC];
//		print ("pic is " + profilePic);

//		if (PIC.Length != 0) {
//			StartCoroutine (loadImage (PIC));
//		} else {
//			// Debug.Log ("profilePic null");
//			playerImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("images/user-default");
//		}

	}




	// update the label
	void updatePopup ()
	{

		playerName.GetComponent<Text> ().text = UserController.getInstance.Name;
		playerCoin.GetComponent<Text> ().text = UserController.getInstance.Coin;
		string profilePic = UserController.getInstance.Image;
		//print ("pic is " + profilePic);
		if (profilePic.Length != 0) {
			StartCoroutine (loadImage (profilePic));
		} else {
			// Debug.Log ("profilePic null");
			playerImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("images/user-default");
		}
	}

	public void CloseAction ()
	{
		transform.gameObject.SetActive (false);
	}

	IEnumerator loadImage (string url)
	{
		WWW www = new WWW (url);
		yield return www;
		if (www.error == null) {
			playerImage.GetComponent<Image> ().sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0, 0));
		} else {
			// Debug.Log ("Error occur while downloading");
			playerImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("images/user-default");
		}
	}


}
}