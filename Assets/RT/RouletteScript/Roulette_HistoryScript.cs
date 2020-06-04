using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace Roullet
{
    public class Roulette_HistoryScript : MonoBehaviour
    {

        // Use this for initialization

        private GameObject[] HistoryCell;
        private GameObject[] HotCell;
        private GameObject[] ColdCell;

        int[] blackNumberSet = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        int[] redNumberSet = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };


        void Start()
        {


            HistoryCell = new GameObject[36];

            for (int i = 0; i < HistoryCell.Length; i++)
            {
                HistoryCell[i] = GameObject.Find("cell (" + i + ")");
            }
            HotCell = new GameObject[6];
            for (int i = 0; i < HotCell.Length; i++)
            {
                HotCell[i] = GameObject.Find("Hotcell (" + i + ")");
            }
            ColdCell = new GameObject[6];
            for (int i = 0; i < ColdCell.Length; i++)
            {
                ColdCell[i] = GameObject.Find("Coldcell (" + i + ")");
            }


        }
        /*void OnEnable ()
        {    // Debug.Log ("Enable History Panel");
            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
        }

        void OnDisable ()
        {
            RouletteDelegate.onWarpChatRecived -= onWarpChatRecived;
        }

        void onWarpChatRecived (string sender, string message)
        {
            JSONNode s = JSON.Parse (message);

            switch (s [RouletteTag.TAG]) {

            case RouletteTag.MOVE_TO_TABLE:
                {
                    JSONNode Number = JSON.Parse(s ["PREVIOUS_NUMBER"]);
                    ManageHistoryPanels (Number);
                }
                break;
            case RouletteTag.TABLE_STATUS:
                {
                    JSONNode Number = JSON.Parse(s ["PREVIOUS_NUMBER"]);
                    ManageHistoryPanels (Number);
                }
                break;
            }
        }
    */
        public void ManageHistoryPanels(JSONNode Number)
        {
            setHistoryPanelNumber(Number);
            setHotPanelNumber(Number);
            setColdPanelNumber(Number);

        }

        void setHistoryPanelNumber(JSONNode Number)
        {

            for (int i = 0; i < HistoryCell.Length; i++)
            {
                try
                {
                    if (Number.Count - (i + 1) >= 0)
                    {
                        if (HistoryCell[i] != null)
                        {
                            int number = int.Parse("" + Number[Number.Count - (i + 1)]);
                            HistoryCell[i].transform.GetChild(0).GetComponent<Text>().text = "" + number;
                            HistoryCell[i].GetComponent<Image>().color = getColor(number);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    // Debug.Log(ex.Message);
                }
            }

        }



        List<int> getNumberWithFrequency(JSONNode Number)
        {
            List<int> Pre = new List<int>();
            try
            {
                for (int i = 0; i <= 36; i++)
                {
                    int count = 0;
                    for (int j = 0; j < Number.Count; j++)
                    {
                        if (i == int.Parse(Number[j]))
                        {
                            count++;
                        }
                    }
                    Pre.Add(count);
                }
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
            return Pre;
        }



        void setHotPanelNumber(JSONNode Number)
        {
            List<int> Pre = getNumberWithFrequency(Number);
            for (int i = 0; i < HotCell.Length; i++)
            {
                try
                {
                    if (HotCell[i] != null)
                    {
                        int max = Pre[0];
                        int num = 0;
                        for (int j = 1; j <= 36; j++)
                        {
                            if (max < Pre[j])
                            {
                                max = Pre[j];
                                num = j;
                            }
                        }
                        HotCell[i].transform.GetChild(0).GetComponent<Text>().text = "" + num;
                        HotCell[i].GetComponent<Image>().color = getColor(num);
                        Pre[num] = -1;
                    }
                }
                catch (System.Exception ex)
                {
                    // Debug.Log(ex.Message);
                }
            }

        }

        void setColdPanelNumber(JSONNode Number)
        {
            List<int> Pre = getNumberWithFrequency(Number);
            for (int i = 0; i < ColdCell.Length; i++)
            {
                try
                {
                    if (ColdCell[i] != null)
                    {
                        int max = Pre[0];
                        int num = 0;
                        for (int j = 1; j <= 36; j++)
                        {
                            if (max > Pre[j])
                            {
                                max = Pre[j];
                                num = j;
                            }
                        }
                        ColdCell[i].transform.GetChild(0).GetComponent<Text>().text = "" + num;
                        ColdCell[i].GetComponent<Image>().color = getColor(num);
                        Pre[num] = 1000000;
                    }
                }
                catch (System.Exception ex)
                {
                    // Debug.Log(ex.Message);
                }
            }

        }

        Color getColor(int number)
        {

            for (int i = 0; i < blackNumberSet.Length; i++)
            {
                if (blackNumberSet[i] == number)
                {
                    return Color.black;
                }
            }
            for (int i = 0; i < redNumberSet.Length; i++)
            {
                if (redNumberSet[i] == number)
                {
                    return Color.red;
                }
            }
            return new Color(0.02f, 0.5f, 0.14f, 1.0f);
        }

    }

}