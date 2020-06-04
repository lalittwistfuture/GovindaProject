using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoutPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            No();
        }
    }
    public void No()
    {
        transform.gameObject.SetActive(false);
    }
    public void Yes()
    {
        SceneManager.LoadSceneAsync("LogIn");
        PlayerPrefs.DeleteAll();
    }
}
