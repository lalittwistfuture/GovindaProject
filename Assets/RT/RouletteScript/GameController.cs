using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
namespace Roullet
{
    public class GameController
    {

        public static int totalOpponent = 3;
        public static ArrayList totalCard = new ArrayList();
        public static float countTimer = 45.0f;
        public static float coin = 0;
        public static float practic_coin = 0;
        public static int GameLevel = 1;
        public static string Message = "";
        public static int JokerCard = -1;
        public static bool GameStarted = true;
        public static bool isPlayerActive = true;
        public static bool isGroupArrange = true;
        public static bool isMyTurn = false;
        public static bool isGameOver = false;
        public static int ExtraCard = -1;
        public static int ConnectionNumber = -1;
        public static bool extraCardFromPack = false;

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


            }
        }


        public static bool validateEmail(string email)
        {
            if (email != null)
                return Regex.IsMatch(email, MatchEmailPattern);
            else
                return false;
        }

        public delegate void AddPlayer(SimpleJSON.JSONNode name);
        public static event AddPlayer onAddPlayer;
        public static void newPlayer(SimpleJSON.JSONNode name)
        {
            if (onAddPlayer != null)
            {
                onAddPlayer(name);
            }
        }

        public delegate void MobileNotRegister();
        public static event MobileNotRegister onMobileNotRegister;
        public static void notRegister()
        {
            if (onMobileNotRegister != null)
            {
                onMobileNotRegister();
            }
        }

        public delegate void NotHaveEnoughCoin();
        public static event NotHaveEnoughCoin onNotHaveEnoughCoin;
        public static void playerNotHaveCoin()
        {
            if (onNotHaveEnoughCoin != null)
            {
                onNotHaveEnoughCoin();
            }
        }



        public delegate void OpenOTPContainer(string userMobile, string senderMobile);
        public static event OpenOTPContainer onOpenOTPContainer;
        public static void openOTP(string userMobile, string senderMobile)
        {
            if (onOpenOTPContainer != null)
            {
                onOpenOTPContainer(userMobile, senderMobile);
            }
        }

        public delegate void VarifyOTP();
        public static event VarifyOTP onVarifyOTP;
        public static void doneOTP()
        {
            if (onVarifyOTP != null)
            {
                onVarifyOTP();
            }
        }

        public delegate void ErrorMessage(string message);
        public static event ErrorMessage onErrorMessage;
        public static void sendErrorMessage(string message)
        {
            if (onErrorMessage != null)
            {
                onErrorMessage(message);
            }
        }


        public delegate void ReConnectServer();
        public static event ReConnectServer onReConnectServer;
        public static void recoverConnection()
        {
            if (onReConnectServer != null)
            {
                onReConnectServer();
            }
        }

        public delegate void ShowRummyTableID(string id);
        public static event ShowRummyTableID onShowRummyTableID;
        public static void showRummyTablePopUP(string id)
        {
            if (onShowRummyTableID != null)
            {
                onShowRummyTableID(id);
            }
        }

        public delegate void EnableToStart();
        public static event EnableToStart onEnableToStart;
        public static void readyToGame()
        {
            if (onEnableToStart != null)
            {
                onEnableToStart();
            }
        }

        public delegate void AddCardToHolder(string cardNumber);
        public static event AddCardToHolder onAddCardToHolder;
        public static void addCardInHand(string cardNumber)
        {
            if (onAddCardToHolder != null)
            {
                onAddCardToHolder(cardNumber);
            }
        }


        public delegate void RemoveAllCard();
        public static event RemoveAllCard onRemoveAllCard;
        public static void removeAllCards()
        {
            if (onRemoveAllCard != null)
            {
                onRemoveAllCard();
            }
        }

        public delegate void SendGroupData();
        public static event SendGroupData onSendGroupData;
        public static void sendCardToServer()
        {
            if (onSendGroupData != null)
            {
                onSendGroupData();
            }
        }

        public delegate void ArrangeGroup();
        public static event ArrangeGroup onArrangeGroup;
        public static void makeGroup()
        {
            if (onArrangeGroup != null)
            {
                onArrangeGroup();
            }
        }


        public delegate void SortCard();
        public static event SortCard onSortCard;
        public static void sortHandCard()
        {
            if (onSortCard != null)
            {
                onSortCard();
            }
        }


        public delegate void ShowRummyTableJoin();
        public static event ShowRummyTableJoin onShowRummyTableJoin;
        public static void showRummyTableJoinPopUP()
        {
            if (onShowRummyTableJoin != null)
            {
                onShowRummyTableJoin();
            }
        }

        public delegate void SendDeclare();
        public static event SendDeclare onSendDeclare;
        public static void sendDeclareRequest()
        {
            if (onSendDeclare != null)
            {
                onSendDeclare();
            }
        }


        public delegate void ActiveHolder();
        public static event ActiveHolder onActiveHolder;
        public static void openHolders()
        {
            if (onActiveHolder != null)
            {
                onActiveHolder();
            }
        }

        public delegate void AddCardFromCloseDeck(string card);
        public static event AddCardFromCloseDeck onAddCardFromCloseDeck;
        public static void addCardFromCloseDeck(string card)
        {
            if (onAddCardFromCloseDeck != null)
            {
                onAddCardFromCloseDeck(card);
            }
        }

        public delegate void ConnectionStatus(int status);
        public static event ConnectionStatus onConnectionStatus;
        public static void setConnectionStatus(int card)
        {
            if (onConnectionStatus != null)
            {
                onConnectionStatus(card);
            }
        }

        public delegate void AddCardFromOpenDeck(string card);
        public static event AddCardFromOpenDeck onAddCardFromOpenDeck;
        public static void addCardFromOpenDeck(string card)
        {
            if (onAddCardFromOpenDeck != null)
            {
                onAddCardFromOpenDeck(card);
            }
        }

        public delegate void LeaveCard();
        public static event LeaveCard onLeaveCard;
        public static void leaveCard()
        {
            if (onLeaveCard != null)
            {
                onLeaveCard();
            }
        }


        public delegate void ArrangeCard();
        public static event ArrangeCard onArrangeCard;
        public static void resetCards()
        {
            if (onArrangeCard != null)
            {
                onArrangeCard();
            }
        }

        public delegate void ShowCards();
        public static event ShowCards onShowCards;
        public static void showCards()
        {
            if (onShowCards != null)
            {
                onShowCards();
            }
        }


        public delegate void DeActiveHolder();
        public static event DeActiveHolder onDeActiveHolder;
        public static void closeHolders()
        {
            if (onDeActiveHolder != null)
            {
                onDeActiveHolder();
            }
        }

        public delegate void CardSelected(Transform name);
        public static event CardSelected onCardSelected;
        public static void selectCard(Transform name)
        {
            if (onCardSelected != null)
            {
                onCardSelected(name);
            }
        }

        public delegate void RemovePlayer(string name);
        public static event RemovePlayer onRemovePlayer;
        public static void removePlayer(string name)
        {
            if (onRemovePlayer != null)
            {
                onRemovePlayer(name);
            }
        }


        public delegate void RecivedMassage(string sender, string msg);
        public static event RecivedMassage onRecivedMassage;
        public static void chatRecived(string sender, string msg)
        {
            if (onRecivedMassage != null)
            {
                onRecivedMassage(sender, msg);
            }
        }

        public delegate void ConnectionError();
        public static event ConnectionError onConnectionError;
        public static void connectionError()
        {
            if (onConnectionError != null)
            {
                onConnectionError();
            }
        }

        public delegate void RecoverConnection();
        public static event RecoverConnection onRecoverConnection;
        public static void recoverAppConnection()
        {
            if (onRecoverConnection != null)
            {
                onRecoverConnection();
            }
        }

        public delegate void ShowGameFinish(GameObject obj);
        public static event ShowGameFinish onShowGameFinish;
        public static void showGameFinishPanel(GameObject obj)
        {
            if (onShowGameFinish != null)
            {
                onShowGameFinish(obj);
            }
        }



    }
}