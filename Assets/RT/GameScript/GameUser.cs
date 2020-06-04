using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUser : MonoBehaviour {

    public delegate void RefreshDone ();
    public static event RefreshDone onRefreshDone;
    public void refreshUser () {
        if (onRefreshDone != null) {
            onRefreshDone ();
        }
    }
    private static GameUser _instance;
    public string ID { get; set; }

    //  public string ID { get { return PlayerPrefs.GetString (GetPlayerDetailsTags.PLAYER_ID); } set { PlayerPrefs.SetString (GetPlayerDetailsTags.PLAYER_ID, value); } }
    public string Name { get; set; }
    public string Coin { get; set; }
    public string Pic { get; set; }
    public string Password { get; set; }
    public string ReferalCode { get; set; }
    public string ReferalCoin { get; set; }
    public static GameUser CurrentUser {
        get { return _instance; }
    }
    private void Awake () {
        if (_instance != null && _instance != this) {
            Destroy (this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad (this.gameObject);
    }

    public void Refresh () {
        WWWForm form = new WWWForm ();
        form.AddField ("TAG", "GET_COIN");
        form.AddField ("user_id", ID);
        WebManager.Instance.StartRequest (form, ServerRequest, "REFRESH");
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
                        string appversion = data["app_version"];
                        if (float.Parse (appversion) > float.Parse (Application.version)) {
                            SceneManager.LoadScene ("AppUpdate");
                        }
                        refreshUser ();
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