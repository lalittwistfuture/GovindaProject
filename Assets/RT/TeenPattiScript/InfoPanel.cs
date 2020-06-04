using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class InfoPanel : MonoBehaviour
{
    public Text boot;
    public Text maxBlind;
    public Text chaalLimit;
    public Text potLimit;

    // Start is called before the first frame update
    void Start()
    {
        boot.text = ""+GameControllerTeenPatti.BootAmount;
        maxBlind.text = ""+4;
        chaalLimit.text = ""+GameControllerTeenPatti.MaxBetAmt;
        potLimit.text = ""+GameControllerTeenPatti.PortLimit;

}

// Update is called once per frame
void Update()
    {
        
    }
    public void Back()
    {
        transform.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        GameDelegateTeenPatti.onRecivedMassage += onRecivedMassage;
    }

    void OnDisable()
    {
        GameDelegateTeenPatti.onRecivedMassage -= onRecivedMassage;
    }

    void onRecivedMassage(string sender, string msg)
    {

      
    }
}

