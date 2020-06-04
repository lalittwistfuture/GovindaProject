using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate
{
    public class GotiScript : MonoBehaviour
    {

        public const int LEFT = 1;
        public const int RIGHT = 2;
        public const int TOP = 3;
        public const int BOTTOM = 4;
        public const int CENTER = 5;

        public bool isLock = true;
        public LudoColor gotiColor;
        public GameObject Indicator;
        public GameObject Panel;
        public GameObject[] Button;
        public GameObject animator;
        [HideInInspector]
        public int openPoint;
        [HideInInspector]
        public int winingPoint;
        [HideInInspector]
        public int currentPosition = -1;
        [HideInInspector]
        public int initialPosition;
        [HideInInspector]
        public string playerName;
        [HideInInspector]
        public int winingGate;
        [HideInInspector]
        private bool Enable = false;
        [HideInInspector]
        private int destination = 0;
        [HideInInspector]
        public bool isHome = false;
        [HideInInspector]
        public int homeEntryPoint = 0;
        [HideInInspector]
        private Color originalColor;
        private JSONNode TurnValue;
        [HideInInspector]
        public Vector3 InitialPositionVector;
        [HideInInspector]
        public int index;

        [HideInInspector]
        public int CellNumber;


        [HideInInspector]
        public bool isMoving = false;


        Listener listener;

        // Use this for initialization
        void Start()
        {
            InitialPositionVector = transform.position;
            originalColor = GetComponent<Image>().color;
            Indicator.SetActive(false);
            animator.SetActive(false);
            listener = GameObject.Find("Canvas").GetComponent<Listener>();
            //removeOption ();
            //GetComponent<Animation> ().Stop ();
            //updateData ();
            //gameObject.SetActive(false);

            //if (playerName.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
            //{
            //    StartCoroutine(sendData());
            //}
        }

        //IEnumerator sendData(){
        //    while(true){
        //        int[] data_f = new int[3];
        //        data_f[0] = index;
        //        data_f[1] = currentPosition;
        //        data_f[2] = 1;
        //        int data_len = (sizeof(int) * 3) + (SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
        //        byte[] data = new byte[data_len];
        //        System.Buffer.BlockCopy(data_f, 0, data, 0, sizeof(int) * 3);
        //        System.Buffer.BlockCopy(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).ToCharArray(), 0, data, sizeof(int) * 3, SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
        //        listener.sendBytes(data, true);
        //        yield return new WaitForSeconds(0.2f);
        //    }


        //}



        public void updateData(string player)
        {
            currentPosition = -1;
            switch (player)
            {
                case "Player1":
                    {
                        initialPosition = 28;
                        homeEntryPoint = 65;
                        winingPoint = 70;
                        //playerName = base.userName.text;
                    }
                    break;
                case "Player2":
                    {
                        initialPosition = 2;
                        homeEntryPoint = 53;
                        winingPoint = 58;
                        //playerName = base.userName.text;
                    }
                    break;
                case "Player4":
                    {
                        initialPosition = 15;
                        homeEntryPoint = 59;
                        winingPoint = 64;
                        //playerName = base.userName.text;
                    }
                    break;
                case "Player3":
                    {
                        initialPosition = 41;
                        homeEntryPoint = 71;
                        winingPoint = 76;
                        //playerName = base.userName.text;
                    }
                    break;
                default:
                    break;
            }
        }


        public void numberSelection(GameObject btn)
        {
            try
            {
                int number = int.Parse(btn.transform.GetChild(0).GetComponent<Text>().text);
                GameDelegate.stopAnimation();
                GameDelegate.selectNumber(number);
                appwarp.userSelectGoti(index, number);
                Enable = false;
                removeOption();
                GameDelegate.StartDisableAllGoti();
            }
            catch (System.Exception ex)
            {
                // Debug.Log (ex.Message);
            }
        }



        // Update is called once per frame
        void Update()
        {
            /*	if (Enable) {
                transform.Rotate (Vector3.back, 10, Space.World);
            }*/

        }

        void OnEnable()
        {
            GameDelegate.onStopSelection += onStopSelection;
            GameDelegate.onRecivedMassage += onRecivedMassage;
            GameDelegate.onHideGotipanel += removeOption;
            GameDelegate.onDisableAllGoti += onDisableAllGoti;
            GameDelegate.onRemoveNumber += onRemoveNumber;
            GameDelegate.onPlayerSyncronized += syncronized;
        }

        void OnDisable()
        {
            GameDelegate.onStopSelection -= onStopSelection;
            GameDelegate.onRecivedMassage -= onRecivedMassage;
            GameDelegate.onHideGotipanel -= removeOption;
            GameDelegate.onDisableAllGoti -= onDisableAllGoti;
            GameDelegate.onRemoveNumber -= onRemoveNumber;
            GameDelegate.onPlayerSyncronized -= syncronized;
        }

        void onRemoveNumber(int number)
        {
            TurnValue = new JSONNode();
        }

        void onStopSelection()
        {
            animator.GetComponent<Animation>().Stop();
            animator.SetActive(false);
        }

        void onRecivedMassage(string sender, string msg)
        {
            try
            {
                JSONNode s = JSON.Parse(msg);
                switch (s[ServerTags.TAG])
                {
                    case ServerTags.GOTI_WIN:
                        {
                            string player_name = playerName;

                            if (player_name.Equals(s[ServerTags.PLAYER]))
                            {
                                int from = int.Parse(s[ServerTags.FROM_POSITION]);
                                if (from == index)
                                {
                                    gameObject.SetActive(false);
                                }
                            }

                        }
                        break;
                    case ServerTags.TURN:
                        {
                            Enable = false;
                            animator.SetActive(false);
                            GetComponent<Image>().color = originalColor;

                        }
                        break;
                    case ServerTags.READY_GOTI:
                        {
                            //string player_name = playerName;
                            //// Debug.Log ("READY_GOTI "+playerName);
                            //// Debug.Log ("Player name "+player_name);
                            if (playerName.Equals(s[ServerTags.PLAYER]))
                            {
                                int position = int.Parse(s[ServerTags.POSITION]);
                                readyGoti(position);
                                TurnValue = s["STEPS"];
                            }

                        }
                        break;

                }
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
        }

        public void moveGoti(int current, int next)
        {
            if (playerName.Equals(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID)))
            {
                if (next != 0)
                {
                    if (currentPosition == current)
                    {
                        try
                        {
                            int nextPos = initialPosition + next;
                            int Object = nextPos > 52 ? nextPos - 52 : nextPos;
                            GameObject pos = GameObject.Find("" + Object);
                            transform.position = pos.transform.position;
                            currentPosition = next;

                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log("moveGoti Exception " + ex.Message);
                        }
                    }
                }
            }
        }





        public void syncronized(string player, int[] data)
        {
            //if (player.Equals(playerName))
            //{
            //    if(data[2]==1){
            //        if (data[0] == index)
            //        {
            //            if (currentPosition != data[1])
            //            {
            //                currentPosition = data[1];
            //                if (data[1] == -1)
            //                {
            //                    transform.position = InitialPositionVector;
            //                }
            //                else
            //                {
            //                    int nextPos = initialPosition + data[1];
            //                    int Object = nextPos > 52 ? nextPos - 52 : nextPos;
            //                    GameObject pos = GameObject.Find("" + Object);
            //                    transform.position = pos.transform.position;
            //                }
            //            }
            //        }
            //    }
            //}
        }



        void readyGoti(int pos)
        {

            if (index == pos)
            {
                Enable = true;
                animator.SetActive(true);
                animator.GetComponent<Animation>().Play();
            }
        }

        void removeOption()
        {
            // Debug.Log("HideButton");
            // for (int i = 0; i < Button.Length; i++) {
            // 	Button [i].SetActive (false);
            // }
            // Panel.SetActive (false);
            Indicator.SetActive(false);
            animator.SetActive(false);

        }

        void onDisableAllGoti()
        {
            Enable = false;
        }

        public void TapGotiAction()
        {
            GameDelegate.hideButton();

            if (Enable)
            {
                GameDelegate.hideGotipanel();
                if (playerName == UserController.getInstance.ID)
                {
                    //// Debug.Log (" value is " + TurnValue.Count);
                    // Debug.Log (" value is " + TurnValue.ToString ());
                    if (TurnValue.Count > 1)
                    {
                        //removeOption ();
                        // Debug.Log ("Multiple Values ");
                        if (currentPosition == -1)
                        {
                            // Debug.Log ("Click on House goti");
                            if (userHasOne() && userHasSix())
                            {

                                GameDelegate.showButton(TurnValue, transform.gameObject);
                                // Debug.Log("call showButton");
                                //							Panel.SetActive (true);
                                //							Indicator.SetActive (true);
                                //							for (int i = 0; i < TurnValue.Count; i++) {
                                //								Button [i].SetActive (true);
                                //								Button [i].transform.GetChild (0).GetComponent<Text> ().text = TurnValue [i];
                                //							}
                            }
                            else
                            {

                                if (userHasSix())
                                {
                                    GameDelegate.stopAnimation();
                                    Enable = false;
                                    GameDelegate.showRemoveNumber(6);
                                    GameDelegate.selectNumber(6);
                                    appwarp.userSelectGoti(index, 6);
                                    // Debug.Log (" You have 6 ");
                                }
                                else if (userHasOne())
                                {
                                    GameDelegate.stopAnimation();
                                    GameDelegate.showRemoveNumber(1);
                                    GameDelegate.selectNumber(1);
                                    appwarp.userSelectGoti(index, 1);
                                    Enable = false;
                                    // Debug.Log (" You have 1 ");
                                }
                            }
                        }
                        else
                        {
                            //						GameDelegate.hideGotipanel ();
                            // Debug.Log ("Click on running goti. Show PopUp");

                            GameDelegate.showButton(TurnValue, transform.gameObject);
                            //						Panel.SetActive (true);
                            //
                            //						Indicator.SetActive (true);
                            //						for (int i = 0; i < TurnValue.Count; i++) {
                            //							Button [i].SetActive (true);
                            //							Button [i].transform.GetChild (0).GetComponent<Text> ().text = TurnValue [i];
                            //						}
                        }

                    }
                    else if (TurnValue.Count == 1)
                    {
                        // Debug.Log ("Have single value");
                        int number = int.Parse(TurnValue[0]);

                        if (currentPosition == -1)
                        {
                            if (number == 6 || number == 1)
                                GameDelegate.stopAnimation();
                            GameDelegate.selectNumber(number);
                            appwarp.userSelectGoti(index, number);
                            Enable = false;
                        }
                        if (currentPosition != -1)
                        {
                            GameDelegate.stopAnimation();
                            GameDelegate.selectNumber(number);
                            appwarp.userSelectGoti(index, number);
                            Enable = false;
                        }
                    }

                    // Debug.Log (" value is " + TurnValue.Count);
                    animator.GetComponent<Animation>().Stop();
                    animator.SetActive(false);
                    //print ("Tap on working " + index);
                }
            }
            else
            {
                // Debug.Log ("is not enable");
            }


        }


        bool userHasSix()
        {
            for (int i = 0; i < TurnValue.Count; i++)
            {
                if (int.Parse(TurnValue[i]) == 6)
                {
                    return true;
                }
            }
            return false;
        }

        bool userHasOne()
        {
            for (int i = 0; i < TurnValue.Count; i++)
            {
                if (int.Parse(TurnValue[i]) == 1)
                {
                    return true;
                }
            }
            return false;
        }


        public static int getGotiPosition(GameObject goti)
        {
            GameObject left = GameObject.Find("72");
            GameObject right = GameObject.Find("60");
            GameObject upper = GameObject.Find("53");
            GameObject bottom = GameObject.Find("65");
            if (goti.transform.position.x < left.transform.position.x)
            {
                return GotiScript.LEFT;
            }
            if (goti.transform.position.x > right.transform.position.x)
            {
                return GotiScript.RIGHT;
            }
            if (goti.transform.position.y > upper.transform.position.y)
            {
                return GotiScript.TOP;
            }
            if (goti.transform.position.y < bottom.transform.position.y)
            {
                return GotiScript.BOTTOM;
            }

            return GotiScript.CENTER;

        }


    }
}