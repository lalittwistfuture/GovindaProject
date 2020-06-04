using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate
{


    public class OpponentPlayer : BasePlayer
    {
        public bool isRotateDice = false;
        public GameObject DiceHolder;
        public static bool isOpponentActive = false;

        void Start()
        {
            base.listener = GameObject.Find("Canvas").GetComponent<Listener>();
            //base.userName.text = "Computer";
            DiceHolder.SetActive(true);
            TimerBg.SetActive(false);
            ChatBox.SetActive(false);
            Emoji.SetActive(false);
            //showArrow.SetActive (false);
            //base.setColor (LudoColor.Yellow);
            for (int i = 0; i < base.goti.Length; i++)
            {
                base.goti[i].GetComponent<GotiScript>().playerName = base.Player_ID;
                base.goti[i].GetComponent<GotiScript>().index = i + 1;
                //	base.goti [i].SetActive (false);
            }

            if (PlayerImageUrl.Length != 0)
            {
                if (PlayerImageUrl.Contains("AVTAR"))
                {
                    string[] img = PlayerImageUrl.Split(new string[] { "-" }, System.StringSplitOptions.None);
                    base.Image.sprite = Resources.Load<Sprite>("Avtar/" + img[1]);
                }
                else
                {
                    StartCoroutine(loadImage(PlayerImageUrl));
                }
            }
            else
            {
                // Debug.Log ("profilePic null");
                base.Image.sprite = Resources.Load<Sprite>("images/user-default");
            }

            base.color = LudoColor.Yellow;
            base.updateData();

        }



        // Update is called once per frame
        void Update()
        {
            if (base.isTimeOn)
            {

                /*
                float remain = GameConstantData.countTimer - time;	 
                float percentage = remain * 100 / GameConstantData.countTimer;
                percentage = percentage / 100;
                TimerImage.GetComponent<Image> ().fillAmount = 1 - percentage;
                time -= Time.deltaTime;
                if (time <= 0) {
                    DiceHolder.SetActive (true);
                    base.isTimeOn = false;
                    isOpponentActive = false;
                }
                */

            }

        }

        void FixedUpdate()
        {
            if (isRotateDice)
            {
                try
                {

                    int month = Random.Range(1, ImageController.getInstance.DiceRotation.Length);

                    // PlayerDice.GetComponent<Image>().sprite = Resources.Load<Sprite>("Dices/d-" + month);
                    PlayerDice.GetComponent<Image>().sprite = ImageController.getInstance.DiceRotation[month - 1];
                    PlayerDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                catch (System.Exception ex)
                {
                    // Debug.Log ("FixedUpdate Exception "+ex.Message);
                }

                /*
                int index = (Time.deltaTime) % frame.Length;
                GameObject g  = frame[index];
                GameObject newCell = Instantiate (g);
                newCell.transform.SetParent (Container.transform);
                newCell.transform.localScale = new Vector3 (1, 1, 1);

                newCell.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("images/dice_" + month);
                */
            }
        }


        IEnumerator loadImage(string url)
        {
            // Debug.Log ("loadImage url "+url);
            WWW www = new WWW(url);
            yield return www;
            if (www.error == null)
            {

                // Debug.Log ("loadImage url12775675 "+url);
                base.Image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
                // Debug.Log (" download done  "+url);
            }
            else
            {
                // Debug.Log ("Error occur while downloading");
                base.Image.sprite = Resources.Load<Sprite>("images/user-default");


            }


        }

        public override void activeDice()
        {
            DiceHolder.SetActive(true);

        }



        public override void onNumberSelection(int index)
        {
        }

        public override void readyGoti(int position)
        {

        }

        public override void TurnPlayer()
        {
            base.setColor(LudoColor.Red);
            DiceHolder.SetActive(true);
            time = GameConstantData.countTimer;
            //TimerImage.GetComponent<Image> ().fillAmount = 1.0f;
            isTimeOn = true;
            //showArrow.SetActive (false);
        }

        public override void diceRollDone(int value, JSONNode TurnValue)
        {
            if (base.isTimeOn)
            {
                //showArrow.SetActive (false);
                isRotateDice = true;
                isOpponentActive = true;
                StartCoroutine(stopRotation(value, TurnValue));

                GameDelegate.showRollDice("" + value);
                //iTween.ScaleTo (PlayerDice, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f, 1.5f), "speed", 8.0f, "easetype", "linear"));
            }
        }

        public void TapDiceAction()
        {

        }

        //	public void showPlayerProfile(){
        //		// Debug.Log ("showPlayerProfile working");
        //		ProfilePanel.SetActive (true);
        //
        //	}

        IEnumerator stopRotation(int value, JSONNode TurnValue)
        {
            yield return new WaitForSeconds(0.5f);
            isRotateDice = false;
            try
            {
                PlayerDice.GetComponent<Image>().sprite = ImageController.getInstance.Dice[value - 1];
                //PlayerDice.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Dices/dice_" + value);
                PlayerDice.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                PlayerDice.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            }
            catch (System.Exception ex)
            {
                // Debug.Log ("stopRotation Exception "+ex.Message);
            }
            base.resetDices();
            // Debug.Log("Opponent "+TurnValue.ToString());
            for (int i = 0; i < TurnValue.Count; i++)
            {
                try
                {
                    diceSample[i].SetActive(true);
                    diceSample[i].GetComponent<DiceSample>().index = int.Parse(TurnValue[i]);
                    diceSample[i].GetComponent<Image>().sprite = ImageController.getInstance.Dice[int.Parse(TurnValue[i]) - 1];
                }
                catch (System.Exception ex)
                {
                    // Debug.Log(ex.Message);
                }
            }

            //print (" " + base.dicNumber + " Occured");
        }
    }
}