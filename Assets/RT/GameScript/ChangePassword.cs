using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePassword : MonoBehaviour {
    public InputField CurrentPassword;
    public InputField NewPassword;
    public InputField ConfirmNewPassword;

    private GameObject PopUpContainer;

    // Use this for initialization
    void Start () {

        PopUpContainer = Instantiate ((GameObject) Resources.Load ("_prefeb/ShowMsgPanel"));
        PopUpContainer.transform.SetParent (transform);
        PopUpContainer.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
        PopUpContainer.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        PopUpContainer.GetComponent<PopUp> ().msg.GetComponent<Text> ().text = "Say Hello";
        PopUpContainer.SetActive (false);

    }

    // Update is called once per frame
    void Update () {

    }
    public void ChangePwdBack () {
        transform.gameObject.SetActive (false);
    }

    void FixedUpdate () {

    }

    public void SaveChange () {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            // Debug.Log (" interconnectivity found");
            if (CurrentPwd ()) {
                WWWForm form = new WWWForm ();
                form.AddField ("TAG", "CHANGE_PASSWORD");
                form.AddField ("user_id", GameUser.CurrentUser.ID);
                form.AddField ("newPassword", NewPassword.text);
                WebManager.Instance.StartRequest (form, ServerRequest, "CHANGE PASSWORD");
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
                    GameControllerTeenPatti.showToast ("Your password has been changed successfully");
                    SceneManager.LoadSceneAsync ("LogIn");
                    PlayerPrefs.DeleteAll ();
                } else {
                    GameControllerTeenPatti.showToast (node["message"]);
                }
            }
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        }
    }

    public bool CurrentPwd () {
        if (GameUser.CurrentUser.Password.Equals (CurrentPassword.text)) {
            if (NewPassword.text.Length >= 6) {
                if (!NewPassword.text.Equals (CurrentPassword.text)) {
                    if (NewPassword.text.Equals (ConfirmNewPassword.text)) {

                    } else {
                        GameControllerTeenPatti.showToast ("New Passwords and confirm password doesn't match. Try again");
                        return false;

                    }
                } else {
                    GameControllerTeenPatti.showToast ("New password equals to current password. Try again");
                    return false;

                }
            } else {
                GameControllerTeenPatti.showToast ("New password must be atleast 10 characters. Try again");
                return false;

            }
        } else {
            GameControllerTeenPatti.showToast ("Current password is incorrect. Try again ");
            return false;

        }
        return true;
    }

}