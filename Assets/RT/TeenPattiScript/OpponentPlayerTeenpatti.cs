
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class OpponentPlayerTeenpatti : BasePlayerTeenpatti
{
   
	// Use this for initialization
	void Start ()
	{

		base.Start ();

       

		try {
			player_coin.GetComponent<Text> ().text = "0";
		} catch (System.Exception ex) {
			// Debug.Log ("opponent start Exception " + ex.Message);
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate ()
	{
		base.FixedUpdate ();
	}

	public override void myTrun (int amount)
	{

	}

	public override void UserActive (string msg)
	{
		//PlayerStatus.SetActive (true);
		try {
			//Color c = player_Image.GetComponent<Image> ().color;
			//c.a = 1.0f;
            player_Image.GetComponent<Image> ().color = Color.white;
			//Color c1 = player_Image.GetComponent<Image> ().color;
			//c1.a = 1.0f;
			//player_coinHolder.GetComponent<Image> ().color = c1;
           // PlayerStatus.GetComponent<Text> ().text = msg;
			player_coin.GetComponent<Text> ().text = "";
		} catch (System.Exception ex) {
			// Debug.Log ("UserActive " + ex.Message);
		}
	}

	public override void UserInactive (string msg)
	{
		//PlayerStatus.SetActive (true);
		try {
			//Color c = player_Image.GetComponent<Image> ().color;
			//c.a = 0.5f;
            player_Image.GetComponent<Image> ().color = Color.gray;
			//Color c1 = player_Image.GetComponent<Image> ().color;
			//c1.a = 0.5f;
			//base.clearCards ();
			//PlayerStatus.GetComponent<Text> ().text = msg;
			player_coin.GetComponent<Text> ().text = "";
		} catch (System.Exception ex) {
			// Debug.Log ("UserActive " + ex.Message);
		}

	}

	public override void UserLeft ()
	{
        //// Debug.Log ("UserLeft "+base.name);
        base.player_id = "";
        transform.gameObject.SetActive (false);

	}

	public override void UpdateCoin (float TotalBetAmt)
	{
		try {
            
            //GameObject CoinStrip1 = Instantiate(base.player_coinHolder);
            //CoinStrip1.transform.SetParent(player_coinHolder.transform.parent);
            //CoinStrip1.transform.localPosition = Total_pos.position;
            //CoinStrip1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //CoinStrip1.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + coin;
            //iTween.MoveTo(CoinStrip1, player_coinHolder.transform.position, 0.5f);
            //GameDelegateTeenPatti.ShowAddTotalAmount(coin);
            //Destroy(CoinStrip1, 0.5f);
			player_coin.GetComponent<Text> ().text = "" + (TotalBetAmt).ToString("F2");
		} catch (System.Exception ex) {
			// Debug.Log ("UpdateCoin " + ex.Message);
		}

	}

	public override void ResetGame ()
	{
		//// Debug.Log ("ResetGame opponent ");
		this.Betcoin = 0;
		player_coin.GetComponent<Text> ().text = "" + this.Betcoin;

		//PlayerStatus.SetActive(false);
		//try {
		//	PlayerStatus.GetComponent<Text> ().text = PlayerTagsTeenPatti.BLIND;
		//} catch (System.Exception ex) {
		//	// Debug.Log (ex.Message);
		//}

		try {

			foreach (GameObject cc in cards) {
				cc.SetActive (false);
			}

			Color c = new Color (255.0f, 255.0f, 255.0f, 1.0f);
			player_Image.GetComponent<Image> ().color = c;
			StopTimer ();
//			for (int i = 0; i < cards.Length; i++) {
//				GameObject card = cards [i];
//				card.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/backcard");
//			}
		} catch (System.Exception ex) {
			// Debug.Log ("ResetGame Exception " + ex.Message);
		}
	}



}


