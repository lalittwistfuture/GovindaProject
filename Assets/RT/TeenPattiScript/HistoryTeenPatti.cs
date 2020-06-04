using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
public class HistoryTeenPatti : MonoBehaviour {
    public GameObject CardRow;
    public GameObject SpaceRow;
    public GameObject roundHeader;
    public GameObject header;
    public Transform Container;
    bool isDetail = false;

    public void closeAction () {
        transform.gameObject.SetActive (false);

    }

    private void Start () {
        //  roundHeader.SetActive(false);
        header.SetActive (false);
        roundHeader.SetActive (false);
    }

    private void OnEnable () {
        serverRequestForGame ();
        GameDelegateTeenPatti.onGameDetail += onGameDetail;
        //  Head();

    }

    private void OnDisable () {
        GameDelegateTeenPatti.onGameDetail -= onGameDetail;
    }

    public void backToGame () {
        if (isDetail) {
            serverRequestForGame ();

        } else {
            transform.gameObject.SetActive (false);
        }

    }

    void onGameDetail (string tableID, string round) {
        serverRequestForGameDetail (tableID, round);
    }
    void clearRow () {
        foreach (Transform t in Container) {
            Destroy (t.gameObject);
        }
    }

    void getResponse (string response) {
        try {

            JSONNode node = JSON.Parse (response);
            isDetail = false;
            if (node != null) {

                string result = node["status"];
                string msg = node["message"];
                if (result.Equals ("OK")) {

                    JSONNode data1 = node["data"];
                    roundHeader.SetActive (true);

                    for (int i = 0; i < data1.Count; i++) {
                        if (i > 0) {
                            JSONNode data = data1[i];
                            GameObject g = Instantiate (SpaceRow);
                            g.transform.position = Vector3.zero;
                            g.transform.SetParent (Container);
                            g.transform.localScale = new Vector3 (1, 1, 1);
                            g.transform.position = Vector3.zero;
                            g.GetComponent<HistoryGame> ().gameID.text = "Round-" + data["game_number"];
                            g.GetComponent<HistoryGame> ().tableID = appwrapTeenpatti.roomID;
                            g.GetComponent<HistoryGame> ().round = data["game_number"];
                        }
                    }

                } else {
                    //loading.SetActive (false);
                    GameControllerTeenPatti.showToast (node["message"]);

                }

            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    void serverRequestForGame () {
        WWWForm form = new WWWForm ();
        form.AddField ("TAG", "GAME_HISTORY_TEEN_PATTI");
        form.AddField ("user_id", GameUser.CurrentUser.ID);
        form.AddField ("table_id", appwrapTeenpatti.roomID);
        WebManager.Instance.StartRequest (form, getResponse, "GAME_HISTORY_TEEN_PATTI");
        clearRow ();
    }

    void getResponseDetail (string response) {
        try {

            JSONNode node = JSON.Parse (response);

            if (node != null) {

                string result = node["status"];
                string msg = node["message"];
                if (result.Equals ("OK")) {

                    JSONNode data1 = node["data"];
                    isDetail = true;
                    roundHeader.SetActive (false);
                    header.SetActive (true);

                    for (int i = 0; i < data1.Count; i++) {
                        JSONNode data = data1[i];
                        GameObject g = Instantiate (CardRow);
                        g.transform.position = Vector3.zero;
                        g.transform.SetParent (Container);
                        g.transform.localScale = new Vector3 (1, 1, 1);
                        g.transform.position = Vector3.zero;
                        g.GetComponent<HistoryRowTeenPatti> ().playerName.text = data["username"];
                        g.GetComponent<HistoryRowTeenPatti> ().playerCoin.text = data["coin"];
                        JSONNode card = JSON.Parse (data["card"]);
                        g.GetComponent<HistoryRowTeenPatti> ().cards[0].sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/" + card[0]);
                        g.GetComponent<HistoryRowTeenPatti> ().cards[1].sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/" + card[1]);
                        g.GetComponent<HistoryRowTeenPatti> ().cards[2].sprite = Resources.Load<Sprite> ("Images_Teenpatti/cards/" + card[2]);

                        g.GetComponent<HistoryRowTeenPatti> ().winImage.SetActive (false);
                        // }

                        // Debug.Log ("cards/" + card[0] + " Total Card " + card);
                    }

                } else {
                    //loading.SetActive (false);
                    GameControllerTeenPatti.showToast (node["message"]);

                }

            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }
    void serverRequestForGameDetail (string table, string round) {
        WWWForm form = new WWWForm ();
        form.AddField ("TAG", "GAME_HISTORY_TEEN_PATTI_DETAIL");
        form.AddField ("user_id", GameUser.CurrentUser.ID);
        form.AddField ("table_id", table);
        form.AddField ("round", round);
        WebManager.Instance.StartRequest (form, getResponseDetail, "GAME_HISTORY_TEEN_PATTI_DETAIL");

        clearRow ();

    }
}