using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuRoulette : MonoBehaviour {

    public GameObject ChangePasswordPanel;
    public GameObject LogoutPanel;

    // Start is called before the first frame update

    public void ChangePasswordAction () {
        ChangePasswordPanel.SetActive (true);
        transform.gameObject.SetActive (false);
    }
    public void LogoutAction () {
        LogoutPanel.SetActive (true);
        transform.gameObject.SetActive (false);
    }

    public void closeMenu () {
        transform.gameObject.SetActive (false);
    }
}