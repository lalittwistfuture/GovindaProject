using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class teenPattiConnectionSymbol : MonoBehaviour {

	//public Image ConnectionSignal;

	//private int currentNetwork = ErrorType.RecoverConnection;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable ()
	{
		
		//RouletteDelegate.onErrorRecieved += onErrorRecieved;

	}

	void OnDisable ()
	{

		//RouletteDelegate.onErrorRecieved -= onErrorRecieved;
	}

	void onErrorRecieved (int type)
	{
		//RouletteDelegate.NetworkError = type;

	}

	void FixedUpdate ()
	{

	/*	if (currentNetwork != RouletteDelegate.NetworkError) {
			currentNetwork = RouletteDelegate.NetworkError;
			switch (RouletteDelegate.NetworkError) {
			case ErrorType.ConnectionNotFound:
				ConnectionSignal.sprite = Resources.Load<Sprite> ("red");
				break;
			case ErrorType.RoomNotFound:
				ConnectionSignal.sprite = Resources.Load<Sprite> ("red");
				break;
			case ErrorType.ConnectionTempraryError:
				ConnectionSignal.sprite = Resources.Load<Sprite> ("yellow");
				break;
			case ErrorType.RecoverConnection:
				ConnectionSignal.sprite = Resources.Load<Sprite> ("green");
				break;
			}
		}*/
	}
}
