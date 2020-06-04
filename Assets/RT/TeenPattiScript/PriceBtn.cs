using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceBtn : MonoBehaviour {

    public Text PotAmount;
    public Text MaxBet;

    // Start is called before the first frame update
    void Start () {
        AmountSelect (5);
    }
    public void AmountSelect (int value) {
        GameControllerTeenPatti.BootAmount = value;
        GameControllerTeenPatti.MaxBetAmt = (value * 128) / 2;
        GameControllerTeenPatti.PortLimit = (value * 1024) / 2;
        updateBet ();
    }

    void updateBet () {
        MaxBet.text = "" + GameControllerTeenPatti.MaxBetAmt;
        PotAmount.text = "" + GameControllerTeenPatti.PortLimit;
    }

}