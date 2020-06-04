using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTeenPatti : MonoBehaviour
{
	public AudioClip ClappingSound;
	public AudioClip CollectAmtSound;
	public AudioClip DistributeCardSound;
	public AudioClip ClockSound;
    public AudioClip BlindSound;
    public AudioClip ChalSound;
    public AudioClip ShowSound;
    public AudioClip TurnSound;
    public AudioClip AddSound;
    public void OnEnable ()
	{
		GameDelegateTeenPatti.onClappingSound += onClappingSound;
		GameDelegateTeenPatti.onCollectBootSound += onCollectBootSound;
		GameDelegateTeenPatti.onDistrubuteCardSound += onDistrubuteCardSound;
		GameDelegateTeenPatti.onStartClockSound += onStartClockSound;
        GameDelegateTeenPatti.onBlindChal += onBlindChal;
        GameDelegateTeenPatti.onChaalChal += onChaalChal;
        GameDelegateTeenPatti.onShowChal += onShowChal;
        GameDelegateTeenPatti.onTurnChange += onTurnChange;
        GameDelegateTeenPatti.onAddCoinSound += onAddCoinSound;

        if (PlayerPrefs.GetInt ("Sound") == 1) {
			GameControllerTeenPatti.isSoundOn = true;
		} else {
			GameControllerTeenPatti.isSoundOn = false;
		}
		//GameDelegateTeenPatti.on
	}

	public void OnDisable ()
	{
		GameDelegateTeenPatti.onClappingSound -= onClappingSound;
		GameDelegateTeenPatti.onCollectBootSound -= onCollectBootSound;
		GameDelegateTeenPatti.onDistrubuteCardSound -= onDistrubuteCardSound;
		GameDelegateTeenPatti.onStartClockSound -= onStartClockSound;
        GameDelegateTeenPatti.onBlindChal -= onBlindChal;
        GameDelegateTeenPatti.onChaalChal -= onChaalChal;
        GameDelegateTeenPatti.onShowChal -= onShowChal;
        GameDelegateTeenPatti.onTurnChange -= onTurnChange;
        GameDelegateTeenPatti.onAddCoinSound -= onAddCoinSound;
    }


    void onAddCoinSound()
    {
        if (GameControllerTeenPatti.isSoundOn)
        {
            try
            {
                GetComponent<AudioSource>().volume = 2.0f;
                GetComponent<AudioSource>().clip = AddSound;
                GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().loop = false;
            }
            catch (System.Exception ex)
            {

                // Debug.Log("onCollectBootSound Exception " + ex.Message);
            }
        }

    }


    void onTurnChange()
    {
        if (GameControllerTeenPatti.isSoundOn)
        {
            try
            {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().volume = 2.0f;
                GetComponent<AudioSource>().clip = TurnSound;
                GetComponent<AudioSource>().Play();

            }
            catch (System.Exception ex)
            {

                // Debug.Log("onCollectBootSound Exception " + ex.Message);
            }
        }

    }



	void onDistrubuteCardSound ()
	{
		if (GameControllerTeenPatti.isSoundOn) {
			try {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource> ().volume = 2.0f;
				GetComponent<AudioSource> ().clip = DistributeCardSound;
				GetComponent<AudioSource> ().Play ();
			} catch (System.Exception ex) {

				// Debug.Log ("onCollectBootSound Exception " + ex.Message);
			}
		}

	}

    void onShowChal(){
        if (GameControllerTeenPatti.isSoundOn)
        {
            try
            {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().volume = 2.0f;
                GetComponent<AudioSource>().clip = ShowSound;
                GetComponent<AudioSource>().Play();
            }
            catch (System.Exception ex)
            {

                // Debug.Log("onCollectBootSound Exception " + ex.Message);
            }
        }  
    }


	void onCollectBootSound ()
	{
		if (GameControllerTeenPatti.isSoundOn) {
			try {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource> ().volume = 2.0f;
				GetComponent<AudioSource> ().clip = CollectAmtSound;
				GetComponent<AudioSource> ().Play ();
			} catch (System.Exception ex) {

				// Debug.Log ("onCollectBootSound Exception " + ex.Message);
			}
		}

	}


    void onBlindChal()
    {
        if (GameControllerTeenPatti.isSoundOn)
        {
            try
            {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().volume = 2.0f;
                GetComponent<AudioSource>().clip = BlindSound;
                GetComponent<AudioSource>().Play();
            }
            catch (System.Exception ex)
            {

                // Debug.Log("onCollectBootSound Exception " + ex.Message);
            }
        }

    }


    void onChaalChal()
    {
        if (GameControllerTeenPatti.isSoundOn)
        {
            try
            {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().volume = 2.0f;
                GetComponent<AudioSource>().clip = ChalSound;
                GetComponent<AudioSource>().Play();
            }
            catch (System.Exception ex)
            {

                // Debug.Log("onCollectBootSound Exception " + ex.Message);
            }
        }

    }


	void onClappingSound ()
	{
		if (GameControllerTeenPatti.isSoundOn) {
			try {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource> ().volume = 2.0f;
				GetComponent<AudioSource> ().clip = ClappingSound;
				GetComponent<AudioSource> ().Play ();
			} catch (System.Exception ex) {

				// Debug.Log ("onClappingSound Exception " + ex.Message);
			}
		}
	}

	void onStartClockSound ()
	{
		if (GameControllerTeenPatti.isSoundOn) {
			try {
                //// Debug.Log("onStartClockSound calling ");
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().volume = 2.0f;
				GetComponent<AudioSource> ().clip = ClockSound;
				GetComponent<AudioSource> ().Play ();
			} catch (System.Exception ex) {
				// Debug.Log ("onClappingSound Exception " + ex.Message);
			}
		}
	}




}
