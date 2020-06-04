using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
namespace Roullet {
    public class Roulette_FirstPage : MonoBehaviour {

        // Use this for initialization
        private GameObject panel;
        public AudioClip placeYourBet;
        public AudioClip Congradulation;
        public AudioClip StopBet;
        private AudioSource Player;
        public Image ConnectionSignal;
        JSONNode Numbers;
        private int currentNetwork = ErrorType.RecoverConnection;

        void Start () {
            panel = GameObject.Find ("RouletteTable");
            panel.SetActive (false);
            Player = transform.GetComponent<AudioSource> ();

        }

        void OnEnable () {
            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
            RouletteDelegate.onErrorRecieved += onErrorRecieved;
            RouletteDelegate.onWinChip += onWinChip;
        }

        void OnDisable () {
            RouletteDelegate.onWarpChatRecived -= onWarpChatRecived;
            RouletteDelegate.onWinChip -= onWinChip;
            RouletteDelegate.onErrorRecieved -= onErrorRecieved;
        }

        void onWinChip () {
            if (PlayerPrefs.GetInt ("Sound") == 1) {
                Player.PlayOneShot (Congradulation);
            }
        }

        void onErrorRecieved (int type) {
            RouletteDelegate.NetworkError = type;

        }

        IEnumerator ErrorHandler (int type) {
            yield return new WaitForSeconds (0.5f);

        }

        void onWarpChatRecived (string sender, string message) {
            JSONNode s = JSON.Parse (message);

            switch (s[RouletteTag.TAG]) {
                case "TABLE_STATUS":
                    {

                        Numbers = JSON.Parse (s["PREVIOUS_NUMBER"]);
                        if (panel.activeSelf) {
                            panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                            panel.GetComponent<RoulleteManager> ().ManageHistoryPanels (Numbers);

                        }
                    }
                    break;
                case RouletteTag.TIME:
                    {
                        panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                        if (!panel.activeSelf) {

                            panel.SetActive (true);

                        }
                    }
                    break;
                case RouletteTag.MOVE_TO_TABLE:
                    {
                        panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                        if (!panel.activeSelf) {
                            //panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                            panel.SetActive (true);

                        }
                    }
                    break;
                case RouletteTag.BETTING_START:
                    {
                        panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                        if (!panel.activeSelf) {
                            if (PlayerPrefs.GetInt ("Sound") == 1) {
                                // panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                                panel.SetActive (true);

                                Player.PlayOneShot (placeYourBet);
                            }
                        }

                    }
                    break;
                case RouletteTag.BETTING_STOP:
                    {
                        panel.GetComponent<RoulleteManager> ().Numbers = Numbers;
                        if (!panel.activeSelf) {

                            panel.SetActive (true);

                        }
                        if (PlayerPrefs.GetInt ("Sound") == 1) {
                            Player.PlayOneShot (StopBet);
                        }
                    }
                    break;

            }
        }

        void FixedUpdate () {

            if (currentNetwork != RouletteDelegate.NetworkError) {
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
            }
        }
    }
}