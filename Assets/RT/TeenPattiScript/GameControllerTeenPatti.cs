using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameControllerTeenPatti
{

	public  const string MatchEmailPattern =
		@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


	public static string TeenPatti_message = "";
	public static int NetworkStatus = NetworkTagsTeenPatti.NOT_CONNECTED;

	//List<CardArrayTeenPatti> mylist = new List<CardArrayTeenPatti>();
	//public static List<CardArrayTeenPatti> myCardList = new List<CardArrayTeenPatti> ();
	//public static CardArrayTeenPatti CardDetails = new CardArrayTeenPatti();

	// player details

	public static string Player_ID = "randhir313";
	public static string Player_Display_Name = "Randhir";
	public static string Player_Image = "";
	//public static int Total_coin = 0;
	public static int match_won = 0;
	public static string Player_ObjectID = "121";
	public static bool isGuest = false;
	public static float countTimer = 20.0f;
	public static float BetAmount = 0;
	public static bool SideShowRequest = false;
    public static bool variation = false;
	public static bool isChallenge = false;
	public static float BootAmount = 0;
	public static float PortLimit = 0;
	public static float MaxBetAmt = 0;
	public static string GameType = "";
	public static string PrivateGameType = "";
	public static bool isSoundOn = true;
	public static bool isConnected = true;
	public static int TipAmt = 10;

	//store card using this key
	//public const string CardArray = "CardArray";
	//public const string OpponentCardArray = "OpponentCardArray";

	public static void showToast (string text)
	{
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

			AndroidJavaClass Toast = new AndroidJavaClass ("android.widget.Toast");
			AndroidJavaObject javaString = new AndroidJavaObject ("java.lang.String", text);
			AndroidJavaObject context = currentActivity.Call<AndroidJavaObject> ("getApplicationContext");
			AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int> ("LENGTH_SHORT"));
			toast.Call ("show");
		} else {
						// Debug.Log (text);
		}
	}

	public static bool CheakInternetConnectivity ()
	{

		if (Application.internetReachability != NetworkReachability.NotReachable) {
			//// Debug.Log ("connection found!");
			return true;
		} else {
			//// Debug.Log ("connection  not found!");
			return false;
		}
	}

    public static bool validateEmail(string email)
    {
        if (email != null)
        {
            	return Regex.IsMatch (email, MatchEmailPattern);
        }
        else
        {
          
        }
        return false;
    }

	public static string UppercaseFirst (string s)
	{
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}
		char[] a = s.ToCharArray ();
		a [0] = char.ToUpper (a [0]);
		return new string (a);
	}

	public static void shareText (string message)
	{

		if (Application.platform == RuntimePlatform.Android) {

			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			//Refernece of AndroidJavaObject class for intent
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			//call setAction method of the Intent object created
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			//set the type of sharing that is happening
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			//add data to be passed to the other activity i.e., the data to be sent
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), "Share");
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), message);
			//get the current activity
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
			//start the activity by sending the intent data
			currentActivity.Call ("startActivity", intentObject);


		} else {
			// Debug.Log (message);
		}
	}
    public delegate void SelectAvtar(string name);
    public static event SelectAvtar onSelectAvtar;
    public static void selectAvtarName(string name)
    {
        if (onSelectAvtar != null)
        {
            onSelectAvtar(name);
        }
    }





}
