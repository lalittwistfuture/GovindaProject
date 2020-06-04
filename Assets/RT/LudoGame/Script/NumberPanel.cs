using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate
{
    public class NumberPanel : MonoBehaviour
    {

        public GameObject[] Button;
        public GameObject Panel;
        public GameObject arrow;
        private int GotiNumber;
        void Start()
        {
            onHideButtonPanel();
        }

        public void OnEnable()
        {
            GameDelegate.onRecivedMassage += onRecivedMassage;
            GameDelegate.onHideButtonPanel += onHideButtonPanel;
            GameDelegate.onShowButtonPanel += onShowButtonPanel;
        }

        public void OnDisable()
        {
            GameDelegate.onRecivedMassage -= onRecivedMassage;
            GameDelegate.onHideButtonPanel -= onHideButtonPanel;
            GameDelegate.onShowButtonPanel -= onShowButtonPanel;
        }


        public void numberSelection(GameObject btn)
        {
            try
            {
                int number = int.Parse(btn.transform.GetChild(0).GetComponent<Text>().text);
                GameDelegate.stopAnimation();
                GameDelegate.selectNumber(number);
                this.onHideButtonPanel();
                appwarp.userSelectGoti(this.GotiNumber, number);
                GameDelegate.StartDisableAllGoti();
            }
            catch (System.Exception ex)
            {
                // Debug.Log (ex.Message);
            }
        }
        void onRecivedMassage(string sender, string msg)
        {
            try
            {
                JSONNode s = JSON.Parse(msg);


                switch (s[ServerTags.TAG])
                {

                    case ServerTags.TURN:
                        {
                            onHideButtonPanel();

                        }
                        break;
                }
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
        }
        public void onShowButtonPanel(JSONNode TurnValue, GameObject goti)
        {
            // Debug.Log ("Show Panel " + TurnValue.ToString ());
            this.GotiNumber = goti.GetComponent<GotiScript>().index;
            Panel.SetActive(true);
            for (int i = 0; i < TurnValue.Count; i++)
            {
                Button[i].SetActive(true);
                Button[i].transform.GetChild(0).GetComponent<Text>().text = TurnValue[i];
            }
            int Position = GotiScript.getGotiPosition(goti);
            switch (Position)
            {
                case GotiScript.LEFT:
                    if (TurnValue.Count == 2)
                    {
                        Panel.transform.localPosition = new Vector3(32.0f, Panel.transform.localPosition.y, 0.0f);
                    }
                    else
                    {
                        Panel.transform.localPosition = new Vector3(60.0f, Panel.transform.localPosition.y, 0.0f);
                    }
                    break;
                case GotiScript.RIGHT:
                    if (TurnValue.Count == 2)
                    {
                        Panel.transform.localPosition = new Vector3(-32.0f, Panel.transform.localPosition.y, 0.0f);
                    }
                    else
                    {
                        Panel.transform.localPosition = new Vector3(-60.0f, Panel.transform.localPosition.y, 0.0f);
                    }
                    break;

                default:
                    Panel.transform.localPosition = new Vector3(0.0f, Panel.transform.localPosition.y, 0.0f);
                    break;
            }
            arrow.SetActive(true);
            transform.position = goti.transform.position;
        }

        public void onHideButtonPanel()
        {
            // Debug.Log ("Hide Panel ");
            for (int i = 0; i < 3; i++)
            {
                Button[i].SetActive(false);

            }
            Panel.SetActive(false);
            arrow.SetActive(false);
        }

    }
}