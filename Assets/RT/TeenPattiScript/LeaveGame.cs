using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LeaveGame : MonoBehaviour {
    public Text QuitGameText;

    void OnEnable () {
        QuitGameText.text = MessageScriptTeenPatti.QUIT_GAME_MESSAGE;
    }

    public void ClosePanel () {
        transform.gameObject.SetActive (false);
    }

    public void YesAction () {

        try {
            appwrapTeenpatti.LeftRoom ();
            appwrapTeenpatti.Disconnect ();
        } catch (System.Exception ex) {
            // Debug.Log (ex.Message);
        } finally {
            transform.gameObject.SetActive (false);
            SceneManager.LoadScene ("MainLobby");
        }
    }
}