using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace LudoGameTemplate
{
    public class PlayerScript : BasePlayer
    {

        //public static bool isTurnOn = false;
        public bool isRotateDice = false;
        public GameObject DiceHolder;
        public GameObject LudoDice;
        // Use this for initialization


        public GameObject DiceBtn;

        public GameObject ChatBtn;
        public GameObject EmojiBtn;

        public GameObject ChatPanel;
        public GameObject ClosePanel;
        public GameObject EmojiPanel;
        public GameObject BoosterPanel;

        void Start()
        {
            base.listener = GameObject.Find("Canvas").GetComponent<Listener>();
            DiceHolder.SetActive(true);
            showArrow.SetActive(false);
            TimerBg.SetActive(false);
            ChatBox.SetActive(false);
            ChatPanel.SetActive(false);
            EmojiPanel.SetActive(false);
            ClosePanel.SetActive(false);

            EmojiBtn.SetActive(false);
            ChatBtn.SetActive(false);
            Emoji.SetActive(false);

            DiceBtn.GetComponent<Button>().onClick.AddListener(TapDiceAction);
            ChatBtn.GetComponent<Button>().onClick.AddListener(startChatting);
            EmojiBtn.GetComponent<Button>().onClick.AddListener(SendEmoji);
            ClosePanel.GetComponent<Button>().onClick.AddListener(HideChatPanel);

            if (GameConstantData.GameType != GameConstantData.Practice)
            {
                ChatBtn.SetActive(true);
                EmojiBtn.SetActive(true);
            }





            base.setColor(LudoColor.Red);
            //base.userName.text = UserController.getInstance.Name;

            try
            {
                if (UserController.getInstance.Name.Length != 0)
                {
                    string capsName = UppercaseFirst(UserController.getInstance.Name);
                    base.userName.text = capsName;
                }
            }
            catch (System.Exception ex)
            {
                // Debug.Log (ex.Message);
            }

            string playerImage = UserController.getInstance.Image;
            if (playerImage.Length != 0)
            {
                if (playerImage.Contains("AVTAR"))
                {
                    string[] img = playerImage.Split(new string[] { "-" }, System.StringSplitOptions.None);
                    // Debug.Log ("bdhvhjbds jndscjn "+img [1]);
                    base.Image.sprite = Resources.Load<Sprite>("Avtar/" + img[1]);
                }
                else
                {
                    StartCoroutine(loadImage(playerImage));
                }
            }
            else
            {
                // Debug.Log ("profilePic null");
                base.Image.sprite = Resources.Load<Sprite>("images/user-default");
            }
            for (int i = 0; i < base.goti.Length; i++)
            {
                base.goti[i].GetComponent<GotiScript>().playerName = UserController.getInstance.ID;
                base.goti[i].GetComponent<GotiScript>().index = i + 1;
                base.goti[i].SetActive(false);
            }

            //diceSample = GameObject.FindGameObjectsWithTag ("redDices");


            base.color = LudoColor.Red;
            base.updateData();
            // StartCoroutine(sendData());
        }

        IEnumerator sendData()
        {
            while (true)
            {
                int[] data_f = new int[3];
                data_f[2] = 8;
                int data_len = (sizeof(int) * 3) + (SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
                byte[] data = new byte[data_len];
                System.Buffer.BlockCopy(data_f, 0, data, 0, sizeof(int) * 3);
                System.Buffer.BlockCopy(SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).ToCharArray(), 0, data, sizeof(int) * 3, SecurePlayerPrefs.GetString(GetPlayerDetailsTags.PLAYER_ID).Length * sizeof(char));
                listener.sendBytes(data, true);
                yield return new WaitForSeconds(1.0f);
            }


        }


        // Update is called once per frame
        void Update()
        {
            if ((base.isTimeOn))
            {


                /*
                float remain = GameConstantData.countTimer - time;
    //			// Debug.Log ("remain time "+remain);
    //			timerText.GetComponent<Text> ().text = "" + remain;
                float percentage = remain * 100 / GameConstantData.countTimer;
                percentage = percentage / 100;
                TimerImage.GetComponent<Image> ().fillAmount = 1 - percentage;
                time -= Time.deltaTime;

                if (time <= 0) {
                    DiceHolder.SetActive (true);
                    base.isTimeOn = false;
                }
                */
            }
        }


        void FixedUpdate()
        {
            if (isRotateDice)
            {

                int month = Random.Range(1, ImageController.getInstance.DiceRotation.Length);

                // PlayerDice.GetComponent<Image>().sprite = Resources.Load<Sprite>("Dices/d-" + month);
                PlayerDice.GetComponent<Image>().sprite = ImageController.getInstance.DiceRotation[month - 1];
                PlayerDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            }
        }

        public string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public override void activeDice()
        {
            DiceHolder.SetActive(true);
            GetComponent<Button>().enabled = true;
            LudoDice.GetComponent<Button>().enabled = true;
        }

        public override void TurnPlayer()
        {
            base.setColor(LudoColor.Red);
            DiceHolder.SetActive(true);
            time = GameConstantData.countTimer;
            //	TimerImage.GetComponent<Image> ().fillAmount = 1.0f;
            base.isTimeOn = true;
            //showArrow.SetActive (true);
            if (SecurePlayerPrefs.GetInt(GameTags.MUSIC_ON) == 1)
            {
                //Handheld.Vibrate ();
            }

        }

        public override void StartClockSound()
        {
            //// Debug.Log ("StartClockSound working");
            GameDelegate.StartTimerSound();
        }



        IEnumerator loadImage(string name)
        {

            //print ("Load image");

            // Debug.Log ("loadImage urls  " + name);
            WWW www = new WWW(name);
            yield return www;
            if (www.error == null)
            {
                base.Image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            }
            else
            {
                // Debug.Log ("Error occur while downloading?");
            }

        }


        public override void diceRollDone(int value, JSONNode TurnValue)
        {
            base.resetDices();
            // Debug.Log("My " + TurnValue.ToString());
            for (int i = 0; i < TurnValue.Count; i++)
            {
                try
                {
                    diceSample[i].SetActive(true);
                    diceSample[i].GetComponent<DiceSample>().index = int.Parse(TurnValue[i]);
                    // diceSample[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Dices/dice_" + int.Parse(TurnValue[i]));
                    diceSample[i].GetComponent<Image>().sprite = ImageController.getInstance.Dice[int.Parse(TurnValue[i]) - 1];
                }
                catch (System.Exception ex)
                {
                    // Debug.Log(ex.Message);
                }
            }

        }

        public override void readyGoti(int position)
        {
            base.showArrow.SetActive(false);
        }



        public override void onNumberSelection(int index)
        {
            for (int i = 0; i < diceSample.Length; i++)
            {
                if (diceSample[i].GetComponent<DiceSample>().index == index)
                {
                    diceSample[i].SetActive(false);
                    break;
                }
            }

        }

        public void EnableDiecesAction()
        {

            LudoDice.GetComponent<Button>().enabled = true;
        }

        public void DisableDiecesAction()
        {

            LudoDice.GetComponent<Button>().enabled = false;
        }

        public void TapDiceAction()
        {
            if (base.isTimeOn)
            {
                //GetComponent<Button> ().enabled = false;
                showArrow.SetActive(false);

                LudoDice.GetComponent<Button>().enabled = false;
                isRotateDice = true;
                GameDelegate.showRollDice("" + base.dicNumber);
                StartCoroutine(stopRotation());
                iTween.ScaleTo(PlayerDice, iTween.Hash("scale", new Vector3(1.5f, 1.5f, 1.5f), "speed", 8.0f, "easetype", "linear"));
            }
        }

        IEnumerator stopRotation()
        {
            yield return new WaitForSeconds(0.5f);
            isRotateDice = false;
            try
            {

                PlayerDice.GetComponent<Image>().sprite = ImageController.getInstance.Dice[base.dicNumber - 1];
                //PlayerDice.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Dices/dice_" + base.dicNumber);
                PlayerDice.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                PlayerDice.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            }
            catch (System.Exception ex)
            {
                // Debug.Log (ex.Message);
            }

            //print (" " + base.dicNumber + " Occured");
            appwarp.sendTapDone(base.dicNumber);
        }


        public void startChatting()
        {
            // Debug.Log ("startChatting working ");
            ClosePanel.SetActive(true);
            ChatPanel.SetActive(true);
            ChatPanel.GetComponent<ChatPanel>().ShowPanel();
        }

        public void SendEmoji()
        {

            // Debug.Log ("SendEmoji working ");
            EmojiPanel.SetActive(true);
            ClosePanel.SetActive(true);
            EmojiPanel.GetComponent<EmojiScript>().ShowEmojiPanel();
        }


        public void HideChatPanel()
        {

            ClosePanel.SetActive(false);
            ChatPanel.SetActive(false);
            EmojiPanel.SetActive(false);

        }

        public void EnableChatBtn()
        {

            string ofline = SecurePlayerPrefs.GetString(GameTags.OFFLINE);
            if (ofline.Equals(GameTags.OFFLINE))
            {
            }
            else
            {
                //ChatBtn.SetActive (true);
                //EmojiBtn.SetActive (true);
            }

        }
    }
}