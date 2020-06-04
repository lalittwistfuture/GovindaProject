using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate{
public class FakePlayer : BasePlayer {

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnEnable ()
	{
		//GameDelegate.onTapDice += onTapDice;
	}

	void OnDisable ()
	{
		//GameDelegate.onTapDice -= onTapDice;
	}



	public void rollDices(){
		
	}

	public void MoveGoti(){

	}

//	void onTapDice (string diceNumber)
//	{
//		
//	}


}
}