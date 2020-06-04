using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
namespace Roullet
{
    public class WheelController : MonoBehaviour
    {
        Vector3 wheelRotVector;

        RectTransform wheel;
        RectTransform ball;
        public AudioClip WheelSound;
        float ballangle = 0f;
        private int[] number = { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5,
        24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26
    };
        private int winNumber;
        private float offset = 360f / 37f;
        private float radius = 123f;
        int counter = 0;
        int previousCounter = -1;
        bool isRotate = false;
        bool isBallRotate = false;
        float time;
        private AudioSource Player;
        private GameObject WinText;
        int[] blackNumberSet = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        int[] redNumberSet = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

        void Start()
        {
            try
            {
                wheel = GameObject.Find("wheel").GetComponent<RectTransform>();
                ball = GameObject.Find("ball").GetComponent<RectTransform>();
                reset();
                WinText = GameObject.Find("WinNumberText");
                Player = transform.GetComponent<AudioSource>();
                WinText.SetActive(false);
            }
            catch (System.Exception ex)
            {
                // Debug.Log(ex.Message);
            }
        }

        void Awake()
        {

        }

        void OnEnable()
        {

            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
        }

        void OnDisable()
        {
            RouletteDelegate.onWarpChatRecived += onWarpChatRecived;
        }

        void onWarpChatRecived(string sender, string message)
        {
            JSONNode s = JSON.Parse(message);


            switch (s[RouletteTag.TAG])
            {

                case RouletteTag.START_WHEEL:
                    {
                        winNumber = int.Parse(s[RouletteTag.VALUE]);
                        startWheel();

                    }
                    break;
                case RouletteTag.MOVE_TO_WHEEL:
                    {
                        reset();
                    }
                    break;
                case RouletteTag.MOVE_TO_TABLE:
                    {



                    }
                    break;


            }
        }


        void FixedUpdate()
        {

            if (isRotate)
            {
                wheelRotVector += new Vector3(0f, 0f, 2.0f);
                wheel.transform.eulerAngles = wheelRotVector;
                if (isBallRotate)
                {
                    ballangle += offset / 2;
                    if (ballangle >= 360f)
                    {
                        ballangle = ballangle - 360f;
                    }

                    int index = (int)(ballangle / offset);
                    if (number[index] == winNumber)
                    {
                        if (previousCounter != counter)
                        {
                            counter++;
                            previousCounter = counter;
                            radius -= 5 / counter;
                        }
                    }
                    else
                    {
                        previousCounter = -1;
                    }
                    if (counter == 8)
                    {
                        ballangle = index * offset + offset / 2;
                        isBallRotate = false;
                        time = Time.time;
                    }
                    float x = radius * Mathf.Sin(Mathf.Deg2Rad * ballangle);
                    float y = radius * Mathf.Cos(Mathf.Deg2Rad * ballangle);
                    ball.transform.localPosition = new Vector3(x, y, 0f);

                }
                else
                {
                    if (Time.time - time > 2)
                    {
                        isRotate = false;
                        Invoke("winFound", 1.0f);
                    }
                }

            }
        }

        void winFound()
        {

            try
            {
                AudioClip clip1 = (AudioClip)Resources.Load("Number/" + winNumber);
              /*  if (PlayerPrefs.GetInt("Sound") == 1)
                {*/
                    // Debug.Log("Play sound Number");
                    Player.PlayOneShot(clip1);
              //  }
                WinText.SetActive(true);
                WinText.GetComponent<Text>().text = "" + winNumber;
                WinText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                iTween.ScaleTo(WinText, new Vector3(5.0f, 5.0f, 5.0f), 1.0f);
                iTween.FadeTo(WinText, iTween.Hash("alpha", 0, "time", 1.0f));
                StartCoroutine(NumberTypeSound());
            }
            catch (System.Exception ex)
            {
                // Debug.Log("Exception occur " + ex.Message);
            }

        }
        IEnumerator NumberTypeSound()
        {
            
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < 18; i++)
            {
                if (winNumber == blackNumberSet[i])
                {
                    AudioClip clip1 = (AudioClip)Resources.Load("Number/BLACK");
                    // Debug.Log("Play BLACK Number");
                    Player.PlayOneShot(clip1);
                }
                if (winNumber == redNumberSet[i])
                {
                    AudioClip clip1 = (AudioClip)Resources.Load("Number/RED");
                    // Debug.Log("Play RED Number");
                    Player.PlayOneShot(clip1);
                }
            }
            yield return new WaitForSeconds(1.0f);
            if (winNumber % 2 == 1)
            {
                AudioClip clip1 = (AudioClip)Resources.Load("Number/ODD");
                // Debug.Log("Play ODD Number");
                Player.PlayOneShot(clip1);
            }
            else
            {
                AudioClip clip1 = (AudioClip)Resources.Load("Number/EVEN");
                // Debug.Log("Play EVEN Number");
                Player.PlayOneShot(clip1);
            }
            clear();


        }

       
        void clear()
        {

            iTween.FadeTo(WinText, iTween.Hash("alpha", 1));
            WinText.SetActive(false);
        }

        public void reset()
        {
            ballangle = 0;
            wheelRotVector = Vector3.zero;
            try
            {
                wheel.transform.eulerAngles = wheelRotVector;
            }
            catch (System.Exception ex)
            {
                // Debug.Log("exception raises " + ex.Message);
            }
            winNumber = -1;
            isBallRotate = false;
            isRotate = false;
            radius = 123f;
            counter = 0;
            previousCounter = -1;

        }

        public void startWheel()
        {
            isBallRotate = true;
            isRotate = true;
            try
            {

                //if (PlayerPrefs.GetInt("Sound") == 1)
                //{
                    // Debug.Log("Play sound Wheel");
                    Player.PlayOneShot(WheelSound);
                //}
            }
            catch (System.Exception ex)
            {
                // Debug.Log("Exception occur " + ex.Message);
            }
            //Player.Play ();
        }
    }
}
