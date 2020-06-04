using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void No()
    {
        transform.gameObject.SetActive(false);
    }
    public void Yes()
    {
        Application.Quit();
    }
}

