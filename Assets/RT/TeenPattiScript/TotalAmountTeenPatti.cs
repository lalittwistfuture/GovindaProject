using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class TotalAmountTeenPatti : MonoBehaviour
{

	public Text TotalCoin;
	int total = 0;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnEnable ()
	{
		//GameDelegateTeenPatti.onCollectBootAmount += UpdateAmount;
		GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
		GameDelegateTeenPatti.onAddTotalAmount += UpdateAmount;
	}

	void OnDisable ()
	{
		//GameDelegateTeenPatti.onCollectBootAmount += UpdateAmount;
		GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
		GameDelegateTeenPatti.onAddTotalAmount -= UpdateAmount;

	}

	void onRecivedMassage (string sender, string msg)
	{
        try
        {
            JSONNode s = JSON.Parse(msg);
            //// Debug.Log ("onRecivedMassage Total "+s.ToString());
            switch (s[TagsTeenpatti.TAG])
            {

                case ServerTagsTeenpatti.ROOM_BETTING:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                    }
                    break;

                case ServerTagsTeenpatti.COLLECT_BOOT_AMOUNT:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        try
                        {
                            if (TotalCoin != null)
                            {
                                transform.gameObject.SetActive(true);
                                TotalCoin.text = s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT];
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log("COLLECT_BOOT_AMOUNT " + ex.Message);
                        }
                    }
                    break;

                case ServerTagsTeenpatti.PLAYER_RESPONDED:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        try
                        {
                            if (TotalCoin != null)
                            {
                                TotalCoin.text = s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT];
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log("PLAYER_RESPONDED " + ex.Message);
                        }
                    }
                    break;
                case ServerTagsTeenpatti.WINNER_PLAYER:
                    {
                        try
                        {
                            GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                            total = 0;
                            TotalCoin.text = "";
                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log("WINNER_PLAYER exception " + ex.Message);
                        }
                    }
                    break;
                case ServerTagsTeenpatti.START_NEW_GAME:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                    }
                    break;
            }
        }catch(System.Exception ex){
            // Debug.Log(ex.Message);
        }
	}

	void UpdateAmount (int amt)
	{
		
	}



}
