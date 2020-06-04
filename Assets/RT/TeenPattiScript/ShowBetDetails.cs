using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class ShowBetDetails : MonoBehaviour
{

    //public Text BetAmt;
    public Text MaxBetAmt;
	public Text PotLimit;
	// Use this for initialization
	void Start ()
	{

		//BetAmt.text = "";
		MaxBetAmt.text = "";
		PotLimit.text = "";

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnEnable ()
	{
		GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
	}

	void OnDisable ()
	{
		GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;
	}

	void onRecivedMassage (string sender, string msg)
	{
        try
        {

            JSONNode s = JSON.Parse(msg);
            //// Debug.Log ("onRecivedMassage " + s.ToString ());
            switch (s[TagsTeenpatti.TAG])
            {
                case ServerTagsTeenpatti.ROOM_INFO:
                    {
                        try
                        {

                            GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                            //BetAmt.text = "Bet Amount: " + s [ServerTagsTeenpatti.BOOT_AMOUNT];
                            MaxBetAmt.text = "" + s[ServerTagsTeenpatti.MAX_BET_AMOUNT];
                            PotLimit.text = "" + s[ServerTagsTeenpatti.POT_AMOUNT];
                            //// Debug.Log (" MAX_BET_AMOUNT " + s [ServerTagsTeenpatti.MAX_BET_AMOUNT]);
                        }
                        catch (System.Exception ex)
                        {

                            // Debug.Log("ROOM_INFO Exception " + ex.Message);

                        }
                    }
                    break;
                case ServerTagsTeenpatti.WINNER_PLAYER:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        //BetAmt.text = "";
                        MaxBetAmt.text = "";
                        PotLimit.text = "";
                    }
                    break;

                default:
                    break;
            }
        }catch(System.Exception ex){
            // Debug.Log(ex.Message);
        }
	}


}
