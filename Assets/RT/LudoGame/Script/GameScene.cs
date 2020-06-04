using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace LudoGameTemplate{
public class GameScene : MonoBehaviour {
	private GameObject Coin;
	public GameObject MessageText;
	public GameObject[] OpponentArray;
	public GameObject CreatePrivateTatble;
	public GameObject JoinGamePanel;
	public GameObject QuitGamePanel;
	//	public GameObject Profilepanel;
	public GameObject WinnerPanel;
	public GameObject Container;
	public GameObject cell;

	public GameObject ChatPanel;
	public GameObject EmojiPanel;
	public GameObject ClosePanel;

	public GameObject CurrentPlayer;
	public GameObject WinnerAnimation;
	public GameObject GotiWinAnimation;
	public GameObject CoinValidationPanel;
	public GameObject loadingPanel;
	public GameObject ErrorMsgPanel;
	public GameObject drawGame;
	public appwarp m_appwarp;

	private int TurnMiss = 0;
	//private Text totalBalance;
	int myIndex = -1;

	void Start () {
		// Debug.Log ("total coin = " + UserController.getInstance.Coin);
		//totalBalance.text = "  "+ UserController.getInstance.Coin;
		CoinValidationPanel.SetActive (false);
		Application.runInBackground = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		WinnerAnimation.SetActive (false);
		MessageText.SetActive (false);
		for (int i = 0; i < OpponentArray.Length; i++) {
			OpponentArray[i].GetComponent<OpponentPlayer> ().hideGoti ();
			OpponentArray[i].SetActive (false);
		}
		drawGame.SetActive (false);
		CreatePrivateTatble.SetActive (false);
		JoinGamePanel.SetActive (false);
		QuitGamePanel.SetActive (false);
		WinnerPanel.SetActive (false);
		ErrorMsgPanel.SetActive (false);
		string tableType = SecurePlayerPrefs.GetString (GameTags.PRIVATE_TABLE_TYPE);
		string FACEBOOK_FRIEND = SecurePlayerPrefs.GetString (GameTags.FACEBOOK_FRIEND);
		if (tableType.Equals (GameTags.CREATE_TABLE)) {
			loadingPanel.SetActive (true);
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = "creating room please wait... ";
			CreatePrivateTatble.SetActive (false);
		} else if (tableType.Equals (GameTags.JOIN_TABLE)) {
			JoinGamePanel.SetActive (true);
		} else if (FACEBOOK_FRIEND.Equals (GameTags.FACEBOOK_FRIEND)) {
			loadingPanel.SetActive (true);
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = "Waiting for your friend to join table...";
		} else if (tableType.Equals (GameTags.CHALLENGE_FRIEND)) {
			loadingPanel.SetActive (true);
			// Debug.Log ("CHALLENGE_FRIEND created game scene");
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = "Waiting for your friend to join table...";
		} else if (tableType.Equals (GameTags.FB_FRIEND_ONLINE)) {
			loadingPanel.SetActive (true);
			// Debug.Log ("FB_FRIEND_ONLINE created game scene");
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = "Waiting for your friend to join table...";
		} else {
			loadingPanel.SetActive (true);
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = "Searching opponent please wait...";
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			pressBackButton ();
		}
	}

	void FixedUpdate () {
		//// Debug.Log ("1");
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (GameController.Message1.Length > 1) {
				MessageText.SetActive (true);
				MessageText.GetComponent<Text> ().text = GameController.Message1;
			} else {
				MessageText.SetActive (false);
			}
		} else {
			MessageText.SetActive (true);
			MessageText.GetComponent<Text> ().text = "";
			//MessageText.GetComponent<Text> ().text = "No Internet connection  found!";
		}
	}
	public void ShowJoinTable () {

	}
	void ChallehngeFrnd () {

	}
	public void OnEnable () {
		MessageText.SetActive (false);
		GameDelegate.onRecivedMassage += onRecivedMassage;
		GameDelegate.onSendTableCode += onSendTableCode;

		GameDelegate.onErrorMessage += onErrorMessage;
		GameDelegate.onMoveGoti += onMoveGoti;

	}

	public void OnDisable () {

		GameDelegate.onRecivedMassage -= onRecivedMassage;
		GameDelegate.onSendTableCode += onSendTableCode;

		GameDelegate.onErrorMessage -= onErrorMessage;
		GameDelegate.onMoveGoti -= onMoveGoti;

	}

	void onMoveGoti (int number) {

		GameObject[] GotiArray = GameObject.FindGameObjectsWithTag ("Goti");
		foreach (GameObject btn in GotiArray) {
			btn.transform.localScale = new Vector3 (1f, 1f, 1f);
			if (btn.GetComponent<GotiScript> ().currentPosition != -1) {
				GameObject pos = GameObject.Find ("" + btn.GetComponent<GotiScript> ().CellNumber);
				btn.transform.position = pos.transform.position;
			}
		}
		for (int i = 1; i < 80; i++) {
			int count = 0;
			foreach (GameObject btn in GotiArray) {

				if (btn.GetComponent<GotiScript> ().CellNumber == i) {
					count++;
				}
			}
			if (count > 1) {
				int index = 1;
				foreach (GameObject btn in GotiArray) {
					if (btn.GetComponent<GotiScript> ().currentPosition != -1) {
						if (btn.GetComponent<GotiScript> ().CellNumber == i) {
							btn.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
							GameObject pos = GameObject.Find ("" + i);
							btn.transform.position = pos.transform.position;
							switch (index) {
								case 1:
									{
										btn.transform.position = new Vector3 (btn.transform.position.x - 0.1f, btn.transform.position.y - 0.1f, btn.transform.position.z);
									}
									break;
								case 2:
									{
										btn.transform.position = new Vector3 (btn.transform.position.x + 0.1f, btn.transform.position.y + 0.1f, btn.transform.position.z);
									}
									break;
								case 3:
									{
										btn.transform.position = new Vector3 (btn.transform.position.x - 0.1f, btn.transform.position.y + 0.1f, btn.transform.position.z);
									}
									break;
								case 4:
									{
										btn.transform.position = new Vector3 (btn.transform.position.x + 0.1f, btn.transform.position.y - 0.1f, btn.transform.position.z);
									}
									break;
								case 5:
									{
										btn.transform.position = new Vector3 (btn.transform.position.x, btn.transform.position.y, btn.transform.position.z);
									}
									break;

							}

							index++;

						}
					}
				}
			}
		}

		//		switch (child) {
		//		case 2:
		//			{
		//				
		//
		//			}
		//			break;
		//		}
	}

	public void onErrorMessage (string msg) {

		string playerType = SecurePlayerPrefs.GetString (GameTags.PRIVATE_TABLE_TYPE);
		if (playerType.Equals (GameTags.JOIN_TABLE)) {

		} else {

			loadingPanel.SetActive (true);
			loadingPanel.GetComponent<LoadingPanel> ().msgText.GetComponent<Text> ().text = msg;
		}
	}
	void JoinGameWithRoomID () {
		//GameConstantData.showToast (transform, "JoinGameWithRoomID  " + SecurePlayerPrefs.GetString (GetPlayerDetailsTags.ROOM_ID));	
	}
	void onSendTableCode (string code) {
		string s = SecurePlayerPrefs.GetString (GameTags.PRIVATE_TABLE_TYPE);

		if (s.Equals (GameTags.CREATE_TABLE)) {
			try {
				// Debug.Log ("CREATE_TABLE " + code);
				CreatePrivateTatble.SetActive (true);
				CreatePrivateTatble.GetComponent<PrivateTable> ().TableCode.GetComponent<Text> ().text = code;
				loadingPanel.SetActive (false);
			} catch (System.Exception ex) {
				// Debug.Log ("Exeception occur " + ex.Message);
			}
		} else if (s.Equals (GameTags.CHALLENGE_FRIEND)) {

			RequestToJoinGame (code);

		}
	}

	void RequestToJoinGame (string roomID) {

	}

	GameObject getEmptySeat (int color) {

		if (GameConstantData.UserLimit == 2) {
			return OpponentArray[0];
		}

		//Debug.Log (myIndex + " --------------- " + color);
		if (myIndex > -1) {
			int diff = color - myIndex;
			// Debug.Log ("DIff " + diff);
			if (color > myIndex) {
				switch (diff) {
					case 1:
						{
							// Debug.Log ("Up Play");
							return OpponentArray[2];
						}
					case 2:
						{
							// Debug.Log ("Digonal Play");
							return OpponentArray[0];
						}
					case 3:
						{
							// Debug.Log ("Bottom Play");
							return OpponentArray[1];
						}

				}
			} else {
				switch (diff) {
					case -1:
						{
							// Debug.Log ("Up Play");
							return OpponentArray[1];
						}
					case -2:
						{
							// Debug.Log ("Digonal Play");
							return OpponentArray[0];
						}
					case -3:
						{
							// Debug.Log ("Bottom Play");
							return OpponentArray[2];
						}
				}
			}
		}
		return null;

		// foreach (GameObject chair in OpponentArray) {
		// 	if (chair.GetComponent<OpponentPlayer> ().empty) {
		// 		return chair;
		// 	}
		// }
		// return null;
	}

	bool checkUserExist (string name) {
		foreach (GameObject chair in OpponentArray) {
			if (chair.GetComponent<OpponentPlayer> ().userName.text.Equals (name)) {
				return true;
			}
		}
		return false;
	}

	public string UppercaseFirst (string s) {
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}
		char[] a = s.ToCharArray ();
		a[0] = char.ToUpper (a[0]);
		return new string (a);
	}

	void TakeSeat (JSONNode data) {
		try {
			myIndex = int.Parse (data["COLOR"]);
			//Debug.Log ("My Color " + myIndex);
			GameObject me = GameObject.FindGameObjectWithTag ("current");
			me.GetComponent<PlayerScript> ().empty = false;
			me.GetComponent<PlayerScript> ().userName.text = UppercaseFirst (data[ServerTags.DISPLAY_NAME]);
			me.GetComponent<PlayerScript> ().Player_ID = "" + data[ServerTags.PLAYER_ID];
			me.GetComponent<PlayerScript> ().updateData ();
		} catch (System.Exception ex) {
			// Debug.Log ("Exception in roomInfo " + ex.Message);
		}
	}

	void onRecivedMassage (string sender, string msg) {
		// try {

		JSONNode s = JSON.Parse (msg);

		switch (s[ServerTags.TAG]) {

			case ServerTags.ROOM_INFO:
				{
					JSONNode pl = s[ServerTags.ROOM_DATA];
					string player = UserController.getInstance.ID;
					for (int i = 0; i < pl.Count; i++) {
						JSONNode data = pl[i];
						if (player.Equals (data[ServerTags.PLAYER_ID])) {
							TakeSeat (data);
						}
					}
					for (int i = 0; i < pl.Count; i++) {
						JSONNode data = pl[i];
						if (!player.Equals (data[ServerTags.PLAYER_ID])) {
							if (!checkUserExist (data[ServerTags.PLAYER_ID])) {
								GameObject chair = getEmptySeat (int.Parse (data["COLOR"]));
								if (chair) {
									chair.SetActive (true);
									chair.GetComponent<OpponentPlayer> ().empty = false;
									chair.GetComponent<OpponentPlayer> ().userName.text = "" + UppercaseFirst (data[ServerTags.DISPLAY_NAME]);
									chair.GetComponent<OpponentPlayer> ().total_match = int.Parse (data[DeviceTags.TOTAL_MATCH]);
									chair.GetComponent<OpponentPlayer> ().won_match = int.Parse (data[DeviceTags.WON_MATCH]);
									chair.GetComponent<OpponentPlayer> ().PlayerImageUrl = "" + data[DeviceTags.PIC];
									chair.GetComponent<OpponentPlayer> ().DisPlayname = "" + data[DeviceTags.DISPLAY_NAME];
									chair.GetComponent<OpponentPlayer> ().Player_ID = "" + data[ServerTags.PLAYER_ID];
									chair.GetComponent<OpponentPlayer> ().updateData ();
									chair.GetComponent<OpponentPlayer> ().showGoti ();
									SecurePlayerPrefs.SetString (GetPlayerDetailsTags.OPPONENT_NAME, "" + data[DeviceTags.DISPLAY_NAME]);
									SecurePlayerPrefs.SetString (GetPlayerDetailsTags.OPPONENT_IMAGE, "" + data[DeviceTags.PIC]);
								}
							}
						}
					}
				}
				break;
			case ServerTags.DRAW_GAME:
				{
					drawGame.SetActive (true);
				}
				break;
			case ServerTags.WINNER_PLAYER:
				{

					string player_name = UserController.getInstance.ID;

					JSONNode node = s["RESULT"];
					string Wincoin = s["VALUE"];
					string playerId1 = "";
					// Debug.Log ("result is " + node.Count);
					for (int i = 0; i < node.Count; i++) {
						JSONNode node1 = node[i];
						string playerId = node1["PLAYER_ID"];
						playerId1 = playerId;
						string position = node1["POSITION"];
						string DISPLAY_NAME = node1[ServerTags.DISPLAY_NAME];
						string pic = node1["PIC"];
						WinnerPanel.SetActive (true);
						//print ("playerId " + playerId + " position " + position);
						if (position.Equals ("1")) {
							if (playerId.Equals (player_name)) {
								WinnerPanel.transform.Find ("panel").Find ("WinText").gameObject.SetActive (true);
								WinnerPanel.GetComponent<winnerPanelScript> ().updateName (DISPLAY_NAME, Wincoin, true, pic);
							} else {
								WinnerPanel.transform.Find ("panel").Find ("WinText").gameObject.SetActive (false);
								WinnerPanel.GetComponent<winnerPanelScript> ().updateName (DISPLAY_NAME, Wincoin, false, pic);
							}

						} else {
							GameObject newCell = Instantiate (cell);
							newCell.transform.SetParent (Container.transform);
							newCell.transform.localScale = new Vector3 (1, 1, 1);
							newCell.GetComponent<LooserCell> ().updateLoserCell (DISPLAY_NAME, position);
						}
					}
					if (player_name.Equals (playerId1)) {
						GameDelegate.StartClappingSound ();
						WinnerAnimation.SetActive (true);
						WinnerAnimation.GetComponent<ParticleSystem> ().Clear ();
						WinnerAnimation.GetComponent<ParticleSystem> ().Play ();
					}
				}
				break;
			case ServerTags.GOTI_WIN:
				{
					string player = UserController.getInstance.ID;
					if (!player.Equals (s[ServerTags.PLAYER_ID])) {
						GameDelegate.StartClappingSound ();
						GotiWinAnimation.SetActive (true);
						GotiWinAnimation.GetComponent<ParticleSystem> ().Clear ();
						GotiWinAnimation.GetComponent<ParticleSystem> ().Play ();
						StartCoroutine (StopGotiAnimation ());
					}
				}
				break;

			case ServerTags.START_DEAL:
				{
					//print ("START_DEAL working");

					CreatePrivateTatble.SetActive (false);
					JoinGamePanel.SetActive (false);
					loadingPanel.SetActive (false);
					CurrentPlayer.GetComponent<PlayerScript> ().EnableChatBtn ();

					WinnerPanel.SetActive (false);
					WinnerAnimation.SetActive (false);
					WinnerAnimation.GetComponent<ParticleSystem> ().Clear ();

				}
				break;

			case ServerTags.ROOM_PRICE:
				{
					try {
						string player = UserController.getInstance.ID;
						if (player.Equals (s[ServerTags.PLAYER_ID])) {
							int fee = int.Parse (s[ServerTags.VALUES]);
							if (fee > int.Parse (UserController.getInstance.Coin)) {
								CoinValidationPanel.SetActive (true);
							}
						}
					} catch (System.Exception ex) {
						// Debug.Log (ex.Message);
					}
				}
				break;
			case ServerTags.RESET_GOTI:
				{
					for (int i = 1; i < 72; i++) {
						onMoveGoti (i);
					}
				}
				break;
			case ServerTags.TURN_MISS:
				{

					if (int.Parse (s[ServerTags.VALUES]) != 0) {
						string player = UserController.getInstance.ID;
						if (player.Equals (s[ServerTags.PLAYER_ID])) {

							string message = s["ROOM_MESSAGE"];
							GameConstantData.showToast (transform, message);
							CurrentPlayer.GetComponent<PlayerScript> ().MissTurn (s[ServerTags.PLAYER_ID]);
						} else {

							foreach (GameObject chair in OpponentArray) {
								chair.GetComponent<OpponentPlayer> ().MissTurn (s[ServerTags.PLAYER_ID]);
							}
						}
					} else {

					}
				}
				break;

			default:
				break;
		}
		// } catch (System.Exception ex) {
		// 	 Debug.Log (ex.Message);
		// }
	}

	public void LobbyAction () {
		appwarp.Disconnect ();
		SceneManager.LoadSceneAsync ("MainLobby");

	}
	private IEnumerator StopGotiAnimation () {
		yield return new WaitForSeconds (3.0f);
		GotiWinAnimation.SetActive (false);
		GotiWinAnimation.GetComponent<ParticleSystem> ().Clear ();
		GotiWinAnimation.GetComponent<ParticleSystem> ().Stop ();
	}

	private IEnumerator backToLobby () {
		yield return new WaitForSeconds (4.0f);
		SceneManager.LoadSceneAsync ("MainLobby");
	}

	private IEnumerator HideWinnerAnimation () {
		yield return new WaitForSeconds (4.0f);
		WinnerAnimation.SetActive (false);
	}

	public void pressBackButton () {

		if (ChatPanel.activeSelf) {
			ChatPanel.SetActive (false);
			ClosePanel.SetActive (false);
		} else if (EmojiPanel.activeSelf) {
			EmojiPanel.SetActive (false);

		} else if (loadingPanel.activeSelf) {
			EmojiPanel.SetActive (false);
			appwarp.Disconnect ();
			SceneManager.LoadSceneAsync ("MainLobby");

		} else if (WinnerPanel.activeSelf) {
			EmojiPanel.SetActive (false);
			appwarp.Disconnect ();
			SceneManager.LoadSceneAsync ("MainLobby");
		} else {
			QuitGamePanel.SetActive (true);
		}
	}

}}