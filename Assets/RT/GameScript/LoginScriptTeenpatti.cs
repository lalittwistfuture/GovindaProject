using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoginScriptTeenpatti : MonoBehaviour {
    public InputField username;
    public InputField password;
    public GameObject loading;
    private GameObject PopUpContainer;

    // Use this for initialization
    void Start () {
        loading.SetActive (false);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        try {
            PopUpContainer = Instantiate ((GameObject) Resources.Load ("_prefeb/ShowMsgPanel"));
            PopUpContainer.transform.SetParent (transform);
            PopUpContainer.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
            PopUpContainer.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            PopUpContainer.SetActive (false);
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        if (loading.activeSelf) {
            loading.transform.Rotate (Vector3.back, 3, Space.World);
        }

        if (Input.GetKeyDown (KeyCode.Escape)) {
            SceneManager.LoadSceneAsync ("FirstSceneTeenpatti");
        }
    }

    public void CreateAccount () {
        SceneManager.LoadSceneAsync ("Create");
    }

    public void LoginAction () {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            if (validateField ()) {
                WWWForm form = new WWWForm ();
                form.AddField ("TAG", "LOGIN");
                form.AddField ("user_id", username.text);
                form.AddField ("password", password.text);
                WebManager.Instance.StartRequest (form, ServerRequest, "Login");
                loading.SetActive (true);
            } else

            {
                // Debug.Log ("Validation failed.");
            }
        } else {
            PopUpContainer.GetComponent<PopUp> ().msg.GetComponent<Text> ().text = "Internet connection not found. Please try again.";
            PopUpContainer.SetActive (true);
        }

    }

    private void ServerRequest (string response) {
        try {
            JSONNode node = JSON.Parse (response);
            if (node != null) {
                string result = node["status"];
                string msg = node["message"];
                if (result.Equals ("OK")) {
                    JSONNode data1 = node["data"];
                    JSONNode data = data1[0];
                    try {
                        GameUser.CurrentUser.ID = data["user_id"];
                        GameUser.CurrentUser.Name = data["name"];
                        GameUser.CurrentUser.Coin = data["balance"];
                        GameUser.CurrentUser.Pic = data["picture"];
                        GameUser.CurrentUser.Password = data["password"];
                        GameUser.CurrentUser.ReferalCode = data["r_code"];
                        GameUser.CurrentUser.ReferalCoin = data["referral_coin"];
                        PlayerPrefs.SetString ("TOKEN", "" + data["token"]);
                        SceneManager.LoadSceneAsync ("MainLobby");
                    } catch (System.Exception ex) {
                        loading.SetActive (false);
                    }
                } else {
                    GameControllerTeenPatti.showToast (node["message"]);
                    loading.SetActive (false);
                }
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    public bool validateField () {

        if (username.text.Length == 0) {
            PopUpContainer.SetActive (true);
            PopUpContainer.GetComponent<PopUp> ().msg.text = "Please enter your username";
            return false;
        }

        if (password.text.Length == 0) {
            PopUpContainer.GetComponent<PopUp> ().msg.text = "Please enter your password.";
            PopUpContainer.SetActive (true);
            return false;
        }
        return true;
    }

}