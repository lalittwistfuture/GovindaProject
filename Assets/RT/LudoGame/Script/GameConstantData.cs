using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace LudoGameTemplate
{
    public enum LudoColor
    {
        Red = 1,
        Green = 2,
        Blue = 4,
        Yellow = 3
    }
    ;

    public class GameConstantData : MonoBehaviour
    {

        public static float countTimer = 40.0f;
        public static int OneToOne = 1;
        public static int OneToFour = 2;
        public static int Practice = 3;
        public static int Private = 4;
        public static int GameType = 3;
        public static int UserLimit = 1;
        public static int entryFee = 0;
        public static int winingAmount = 0;
        public static int TokenLimit = 4;




        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


        public static bool validateEmail(string email)
        {
            if (email != null)
                return Regex.IsMatch(email, MatchEmailPattern);
            else
                return false;
        }

        public static void showToast(Transform trans, string text)
        {
            GameObject pop = null;
            try
            {
                // Debug.Log("getting pop");
                pop = GameObject.FindGameObjectWithTag("PopUp");
                if (pop == null)
                {
                    pop = Instantiate(Resources.Load("_prefeb/PopUp")) as GameObject;
                }

            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
                //Resources.Load ("_prefeb/PopUp") as GameObject;
                pop = Instantiate(Resources.Load("_prefeb/PopUp")) as GameObject;
            }
            finally
            {

                // Debug.Log("finally");
                if (pop)
                {
                    pop.GetComponent<PopUpScript>().msgText.text = text;
                    pop.transform.position = trans.position;
                    pop.transform.SetParent(trans);
                    if (SceneManager.GetActiveScene().name.Equals("GameScene"))
                    {
                        pop.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                    }
                    else
                    {
                        pop.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }

                }
                else
                {
                    // Debug.Log("finally null");  
                }
            }



        }


        public static void showToastAndroid(string text)
        {


            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
                AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", text);
                AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
                toast.Call("show");
            }
            else
            {
                // Debug.Log (text);
            }



        }



        public static void shareText(string message)
        {

            if (Application.platform == RuntimePlatform.Android)
            {

                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                //Refernece of AndroidJavaObject class for intent
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                //call setAction method of the Intent object created
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
                //set the type of sharing that is happening
                intentObject.Call<AndroidJavaObject>("setType", "text/plain");
                //add data to be passed to the other activity i.e., the data to be sent
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Share");
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);
                //get the current activity
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                //start the activity by sending the intent data
                currentActivity.Call("startActivity", intentObject);


            }
        }


        public static bool CheckNetworkConnection()
        {

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                return true;
            }
            else
            {
                //showToast ("No internet connection found?");
                return false;

            }
        }




    }
}