using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using UnityEngine.UI;
namespace LudoGameTemplate{

public class GameController:MonoBehaviour
{
	// Use this for initialization
	int count = 0;
	int number = 5;
	public static string Message = "";
	public static string Message1 = "";
    public static Boolean GameRunning = true;
	// join game details
	public GameObject showTimer;
	public GameObject TimerPanel;

	void Start () 
	{
		
		//StartCoroutine (startGame ());
		//TimerPanel.SetActive (true);
	}
	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator startGame ()
	{
		yield return new WaitForSeconds (1.0f);
		if (number > 0) {
			showTimer.GetComponent<Text> ().text = "new game started in " + number;
			//print ("number is " + number);
			number--;
			StartCoroutine (startGame ());
		} else if (number == 0) {
			TimerPanel.SetActive (false);
			InvokeRepeating ("turnOn", 0.0f, GameConstantData.countTimer);
		}
	}

	void turnOn ()
	{
		
		/*	if (count % 2 == 0) {
			GameDelegate.showcurrentPlayer ();
		} else {
			GameDelegate.showMyOpponentPlayer ();
		}
		count++;
		*/
	}

	
}
}