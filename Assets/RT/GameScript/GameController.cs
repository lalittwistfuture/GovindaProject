using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

    public class GameController
    {


        public static bool bettingTime = false;
        public const string MatchEmailPattern =
           @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        // show the message in toast
        public static void showToast(string text)
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
                // Debug.Log(text);
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


            }else{
                // Debug.Log(message);
            }
        }


        public static bool validateEmail(string email)
        {
            if (email != null)
                return Regex.IsMatch(email, MatchEmailPattern);
            else
                return false;
        }

        public delegate void CardSelected(string cardname, string coin);
        public static event CardSelected onCardSelected;
        public static void selectCard(string name, string coi)
        {
            if (onCardSelected != null)
            {
                onCardSelected(name, coi);
            }
        }
        public delegate void LastCardInfo(string cardname);
        public static event LastCardInfo onLastCardInfo;
        public static void sendLastCard(string name)
        {
            if (onLastCardInfo != null)
            {
                onLastCardInfo(name);
            }
        }
        public delegate void LastTicketInfo(string cardname);
        public static event LastTicketInfo onLastTicketInfo;
        public static void sendLastTicket(string name)
        {
            if (onLastTicketInfo != null)
            {
                onLastTicketInfo(name);
            }
        }

        public delegate void RecievedMessage(string cardname, string coin);
        public static event RecievedMessage onRecievedMessage;
        public static void chatRecived(string name, string message)
        {
            if (onRecievedMessage != null)
            {
                onRecievedMessage(name, message);
            }
        }

        public delegate void BettingStop();
        public static event BettingStop onBettingStop;
        public static void stopBetting()
        {
            if (onBettingStop != null)
            {
                onBettingStop();
            }
        }

        public delegate void StartGame();
        public static event StartGame onStartGame;
        public static void startGame()
        {
            if (onStartGame != null)
            {
                onStartGame();
            }
        }


        public delegate void PasswordSend(string message);
        public static event PasswordSend onPasswordSend;
        public static void sendPasswordSuccessFully(string message)
        {
            if (onPasswordSend != null)
            {
                onPasswordSend(message);
            }
        }

        public delegate void StartAnimation();
        public static event StartAnimation onStartAnimation;
        public static void startAnimation()
        {
            if (onStartAnimation != null)
            {
                onStartAnimation();
            }
        }

        public delegate void StopAnimation();
        public static event StopAnimation onStopAnimation;
        public static void stopAnimation()
        {
            if (onStopAnimation != null)
            {
                onStopAnimation();
            }
        }

        public delegate void ChangeTable();
        public static event ChangeTable onChangeTable;
        public static void changeTable()
        {
            if (onChangeTable != null)
            {
                onChangeTable();
            }
        }


        public delegate void WinningCard(string card);
        public static event WinningCard onWinningCard;
        public static void sendWinCard(string card)
        {
            if (onWinningCard != null)
            {
                onWinningCard(card);
            }
        }

    }
