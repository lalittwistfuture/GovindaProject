using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeenPattiPrivate : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //Screen.orientation = ScreenOrientation.Portrait;
        GameControllerTeenPatti.isChallenge = true;
        GameControllerTeenPatti.variation = true;
        GameControllerTeenPatti.GameType = TagsTeenpatti.PRIVATE;
    }

    private void OnEnable () {
        GameControllerTeenPatti.isChallenge = true;
        GameControllerTeenPatti.variation = true;
        GameControllerTeenPatti.GameType = TagsTeenpatti.PRIVATE;

    }

    public void createTableAction () {
        GameControllerTeenPatti.PrivateGameType = TagsTeenpatti.CREATE_PRIVATE_TABLE;
        SceneManager.LoadSceneAsync ("AmountSelectionTeenpatti");
    }

    public void joinTableAction () {
        GameControllerTeenPatti.PrivateGameType = TagsTeenpatti.JOIN_PRIVATE_TABLE;

        SceneManager.LoadSceneAsync ("GameScene_Teenpatti");
    }

}