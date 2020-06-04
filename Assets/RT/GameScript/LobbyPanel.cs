using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
    public Text Name;
    public Text coin;
    public GameObject image;

    public GameObject SettingPanel;
    public GameObject QuitPanel;
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    void Start()
    {
        QuitPanel.SetActive(false);

    }
    private void OnEnable()
    {
        DisplayUserInfo();
        GameUser.onRefreshDone += onRefreshDone;
        GameUser.CurrentUser.Refresh();
    }
    void DisplayUserInfo()
    {
        try
        {
            Name.text = GameUser.CurrentUser.Name;
            coin.text = float.Parse(GameUser.CurrentUser.Coin).ToString("F2");
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Avtar/" + GameUser.CurrentUser.Pic);
            PlayerPrefs.SetInt("Sound", 1);
        }
        catch (System.Exception ex)
        {
            // Debug.Log (ex.Message);
        }
    }
    void OnDisable()
    {
        GameUser.onRefreshDone -= onRefreshDone;
    }
    void onRefreshDone()
    {
        DisplayUserInfo();
    }
    public void PlayNow()
    {
        SceneManager.LoadSceneAsync("TeenPattiLobby");
    }
    public void Roulette()
    {
        SceneManager.LoadScene("Roulette_GameScene");
    }

    public void Ludo()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void SettingPanelAction()
    {
        SettingPanel.SetActive(true);
    }

    public void GameQuit()
    {
        QuitPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }

    }
}