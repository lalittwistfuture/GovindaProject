using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SPlash : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (PlayerPrefs.GetString (GetPlayerDetailsTags.PLAYER_ID).Length != 0) {
            getUserInfo ();
        } else {
            SceneManager.LoadScene ("LogIn");
        }
    }

    // Update is called once per frame
    void Update () {

    }

    void getUserInfo () {
        WWWForm form = new WWWForm ();
        form.AddField ("TAG", "GET_USERINFO");
        form.AddField ("username", PlayerPrefs.GetString (GetPlayerDetailsTags.PLAYER_ID));
        WebManager.Instance.StartRequest (form, ServerRequest, "SPLASH");
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
                        SceneManager.LoadSceneAsync ("MainLobby");
                    } catch (System.Exception ex) {
                        SceneManager.LoadScene ("LogIn");
                    }
                } else {
                    GameControllerTeenPatti.showToast (node["message"]);
                    SceneManager.LoadScene ("LogIn");
                }
            }
        } catch (System.Exception ex) {
            SceneManager.LoadScene ("LogIn");
        }
    }

}