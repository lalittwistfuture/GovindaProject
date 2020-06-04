using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class LudoLobbi : MonoBehaviour
    {

        public Text Coin;
        public GameObject LeftBtn;
        public GameObject RightBtn;
        public GameObject[] Indicator;
        public Transform Left1;
        public Transform Left;
        public Transform Rignt;
        public Transform Mid;
        public GameObject OneOnFour;
        public GameObject OneOnOne;
        public GameObject friends;
        public GameObject practice;
        public GameButtonType[] GameButton;
        public GameObject LudoPanel;
        public GameObject Topbarinfo;
        public Text online1On1;
        public Text online1On4;
        public Text friend;
        public Text offline;
        int count = 0;

        void OnEnable()
        {
            Topbarinfo.SetActive(true);
            online1On1.text = SecurePlayerPrefs.GetString(GameTime.ONLINE1ON1);
            online1On4.text = SecurePlayerPrefs.GetString(GameTime.ONLINE1ON4);
            friend.text = SecurePlayerPrefs.GetString(GameTime.FRIENDS);
            offline.text = SecurePlayerPrefs.GetString(GameTime.OFFLINE_TEXT);
        }
        void OnDisable()
        {
            Topbarinfo.SetActive(false);
        }
        public void Close()
        {
            LudoPanel.SetActive(false);
            Topbarinfo.SetActive(false);
        }
        void Start()
        {

            this.GameButton = new GameButtonType[4];

            GameButtonType g = new GameButtonType();
            g.Button = OneOnOne;
            g.index = 0;
            this.GameButton[0] = g;
            GameButtonType g1 = new GameButtonType();
            g1.Button = OneOnFour;
            g1.index = 1;
            this.GameButton[1] = g1;
            GameButtonType g2 = new GameButtonType();
            g2.Button = friends;
            g2.index = 2;
            this.GameButton[2] = g2;
            GameButtonType g3 = new GameButtonType();
            g3.Button = practice;
            g3.index = 3;
            this.GameButton[3] = g3;
            UnSelectedGame();
            SelectedGame(this.getCurrentCenterIndex());
        }
        Vector3 getPosition(int pos)
        {
            switch (pos)
            {
                case 0:
                    return Rignt.position;

                case 1:
                    return Mid.position;

                case 2:
                    return Left.position;

                case 3:
                    return Left1.position;

            }
            return Vector3.zero;
        }


        // Update is called once per frame
        void Update()
        {

        }

        int getCurrentCenterIndex()
        {
            for (int i = 0; i < 4; i++)
            {
                GameButtonType g = GameButton[i];
                if (g.index == 3)
                {
                    return i;
                    // Debug.Log("indicator call = "+i);
                }
            }
            return 0;
            // Debug.Log("indicator call");
        }

        public void LeftClick()
        {
            foreach (GameButtonType btn in this.GameButton)
            {
                btn.index--;
                bool flag = false;
                if (btn.index == -1)
                {
                    btn.index = 3;
                    flag = true;
                }
                Move(btn, flag);
                // Debug.Log("hello");
                UnSelectedGame();
                SelectedGame(btn.index);
            }
            LeftBtn.SetActive(false);
            Invoke("ActiveLeft", 0.25f);
            // UnSelectedGame ();
            // SelectedGame (currentIndex);
        }
        void ActiveLeft()
        {
            LeftBtn.SetActive(true);
        }

        void siftAllToMove()
        {

        }
        public void RightClick()
        {
            foreach (GameButtonType btn in this.GameButton)
            {
                btn.index++;
                bool flag = false;
                if (btn.index == 4)
                {
                    btn.index = 0;
                    flag = true;
                }
                Move(btn, flag);
                UnSelectedGame();
                SelectedGame(btn.index);
            }
            RightBtn.SetActive(false);
            Invoke("ActiveRight", 0.25f);

        }
        void ActiveRight()
        {
            RightBtn.SetActive(true);
        }
        IEnumerator EnableButton()
        {
            yield return new WaitForSeconds(0.5f);
            LeftBtn.GetComponent<Button>().interactable = true;
            LeftBtn.GetComponent<Button>().interactable = true;
        }
        public void SelectedGame(int myCount)
        {
            // Debug.Log("indicator number = "+myCount);
            GameObject g = Indicator[myCount];
            g.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected");
        }

        public void UnSelectedGame()
        {
            foreach (GameObject g in Indicator)
            {
                g.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected");
            }
        }
        void Move(GameButtonType g, bool direct)
        {
            if (direct)
            {
                g.Button.transform.position = getPosition(g.index);
            }
            else
            {
                iTween.MoveTo(g.Button, getPosition(g.index), 1.0f);
            }
        }

    }
    public class GameButtonType
    {
        public int index = 0;
        public GameObject Button;
    }




}