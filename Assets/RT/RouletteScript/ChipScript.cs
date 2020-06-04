using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Roullet
{
    public class ChipScript : MonoBehaviour
    {

        private GameObject[] BetButton;

        void Start()
        {
            BetButton = GameObject.FindGameObjectsWithTag("chipCoin");
            PlayerPrefs.SetFloat(RouletteTag.RouletteSelectedCoin, 0);
            foreach (GameObject btn in BetButton)
            {
                btn.GetComponent<Button>().onClick.AddListener(() => TaskOnClick(btn));
                //btn.getcom.onClick.AddListener(TaskOnClick);
            }
        }

        void TaskOnClick(GameObject btn)
        {
        if ( float.Parse(GameUser.CurrentUser.Coin) >= int.Parse(btn.transform.name))
        {
            foreach (GameObject btn1 in BetButton)
            {
                //btn1.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
                iTween.ScaleTo(btn1, new Vector3(1.0f, 1.0f, 1.0f), 0.5f);
                //btn.getcom.onClick.AddListener(TaskOnClick);
                // Debug.Log("1");
            }
            iTween.ScaleTo(btn, new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
            // Debug.Log("2");

            //btn.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            print("Chip " + btn.transform.name);
            PlayerPrefs.SetFloat(RouletteTag.RouletteSelectedCoin, float.Parse(btn.transform.name));
           
                switch (float.Parse(btn.transform.name))
                {
                    case 5000:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c7");
                        }
                        break;
                    case 2000:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c6");
                        }
                        break;    
                    case 1000:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c5");
                        }
                        break;
                    case 500:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c4");
                        }
                        break;
                    case 100:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c3");
                        }
                        break;
                    case 50:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c2");
                        }
                        break;
                    case 20:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "c1");
                        }
                        break;
                    /*case 10:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettec2");
                        }
                        break;
                    case 5:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettec1");
                        }
                        break;
                    case 1:
                        {
                            PlayerPrefs.SetString(RouletteTag.RouletteCoinImage, "Roulettechip");
                        }
                        break;*/
                }
            }
            else
            {
                GameController.showToast("Alert! you don't have sufficient coins");
                // Debug.Log("Alert! you don't have sufficient coins");
            }
        }

    }
}