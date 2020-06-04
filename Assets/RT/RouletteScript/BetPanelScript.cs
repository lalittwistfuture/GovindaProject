using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;
namespace Roullet
{
    public class BetPanelScript : MonoBehaviour
    {
        public RoulleteManager roulleteManager;
        private GameObject[] BetButton;
        private GameObject[] optButton;
        private GameObject chipSample;
        private int row, column = 0;
        private int[,] NumberArray = new int[3, 12];
        public List<GameObject> chipSmaples;
        private Side side = Side.none;
        private List<int> SelectedNumber;
        private Vector3 chipPosition = Vector3.zero;
        private bool Enable = false;

        public void ClearBet()
        {
            if (Enable)
            {
                clearTable();
                RouletteDelegate.removeAllBet();
            }
        }

        void clearTable()
        {
            foreach (GameObject btn in chipSmaples)
            {
                Destroy(btn);
            }
            foreach (GameObject btn in BetButton)
            {
                Color c = btn.transform.GetChild (0).GetComponent<Image>().color;
                c.a = 0.0f;
                btn.transform.GetChild (0).GetComponent<Image>().color = c;
            }
            chipSmaples.Clear();
        }


        void OnEnable()
        {

            RouletteDelegate.onCellSelected += onCellSelected;
            RouletteDelegate.onClearBet += clearTable;
            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
        }

        void OnDisable()
        {
            RouletteDelegate.onCellSelected -= onCellSelected;
            RouletteDelegate.onClearBet -= clearTable;
            RouletteDelegate.onWarpChatRecived -= onWarpChatRecived;
        }

        void onWarpChatRecived(string sender, string message)
        {
            JSONNode s = JSON.Parse(message);


            switch (s[RouletteTag.TAG])
            {

                case RouletteTag.BETTING_START:
                    {
                        Enable = true;
                        PlayerPrefs.SetInt(RouletteTag.BET_ENABLE, RouletteTag.ENABLE);
                        PlayerPrefs.SetInt(RouletteTag.TOTAL_BET, 0);
                    }
                    break;
                case RouletteTag.BETTING_STOP:
                    {
                        Enable = false;
                        PlayerPrefs.SetInt(RouletteTag.BET_ENABLE, RouletteTag.DISABLE);
                    }
                    break;
                case RouletteTag.TIME:
                    {
                        Enable = false;
                        PlayerPrefs.SetInt(RouletteTag.BET_ENABLE, RouletteTag.DISABLE);
                        try
                        {
                            if (int.Parse(s[RouletteTag.VALUE]) >= 1)
                            {
                                Enable = true;
                                PlayerPrefs.SetInt(RouletteTag.BET_ENABLE, RouletteTag.ENABLE);
                            }

                        }
                        catch (System.Exception ex)
                        {
                            // Debug.Log(ex.Message);
                        }
                    }
                    break;

            }
        }


        void onCellSelected(int r, int c, Side s)
        {
            if (Enable)
            {
                if (PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin) > 0 && (float.Parse(GameUser.CurrentUser.Coin) >= PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin)))
                {
                    this.SelectedNumber.Clear();
                    if (PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin) > 0)
                    {
                        this.row = r;
                        this.column = c;
                        this.side = s;
               
                        switch (this.side)
                        {
                            case Side.none:
                                {
                                    this.chipPosition = getNumber(this.row, this.column);
                                }
                                break;
                            case Side.top:
                                {
                                if (this.row == 2)
                                {
                                    Vector3 v1 = getNumber(this.row, this.column);
                                    Vector3 v2 = getNumber(this.row - 1, this.column);
                                    getNumber(this.row - 2, this.column);
                                    this.chipPosition = new Vector3(v1.x,v1.y-(v2.y - v1.y) / 2, v1.z);
                                }
                                }
                                break;
                            case Side.bottom:
                                {
                                    if (this.row == 0)
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row + 1, this.column);
                                        getNumber(this.row + 2, this.column);
                                        this.chipPosition = new Vector3(v1.x, v1.y - (v2.y - v1.y) / 2, v1.z);
                                    }
                                    else
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row - 1, this.column);
                                        this.chipPosition = new Vector3(v1.x, (v1.y + v2.y) / 2, v1.z);
                                    }
                                }
                                break;
                            case Side.left:
                                {
                                if (this.column==0)
                                {
                                    this.chipPosition = getNumber(this.row, this.column);
                                }
                                else
                                {
                                    Vector3 v1 = getNumber(this.row, this.column);
                                    Vector3 v2 = getNumber(this.row, this.column - 1);
                                    this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y, v1.z);
                                }
                                }
                                break;
                            case Side.right:
                                {
                                    if (this.column == 11)
                                    {
                                        this.chipPosition = getNumber(this.row, this.column);
                                    }
                                    else
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column + 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y, v1.z);
                                    }
                                }
                                break;
                            case Side.top_Left:
                                {
                                    if (this.row == 2)
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column - 1);
                                        Vector3 v3 = getNumber(this.row - 1, this.column);
                                        getNumber(this.row - 1, this.column - 1);
                                        getNumber(this.row - 2, this.column);
                                        getNumber(this.row - 2, this.column - 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y - (v3.y - v1.y) / 2, v1.z);
                                    }
                                    else
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column - 1);
                                        Vector3 v3 = getNumber(this.row + 1, this.column);
                                        getNumber(this.row + 1, this.column - 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, (v1.y + v3.y) / 2, v1.z);
                                    }
                                }
                                break;

                            case Side.top_Right:
                            {
                                    if (this.row==2)
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column + 1);
                                        Vector3 v3 = getNumber(this.row - 1, this.column);
                                        getNumber(this.row - 1, this.column + 1);
                                        getNumber(this.row - 2, this.column);
                                        getNumber(this.row - 2, this.column + 1);

                                    this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y-(v3.y - v1.y) / 2, v1.z);
                                    }
                                    else
                                    { 
                                        
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column + 1);
                                        Vector3 v3 = getNumber(this.row + 1, this.column);
                                        getNumber(this.row + 1, this.column + 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, (v1.y + v3.y) / 2, v1.z);
                                     }
                                }
                                break;
                            case Side.bottom_Left:
                                {
                                    if (this.row == 0)
                                    {                                       
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column - 1);
                                        Vector3 v3 = getNumber(this.row + 1, this.column);
                                        getNumber(this.row + 1, this.column - 1);
                                        getNumber(this.row + 2, this.column);
                                        getNumber(this.row + 2, this.column - 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y - (v3.y - v1.y) / 2, v1.z);
                                    }
                                    else
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column - 1);
                                        Vector3 v3 = getNumber(this.row - 1, this.column);
                                        getNumber(this.row - 1, this.column - 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, (v1.y + v3.y) / 2, v1.z);
                                    }
                                }
                                break;
                            case Side.bottom_Right:
                                {
                                    if (this.row == 0)
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column + 1);
                                        Vector3 v3 = getNumber(this.row + 1, this.column);
                                        getNumber(this.row + 1, this.column + 1);
                                        getNumber(this.row + 2, this.column);
                                        getNumber(this.row + 2, this.column + 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, v1.y - (v3.y - v1.y) / 2, v1.z);
                                      
                                }
                                    else
                                    {
                                        Vector3 v1 = getNumber(this.row, this.column);
                                        Vector3 v2 = getNumber(this.row, this.column + 1);
                                        Vector3 v3 = getNumber(this.row - 1, this.column);
                                        getNumber(this.row - 1, this.column + 1);
                                        this.chipPosition = new Vector3((v1.x + v2.x) / 2, (v1.y + v3.y) / 2, v1.z);
                                    }
                                }
                                break;

                        }
                        addChips();
                    }
                    else
                    {
                        print("Not have Sufficient Coin");
                        GameController.showToast("Alert! you don't have sufficient coins");
                }
                }
                else
                {
                    GameController.showToast("Alert! Please select coin before placing Bets!");
                    print("Please select Coin First ");

                }
            }
        }


        bool isBetAllow()
        {
            return false;
        }


        void Start()
        {

           
            chipSample = GameObject.Find("CoinSample");
            BetButton = GameObject.FindGameObjectsWithTag("betButton");
            optButton = GameObject.FindGameObjectsWithTag("optionBtn");
            chipSmaples = new List<GameObject>();
            PlayerPrefs.SetInt(RouletteTag.TOTAL_BET, 0);
            this.SelectedNumber = new List<int>();
            int value = 1;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.NumberArray[j, i] = value;
                    value++;
                }
            }
            foreach (GameObject btn in BetButton)
            {
                Color c = btn.transform.GetChild (0).GetComponent<Image>().color;
                
                c.a = 0.0f;
                btn.transform.GetChild (0).GetComponent<Image>().color = c;
            }
            foreach (GameObject btn in optButton)
            {
                Color c = btn.GetComponent<Image>().color;
                c.a = 0.0f;
                btn.GetComponent<Image>().color = c;
                btn.GetComponent<Button>().onClick.AddListener(() => TaskOnClick(btn));
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Debug.Log("NUmber " + this.NumberArray[j, i]);
                    GameObject.Find("" + this.NumberArray[j, i]).GetComponent<BetCell>().setIndexValue(i, j);
                }
            }

        }


        Vector3 getNumber(int row, int column)
        {
            try
            {
                foreach (GameObject btn in BetButton)
                {
                    if (btn.name.Equals("" + this.NumberArray[row, column]))
                    {
                        Color c = btn.transform.GetChild (0).GetComponent<Image>().color;
                        c.a = 1.0f;
                        this.SelectedNumber.Add(int.Parse(btn.name));
                        btn.transform.GetChild (0).GetComponent<Image>().color = c;
                        return btn.transform.position;
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
            return Vector3.zero;
        }

        void TaskOnClick(GameObject btn)
        {
            if (Enable)
            {
               
                if (PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin) > 0 && (float.Parse(GameUser.CurrentUser.Coin) >= PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin)))
                {
                    if (float.Parse(GameUser.CurrentUser.Coin) >= PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin))
                    {
                        if (PlayerPrefs.GetFloat(RouletteTag.RouletteSelectedCoin) >= 5|| btn.transform.name=="0")
                        {
                            GameObject chip = Instantiate(chipSample);
                            // chip.transform.SetParent(btn.transform);
                             chip.transform.position = btn.transform.position;
                            // chip.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                            // chip.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString(RouletteTag.RouletteCoinImage));

                            RouletteDelegate.optionClick(btn.transform.name, chip,PlayerPrefs.GetString(RouletteTag.RouletteCoinImage));
                            chipSmaples.Add(chip);
                        }
                    }
                    else
                    {
                        print("Not have sufficient coin");
                    }
                }
                else
                {
                    print("Please select Coin First ");
                }
            }
        }

        void addChips()
        {
            // Debug.Log("Add chip");
            GameObject chip = Instantiate(chipSample);
            // chip.transform.SetParent(transform);
             chip.transform.position = this.chipPosition;
            // chip.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            // chip.GetComponent<Image>().raycastTarget = false;
            // chip.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString(RouletteTag.RouletteCoinImage));
            chipSmaples.Add(chip);
            RouletteDelegate.numberClick(this.SelectedNumber, chip,PlayerPrefs.GetString(RouletteTag.RouletteCoinImage));
        }
    }
}





