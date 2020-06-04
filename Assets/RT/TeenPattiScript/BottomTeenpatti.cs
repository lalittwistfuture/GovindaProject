//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomTeenpatti : MonoBehaviour {

    public GameObject Panel;
    public GameObject SideShow;
    private float maximumBet = 0;
    private float currentAmount = 0;
    private float minimumBet = 0;
    int PreviousPlayerStatus = 1;
    float stakeAmount = 0;
    int MyStatus = 1;
    float BetAmount = 0;
    public GameObject PackBtn;
    public GameObject ShowBtn;
    public BasePlayerTeenpatti currentPlayer;
    int TOTAL_ACTIVE_PLAYERS = 0;
    string previousPlayerId = "";
    public Text PackText;
    public Text minimumBetTitle;
    public Text maximumBetTitle;
    public Text showTitle;
    public Text minimumBettext;
    public Text maximumBettext;
    public GameObject minimumBetBtn;
    public GameObject maximumBetBtn;

    void Start () {
        try {

            HideMyPanel (Panel);
            SideShow.SetActive (false);
        } catch (System.Exception ex) {
            // Debug.Log ("Start Bottm EXception " + ex.Message);
        }
    }

    void OnEnable () {
        GameDelegateTeenPatti.onShowControlPanel += onShowControlPanel;
        GameDelegateTeenPatti.onHideControlPanel += onHideControlPanel;
        GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
        GameDelegateTeenPatti.onEnableSideShow += onEnableSideShow;
        GameDelegateTeenPatti.onGetRoomInfo += onGetRoomInfo;
    }

    void OnDisable () {
        GameDelegateTeenPatti.onShowControlPanel -= onShowControlPanel;
        GameDelegateTeenPatti.onHideControlPanel -= onHideControlPanel;
        GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;
        GameDelegateTeenPatti.onEnableSideShow -= onEnableSideShow;
        GameDelegateTeenPatti.onGetRoomInfo -= onGetRoomInfo;
    }

    void onGetRoomInfo () {

        HideMyPanel (Panel);
        //ShowMyPanel (BetDetailsPanel);
    }

    void onEnableSideShow () {
        if (PreviousPlayerStatus == 2) {
            SideShow.SetActive (true);
        }
    }

    void updateBetSystem () {

        if (MyStatus != PreviousPlayerStatus) {
            if (MyStatus > PreviousPlayerStatus) {

                this.currentAmount = this.stakeAmount * 2;
            } else {

                this.currentAmount = this.stakeAmount / 2;
            }
        } else {

            this.currentAmount = this.stakeAmount;
        }

        float s = PlayerPrefs.GetFloat (PrefebTagsTeenpatti.COLLECT_BOOT_AMOUNT);
        if (this.currentAmount <= s) {

            this.currentAmount = s;
        }
        //HideMyPanel (BetDetailsPanel);
        //ShowMyPanel (Panel);
        if (this.currentAmount >= GameControllerTeenPatti.MaxBetAmt) {

            currentAmount = GameControllerTeenPatti.MaxBetAmt;
            this.maximumBet = this.currentAmount;
        } else {
            this.maximumBet = 2 * this.currentAmount;
        }
        this.minimumBet = this.currentAmount;

        if (MyStatus == 1) {
            if (this.currentAmount >= GameControllerTeenPatti.MaxBetAmt / 2) {
                this.currentAmount = GameControllerTeenPatti.MaxBetAmt / 2;
                this.maximumBet = this.currentAmount;
            }
        } else {
            if (this.currentAmount >= GameControllerTeenPatti.MaxBetAmt) {
                this.currentAmount = GameControllerTeenPatti.MaxBetAmt;
                this.maximumBet = this.currentAmount;
            }
        }
        // Debug.Log ("total coins = |" + currentPlayer.totalCoin.text + "|");
        if (float.Parse (currentPlayer.totalCoin.text) > this.maximumBet) {
            minimumBettext.text = "" + this.minimumBet;
            maximumBettext.text = "" + this.maximumBet;
        } else {
            if (float.Parse (currentPlayer.totalCoin.text) > this.minimumBet) {
                minimumBettext.text = "" + this.minimumBet;
            } else {
                minimumBettext.text = float.Parse (currentPlayer.totalCoin.text).ToString ("F2");
            }
            maximumBettext.text = float.Parse (currentPlayer.totalCoin.text).ToString ("F2");
        }

        if (this.maximumBet >= float.Parse (currentPlayer.totalCoin.text)) {

        } else {

        }
        checkButtonValidation ();
    }

    public void minimumChal () {
        GameControllerTeenPatti.BetAmount = float.Parse (this.minimumBettext.text);
        if (coinValidation ()) {
            GameControllerTeenPatti.BetAmount = float.Parse (this.minimumBettext.text);
            GameDelegateTeenPatti.challChal ();
            appwrapTeenpatti.SendChaalAmount ();
        } else {

            GameControllerTeenPatti.BetAmount = float.Parse (currentPlayer.totalCoin.text);
            appwrapTeenpatti.SendChaalAmount ();

        }
    }

    public void maximumChal () {
        GameControllerTeenPatti.BetAmount = float.Parse (this.maximumBettext.text);
        if (coinValidation ()) {
            GameControllerTeenPatti.BetAmount = float.Parse (this.maximumBettext.text);
            GameDelegateTeenPatti.blindChal ();
            appwrapTeenpatti.SendChaalAmount ();
        } else {
            GameControllerTeenPatti.BetAmount = float.Parse (currentPlayer.totalCoin.text);
            appwrapTeenpatti.SendChaalAmount ();
        }
    }

    void onShowControlPanel (int amount) {
        //this.stakeAmount = amount;
        //// Debug.Log ("My Stake amount " + amount);
        //updateBetSystem ();
        //this.BetCoin.GetComponent<Text> ().text = "" + this.currentAmount;

    }

    void onHideControlPanel () {
        HideMyPanel (Panel);
    }

    // Update is called once per frame
    void Update () { }

    public void plusAction () {

    }

    void checkButtonValidation () {
        if (float.Parse (currentPlayer.totalCoin.text) >= this.currentAmount && this.currentAmount <= GameControllerTeenPatti.MaxBetAmt) {
            maximumBetBtn.GetComponent<Button> ().interactable = true;
            PackBtn.GetComponent<Button> ().interactable = true;
            ShowBtn.GetComponent<Button> ().interactable = true;
            minimumBetBtn.GetComponent<Button> ().interactable = true;

        }
    }

    public void MinusAction () { }

    public void PackAction () {
        GameDelegateTeenPatti.ShowoDistrubuteCardSound ();
        appwrapTeenpatti.SendPackAmount ();
        StopTimer ();
        HideMyPanel (Panel);
    }

    public void ChalAction () {
        if (coinValidation ()) {
            if (MyStatus == 1) {
                if (this.currentAmount >= GameControllerTeenPatti.MaxBetAmt / 2) {
                    this.currentAmount = GameControllerTeenPatti.MaxBetAmt / 2;
                    this.maximumBet = this.currentAmount;
                }
            } else {
                if (this.currentAmount >= GameControllerTeenPatti.MaxBetAmt) {
                    this.currentAmount = GameControllerTeenPatti.MaxBetAmt;
                    this.maximumBet = this.currentAmount;
                }
            }
            GameControllerTeenPatti.BetAmount = this.currentAmount;
            appwrapTeenpatti.SendChaalAmount ();
        } else {
            GameControllerTeenPatti.BetAmount = float.Parse (currentPlayer.totalCoin.text);
            appwrapTeenpatti.SendChaalAmount ();
        }
    }

    bool coinValidation () {
        if (GameControllerTeenPatti.BetAmount <= float.Parse (currentPlayer.totalCoin.text)) {
            return true;
        }
        return false;
    }

    public void ShowAction () {
        this.currentAmount = this.minimumBet;
        GameControllerTeenPatti.BetAmount = this.currentAmount;
        if (coinValidation ()) {
            appwrapTeenpatti.SendShowRequest ();
        }
    }

    public void StopTimer () {
        try {
            GameObject.Find ("CurrentPlayerDetails").GetComponent<CurrentPlayerTeenpatti> ().StopTimer ();
        } catch (System.Exception ex) {
            // Debug.Log ("CurrentPlayerDetails finding Excdeption " + ex.Message);
        }
    }

    void onRecivedMassage (string sender, string msg) {
        try {
            JSONNode s = JSON.Parse (msg);
            switch (s[TagsTeenpatti.TAG]) {
                case ServerTagsTeenpatti.WINNER_PLAYER:
                    {
                        ResetGame ();
                    }
                    break;
                case ServerTagsTeenpatti.ROOM_BETTING:
                    {

                        string player_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        if (player_ID.Equals (GameUser.CurrentUser.ID)) {
                            this.previousPlayerId = s[PlayerTagsTeenPatti.PREVIOUS_PLAYER_ID];
                            this.TOTAL_ACTIVE_PLAYERS = int.Parse (s[ServerTagsTeenpatti.TOTAL_ACTIVE_PLAYERS]);
                            currentPlayer.totalCoin.text = float.Parse (s[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
                            this.PreviousPlayerStatus = int.Parse (s[ServerTagsTeenpatti.PREVIOUS_PLAYER_STATUS]);
                            this.stakeAmount = float.Parse (s[ServerTagsTeenpatti.BET_AMOUNT]);
                            if (this.TOTAL_ACTIVE_PLAYERS <= 2) {
                                showTitle.text = "Show";
                                ShowBtn.SetActive (true);
                            } else {
                                if (this.PreviousPlayerStatus == 1) {
                                    ShowBtn.SetActive (false);
                                } else {
                                    if (MyStatus == 2) {
                                        ShowBtn.SetActive (true);
                                        showTitle.text = "Side Show";
                                    }
                                }
                            }
                            updateBetSystem ();
                            ShowMyPanel (Panel);
                        } else {
                            HideMyPanel (Panel);
                        }
                    }
                    break;
                case ServerTagsTeenpatti.COLLECT_BOOT_AMOUNT:
                    {
                        HideMyPanel (Panel);
                        PreviousPlayerStatus = 1;
                        MyStatus = 1;
                    }
                    break;

                case ServerTagsTeenpatti.PLAYER_RESPONDED:
                    {
                        string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        if (My_ID.Equals (GameUser.CurrentUser.ID)) {
                            currentPlayer.totalCoin.text = float.Parse (s[ServerTagsTeenpatti.TOTAL_COINS]).ToString ("F2");
                            currentPlayer.player_coin.GetComponent<Text> ().text = s[ServerTagsTeenpatti.TOTAL_BET_AMOUNT];
                            HideMyPanel (Panel);
                            StopTimer ();
                        }
                    }
                    break;

                case ServerTagsTeenpatti.PLAYER_STATUS_CHANGE:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];
                        string status_player = s[ServerTagsTeenpatti.PLAYER_STATUS];
                        if (My_ID.Equals (GameUser.CurrentUser.ID)) {
                            MyStatus = int.Parse (status_player);
                            updateBetSystem ();
                        }
                        if (My_ID.Equals (this.previousPlayerId)) {
                            this.PreviousPlayerStatus = int.Parse (status_player);
                            if (this.TOTAL_ACTIVE_PLAYERS <= 2) {
                                showTitle.text = "Show";
                                ShowBtn.SetActive (true);
                            } else {
                                if (this.PreviousPlayerStatus == 1) {
                                    ShowBtn.SetActive (false);
                                } else {
                                    if (MyStatus == 2) {
                                        ShowBtn.SetActive (true);
                                        showTitle.text = "Side Show";
                                    }
                                }
                            }
                        }

                    }
                    break;

                case ServerTagsTeenpatti.CARD_SHOW_REQUEST:
                    {

                        string My_ID = s[PlayerTagsTeenPatti.PLAYER_ID];

                        if (My_ID.Equals (GameUser.CurrentUser.ID)) {
                            MyStatus = 2;
                            if (this.TOTAL_ACTIVE_PLAYERS <= 2) {
                                ShowBtn.SetActive (true);
                            } else {
                                if (this.PreviousPlayerStatus == 1) {
                                    ShowBtn.SetActive (false);
                                } else {
                                    if (MyStatus == 2) {
                                        ShowBtn.SetActive (true);
                                    }
                                }
                            }

                            updateBetSystem ();
                        }

                    }
                    break;

                case ServerTagsTeenpatti.CARD_SEEN:
                    {
                        GameControllerTeenPatti.TeenPatti_message = s[ServerTagsTeenpatti.ROOM_MESSAGE];
                        if (GameUser.CurrentUser.ID.Equals (s[PlayerTagsTeenPatti.PLAYER_ID])) {
                            minimumBetTitle.text = "Chal";
                            maximumBetTitle.text = "Chal";
                            if (PreviousPlayerStatus == 1 && this.TOTAL_ACTIVE_PLAYERS > 2) {
                                ShowBtn.SetActive (false);
                            }
                            MyStatus = 2;
                            updateBetSystem ();
                        }
                    }
                    break;
                case ServerTagsTeenpatti.START_NEW_GAME:
                    {
                        try {
                            SideShow.SetActive (false);
                            HideMyPanel (Panel);
                        } catch (System.Exception ex) {
                            // Debug.Log ("START_NEW_GAME Exception " + ex.Message);
                        }

                    }
                    break;

                case ServerTagsTeenpatti.ROOM_INFO:
                    {
                        try {
                            GameControllerTeenPatti.MaxBetAmt = float.Parse (s[TagsTeenpatti.MAX_BET_AMOUNT]);
                            GameControllerTeenPatti.PortLimit = float.Parse (s[TagsTeenpatti.PORT_LIMIT]);
                            GameControllerTeenPatti.BootAmount = float.Parse (s[TagsTeenpatti.BOOT_AMOUNT]);
                            updateBetSystem ();
                        } catch (System.Exception ex) {
                            // Debug.Log ("START_NEW_GAME Exception " + ex.Message);
                        }

                    }
                    break;
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    public void HideMyPanel (GameObject CurrentPanel) {
        CurrentPanel.SetActive (false);
    }

    public void ShowMyPanel (GameObject CurrentPanel) {
        CurrentPanel.SetActive (true);
        //iTween.MoveTo (CurrentPanel, upperPos.position, 1.0f);
    }

    void ResetGame () {
        try {

            this.maximumBet = 0;
            this.minimumBet = 0;
            this.currentAmount = 0;
            minimumBetTitle.text = "Blind";
            maximumBetTitle.text = "Blind";
            HideMyPanel (Panel);
            PackBtn.GetComponent<Button> ().interactable = true;
            ShowBtn.GetComponent<Button> ().interactable = true;
        } catch (System.Exception ex) {
            // Debug.Log (ex);
        }
    }

}