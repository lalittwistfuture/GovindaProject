using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System;
namespace LudoGameTemplate{
public class BasePlayer : MonoBehaviour
{

	// Use this for initialization
	public  Text userName;
	public Image Image;
	//public GameObject TimerImage;
	public float time = GameConstantData.countTimer;
	public GameObject PlayerDice;
	private int playerTurn;
	public bool isTimeOn = false;
	public int dicNumber = 0;
	public GameObject[] goti;
	public bool empty = true;
	public LudoColor color = LudoColor.Red;
	public string PlayerIndex;
	private GameObject[] boxes;
    public GameObject[] diceSample;
	public GameObject TimerBg;
	public GameObject TimerText;
	public GameObject showArrow;
    public GameObject[] turnMiss;

	public int total_match = 0;
	public int won_match = 0;
	public int playerCoin = 0;
	public int twoPlayerWon = 0;
	public int fourPlayerWon = 0;

	public string PlayerImageUrl = "";
	public string DisPlayname = "";

	public string Player_ID = "";

	int TotalTime = 30;
	public GameObject ChatBox;
	public GameObject ChatMsg;
	public GameObject Emoji;

	public DateTime PosTime;
	public int lossSecond = 0;
    private int turnMissNo =0;
    public Listener listener;
    float amt = 1.8f;
	//	public GameObject[]  frame;
	//	float framesPerSecond = 10.0f;

    public Text investAmount;
    public Text earnAmount;

    



	void Start ()
	{
        listener = GameObject.Find("Canvas").GetComponent<Listener>();
        resetDices();
        investAmount.text = "";
        earnAmount.text = "";
        PlayerDice.SetActive(false);
        for (int i = 0; i < goti.Length; i++) {
            if (i < GameConstantData.TokenLimit) {
                goti[i].SetActive (true);
            } else {
                goti[i].SetActive (false);
            }
        }
	}

    public void syncronized(string player, int[] data)
    {
        //if (player.Equals(Player_ID))
        //{
        //    if (data[2] == 2)
        //    {
        //        TotalTime = data[0];
        //        if (TotalTime >= 0)
        //        {

        //            if (!TimerBg.activeSelf)
        //                TimerBg.SetActive(true);

        //            if ((TotalTime <= 10) && (TotalTime >= 1))
        //            {
        //                StartClockSound();
        //                TimerText.GetComponent<Text>().color = Color.red;
        //                TimerText.GetComponent<Text>().text = "" + TotalTime;
        //                //GameDelegate.StartTimerSound ();
        //            }
        //            else
        //            {
        //                TimerText.GetComponent<Text>().color = Color.black;
        //                TimerText.GetComponent<Text>().text = "" + TotalTime;
        //            }

        //        }else{
        //            TimerText.GetComponent<Text>().text = "";
        //            if (TimerBg.activeSelf)
        //            TimerBg.SetActive(false);  
        //        }
        //    }
        //}
    }


	void StartTimer ()
	{
        
		TotalTime -= lossSecond;
		lossSecond = 0;
		if (TotalTime >= 0) {

            if(!TimerBg.activeSelf)
			TimerBg.SetActive (true);
            
			if ((TotalTime <= 10) && (TotalTime >= 1)) {
				StartClockSound ();
				TimerText.GetComponent<Text> ().color = Color.red;
				TimerText.GetComponent<Text> ().text = "" + TotalTime;
				//GameDelegate.StartTimerSound ();
			} else {
				TimerText.GetComponent<Text> ().color = Color.white;
				TimerText.GetComponent<Text> ().text = "" + TotalTime;
			}
			TotalTime--;
		}
        //if (Player_ID.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
        //{
        //    try
        //    {
        //        int[] data_f = new int[3];
        //        data_f[0] = TotalTime;
        //        data_f[1] = 0;
        //        data_f[2] = 2;
        //        int data_len = (sizeof(int) * 3) + (SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
        //        byte[] data = new byte[data_len];
        //        System.Buffer.BlockCopy(data_f, 0, data, 0, sizeof(int) * 3);
        //        System.Buffer.BlockCopy(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).ToCharArray(), 0, data, sizeof(int) * 3, SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
        //        listener.sendBytes(data, true);
        //    }catch(System.Exception ex){
        //        // Debug.Log(ex.Message);
        //    }
        //}
	}
    public void MissTurn(string ServerPlayerId){
        if (Player_ID.Equals(ServerPlayerId)){
           turnMiss[turnMissNo].GetComponent<Image> ().sprite = ImageController.getInstance.MissImage;
           turnMissNo++;
            // Debug.Log("turnMiss call");
        }
        
    }

	public void OnApplicationFocus(bool paused) {
		if(!paused) {
			PosTime = System.DateTime.Now;
		} else {
			TimeSpan travelTime = System.DateTime.Now - PosTime;  
			lossSecond = travelTime.Seconds;
			// Debug.Log("travelTime: " + travelTime.Seconds );  
		}
	}

	public void showGoti ()
	{
		for (int i = 0; i < goti.Length; i++) {
			 if (i < GameConstantData.TokenLimit) {
                goti[i].SetActive (true);
            } else {
                goti[i].SetActive (false);
            }
		}
	}

	public void hideGoti ()
	{
		for (int i = 0; i < goti.Length; i++) {
			goti [i].SetActive (false);
			goti [i].GetComponent<GotiScript> ().Panel.SetActive (false);
		}

	}

	public void updateData ()
	{
		showGoti ();
		try {
			
			boxes = GameObject.FindGameObjectsWithTag (PlayerIndex);
			
		} catch (System.Exception ex) {
			// Debug.Log ("updateData Exception " + ex.Message);
		}
		for (int i = 0; i < goti.Length; i++) {
			goti [i].GetComponent<GotiScript> ().gotiColor = this.color;
			goti [i].GetComponent<GotiScript> ().updateData (this.PlayerIndex);
		}
	}



	public void setColor (LudoColor co)
	{
		color = co;
	}

	public LudoColor getColor ()
	{
		return color;
	}

	public void OnEnable ()
	{

		GameDelegate.onRecivedMassage += onRecivedMassage;
		GameDelegate.onNumberSelection += onNumberSelection;
        GameDelegate.onPlayerSyncronized += syncronized;
	}

	public void OnDisable ()
	{
		GameDelegate.onNumberSelection -= onNumberSelection;
		GameDelegate.onRecivedMassage -= onRecivedMassage;
        GameDelegate.onPlayerSyncronized -= syncronized;

	}

	public virtual	void onNumberSelection (int index)
	{

	}

	void onRecivedMassage (string sender, string msg)
	{
        try
        {
            JSONNode s = JSON.Parse(msg);


            switch (s[ServerTags.TAG])
            {

                case ServerTags.TURN:
                    {
                        //// Debug.Log ("Player_ID " + Player_ID);

                        try
                        {
                            string player_name = userName.text;
                            isTimeOn = false;
                            time = 0;
                            GameDelegate.showPlayerTurn(s[ServerTags.PLAYER]);

                            TimerBg.SetActive(false);
                            TimerText.GetComponent<Text>().text = "";
                             PlayerDice.SetActive(false);
                            CancelInvoke("StartTimer");
                            TotalTime = 30;
                            lossSecond = 0;
                            ///TimerImage.GetComponent<Image> ().fillAmount = 0;
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                PlayerDice.SetActive(true);
                                TurnPlayer();
                                isTimeOn = true;
                                //if (Player_ID.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)) || Player_ID.Equals("BOT-COMPUTER"))
                                //{
                                    InvokeRepeating("StartTimer", 0.0f, 1.0f);
                                //}
                            }
                            else
                            {
                                resetDices();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;

                case ServerTags.WINNER_PLAYER:
                    {
                        //string player_name = SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID);

                        //JSONNode node = s["RESULT"];
                        //string Wincoin = s["VALUE"];
                        //// Debug.Log("result is " + node.Count);
                        //JSONNode node1 = node[0];
                        //string playerId = node1["PLAYER_ID"];
                        //if (Player_ID.Equals(playerId))
                        //{
                        //    GameObject ss = Instantiate(GameObject.Find("CoinImage"));
                        //    ss.transform.SetParent(transform);
                        //    ss.transform.localScale = new Vector3(1f, 1f, 1f);
                        //    ss.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                        //    iTween.MoveTo(ss, transform.position, 1.0f);
                        //    Destroy(ss, 1.5f);

                        //}
                    }
                    break;



                case ServerTags.DICE_ROLL:
                    {
                        //string player_name = userName.text;
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                this.dicNumber = int.Parse(s[ServerTags.VALUES]);
                                activeDice();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;

                case ServerTags.FEE_SUBMIT:
                    {
                        //string player_name = userName.text;
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                GameObject ss = Instantiate(GameObject.Find("CoinImage"));

                                ss.transform.SetParent(transform);
                                ss.transform.localScale = new Vector3(1f, 1f, 1f);
                               
                                 iTween.MoveTo(ss,investAmount.transform.position, 0.5f);
                                 Destroy(ss, 1.5f);
                                investAmount.text = "Invest Amount  :" + " " + GameConstantData.entryFee;
                                earnAmount.text = "Earn Amount :" + " " + GameConstantData.entryFee * 1.8;
                                                    
                            }

                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }
                    }
                    break;

                case ServerTags.CHATTING_START:
                    {
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER_NAME]))
                            {
                                ShowChatBox(s[ServerTags.CHAT_MSG]);

                            }
                            else
                            {
                                Hide();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;

                case ServerTags.EMOJI:
                    {
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER_NAME]))
                            {
                                string emoji_number = (s[ServerTags.EMOJI_NUMBER]);
                                ShowEmoji(emoji_number);
                                Emoji.SetActive(true);
                            }
                            else
                            {
                                hideEmoji();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;

                case ServerTags.MOVE_GOTI:
                    {
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                int to = int.Parse(s[ServerTags.TO_POSITION]);
                                int from = int.Parse(s[ServerTags.LAST_POSITION]);
                                int goti_number = int.Parse(s[ServerTags.FROM_POSITION]);
                                moveGoti(from, to, goti_number);
                                if(from == -1){
                                    GameDelegate.openGoti();
                                }
                                if (to == -1)
                                {
                                    GameDelegate.StartCutGotiSound();
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }
                    }
                    break;

                case ServerTags.RESET_GOTI:
                    {
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                int to = int.Parse(s[ServerTags.TO_POSITION]);
                                int goti_number = int.Parse(s[ServerTags.INDEX]);
                                resetGoti(to, goti_number);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;

                case ServerTags.READY_GOTI:
                    {

                        //string player_name = userName.text;
                        try
                        {
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                int position = int.Parse(s[ServerTags.POSITION]);
                                readyGoti(position);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }

                    }
                    break;
                case DeviceTags.DICE_ROLL_DONE_COMPLETE:
                    {
                        try
                        {
                            //string player_name = userName.text;
                            if (Player_ID.Equals(s[ServerTags.PLAYER]))
                            {
                                int to = int.Parse(s["VALUE"]);
                                JSONNode TurnValue = s["STEPS"];
                                diceRollDone(to,TurnValue);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log("DICE_ROLL_DONE Exception " + ex.Message);
                        }

                    }
                    break;

                default:
                    break;
            }
        }catch(System.Exception ex){
            // Debug.Log(ex.Message);
        }
	}

    void resetGoti(int next, int goti_number){
       
            for (int i = 0; i < goti.Length; i++)
            {
                if (goti[i].GetComponent<GotiScript>().index == goti_number)
                {
                    int Object = 0;
                    int nextPos = 0;
                    goti[i].GetComponent<GotiScript>().currentPosition = next;
                    int pos = next;
                    if (pos >= 51)
                    {
                        nextPos = goti[i].GetComponent<GotiScript>().homeEntryPoint + pos - 51;
                        Object = nextPos;
                    }
                    else
                    {
                        nextPos = goti[i].GetComponent<GotiScript>().initialPosition + pos;
                        Object = nextPos > 52 ? nextPos - 52 : nextPos;
                    }

                  
                if (next == -1)
                {
                    goti[i].GetComponent<GotiScript>().CellNumber = next;
                    goti[i].transform.position = goti[i].GetComponent<GotiScript>().InitialPositionVector;
                }
                else
                {
                    goti[i].GetComponent<GotiScript>().CellNumber = Object;
                    GameObject posVector = GameObject.Find("" + Object);
                    goti[i].transform.position = posVector.transform.position;
                }
                    break;
                }
            }
        
    }



	public void moveGoti (int  current, int next,int goti_number)
	{
            if (next >= 0)
            {
                for (int i = 0; i < goti.Length; i++)
                {
                    if (goti[i].GetComponent<GotiScript>().index == goti_number)
                    {
                        if (goti[i].GetComponent<GotiScript>().currentPosition != next)
                        {
                            // Debug.Log("Start Moving Goti");
                            goti[i].GetComponent<GotiScript>().currentPosition = next;
                            if (!goti[i].GetComponent<GotiScript>().isMoving)
                                StartCoroutine(moveGotiSteps(current, next, goti[i]));
                        }
                        break;
                    }
                }
            }
            if (next == -1)
            {
                for (int i = 0; i < goti.Length; i++)
                {
                    if (goti[i].GetComponent<GotiScript>().index == goti_number)
                    {
                        goti[i].GetComponent<GotiScript>().currentPosition = next;
                        GameDelegate.showCloseGoti();
                        goti[i].GetComponent<GotiScript>().CellNumber = -1;
                        goti[i].GetComponent<GotiScript>().currentPosition = -1;
                        goti[i].transform.position = goti[i].GetComponent<GotiScript>().InitialPositionVector;
                        //iTween.MoveTo (goti [i], goti [i].GetComponent<GotiScript> ().InitialPositionVector, 0.2f);
                        GameDelegate.showMoveGoti(-1);
                        break;
                    }
                }

            }
	}

	IEnumerator moveGotiSteps (int current,int next, GameObject goti)
	{
		goti.GetComponent<GotiScript> ().isMoving = true;
		int i = current+1;
		while (i <= goti.GetComponent<GotiScript> ().currentPosition) {
			int Object = 0;
			int nextPos = 0;
			try {
				if (i >= 51) {
                    nextPos = goti.GetComponent<GotiScript> ().homeEntryPoint + i - 51;
					Object = nextPos;
				} else {
					nextPos = goti.GetComponent<GotiScript> ().initialPosition + i;
					Object = nextPos > 52 ? nextPos - 52 : nextPos;
				}
				// Debug.Log("Move Goti "+Object);
				GameObject pos = GameObject.Find ("" + Object);
				goti.GetComponent<GotiScript> ().CellNumber = Object;
				goti.transform.position = pos.transform.position;
                 GameDelegate.showMoveGoti (Object);
				//goti.transform.position = Vector3.SmoothDamp(goti.transform.position,pos.transform.position,ref ss,0.2f);
				//iTween.MoveTo (goti, pos.transform.position, 0.2f);
			} catch (System.Exception ex) {
				// Debug.Log ("moveGotiSteps Exception " + ex.Message);
			}
			i++;
			yield return new WaitForSeconds (0.2f);
		}

		goti.GetComponent<GotiScript> ().isMoving = false;

	}

	public virtual void readyGoti (int position)
	{

	}

	public  void ShowChatBox (string msg)
	{
		ChatBox.SetActive (true);
		ChatMsg.GetComponent<Text> ().text = msg;
		StartCoroutine (HideChat ());
	}

	public  void Hide ()
	{
		ChatBox.SetActive (false);
		ChatMsg.GetComponent<Text> ().text = "";
	}

	IEnumerator HideChat ()
	{
		yield return new WaitForSeconds (3.0f);
		ChatBox.SetActive (false);
	}

	IEnumerator DisableEmoji ()
	{
		yield return new WaitForSeconds (3.0f);
		Emoji.SetActive (false);
	}


	public void ShowEmoji (String Emoji_number)
	{
		Emoji.SetActive (true);
		Emoji.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Emoji/E_"+Emoji_number);
		StartCoroutine (DisableEmoji ());
	}




	public void hideEmoji ()
	{

		Emoji.SetActive (false);
	}


	public virtual void activeDice ()
	{


	}

    public virtual void diceRollDone (int value,JSONNode TurnValue)
	{


	}

	public virtual void TurnPlayer ()
	{

	}

	public virtual void StartClockSound ()
	{

	}

	public void resetDices ()
	{
        for (int i = 0; i < diceSample.Length; i++)
        {
            diceSample[i].SetActive(false);
        }
       
	}



}


}
