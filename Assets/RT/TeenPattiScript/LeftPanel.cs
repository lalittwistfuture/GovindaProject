using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanel : MonoBehaviour
{
    public GameObject QuitPanel;
    // Start is called before the first frame update
    void Start()
    {
        QuitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close()
    {
        transform.gameObject.SetActive(false);
    }
    public void Back()
    {
        try
        {
            QuitPanel.SetActive(true);
            transform.gameObject.SetActive(false);

        }
        catch (System.Exception ex)
        {
            // Debug.Log(ex.Message);
        }
    }
    public void StandUp()
    {
        appwrapTeenpatti.SendStandUp();
        transform.gameObject.SetActive(false);
    }
}
