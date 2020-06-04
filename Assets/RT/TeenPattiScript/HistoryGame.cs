using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
public class HistoryGame : MonoBehaviour
{
    public Text gameID;
    public string tableID;
    public string round;
    public void openGameID()
    {
        GameDelegateTeenPatti.gameDetail(tableID, round);
    }


}
