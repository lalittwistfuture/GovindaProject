using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class WebManager : MonoBehaviour {
    private static WebManager _instance;

    public delegate void RequestData (string responsecode);
    public static WebManager Instance {
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

    public string getURL () {
        return "http://govinda16.online/public/v1.1/index.php";
    }
    public void StartRequest (WWWForm form, RequestData requestedData, string Tag) {
        form.AddField ("key", "152111356fde460c3ae71553434534fs");
        form.AddField ("gameVersion", Application.version);
        form.AddField ("device", SystemInfo.deviceUniqueIdentifier);
        form.AddField ("token", PlayerPrefs.GetString ("TOKEN"));
        UnityWebRequest w = UnityWebRequest.Post (getURL (), form);
        StartCoroutine (ServerRequest (w, requestedData, Tag));
    }

    private IEnumerator ServerRequest (UnityWebRequest www, RequestData requestedData, string Tag) {
        yield return www.SendWebRequest ();
        if (www.error == null) {
            string response = www.downloadHandler.text;
             Debug.Log (Tag + " : " + response);
            if (requestedData != null) {
                JSONNode data = JSONNode.Parse (response);
                string result = data["status"];
                if (result.Equals ("NOT_VALID")) {
                    SceneManager.LoadScene ("LogIn");
                } else {
                    requestedData (response);
                }
            }
        } else {
            GameController.showToast ("response " + www.error.ToString ());
        }

    }
}