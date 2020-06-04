using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LudoGameTemplate
{
    public class SettingPanelScript : MonoBehaviour
    {

        public GameObject musicBtn;
        public GameObject soundBtn;
        public GameObject notificationBtn;

        int musicOn;
        int soundOn;
        int notificationOn;
        // Use this for initialization
        void Start()
        {


        }

        public void ShowPopup()
        {
            musicOn = SecurePlayerPrefs.GetInt(GameTags.MUSIC_ON);
            soundOn = SecurePlayerPrefs.GetInt(GameTags.SOUND_ON);
            notificationOn = SecurePlayerPrefs.GetInt(GameTags.NOTIFICATION_ON);

            // Debug.Log ("musicOn " + musicOn + " soundOn " + soundOn + " notificationOn " + notificationOn);

            if (musicOn == 1)
            {
                musicBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
            }
            else
            {
                musicBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
            }

            if (soundOn == 1)
            {
                soundBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
            }
            else
            {
                soundBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
            }

            if (notificationOn == 1)
            {
                notificationBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
            }
            else
            {
                soundBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
            }

        }
        // Update is called once per frame
        void Update()
        {

        }

        public void ClosePanel()
        {
            transform.gameObject.SetActive(false);
        }

        public void MusicAction()
        {

            if (musicOn == 1)
            {
                musicBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
                SecurePlayerPrefs.SetInt(GameTags.MUSIC_ON, 0);
                musicOn = 0;
            }
            else
            {
                musicBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
                SecurePlayerPrefs.SetInt(GameTags.MUSIC_ON, 1);
                musicOn = 1;
            }

        }

        public void SoundAction()
        {
            if (soundOn == 1)
            {
                soundBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
                SecurePlayerPrefs.SetInt(GameTags.SOUND_ON, 0);
                soundOn = 0;
            }
            else
            {
                soundBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
                SecurePlayerPrefs.SetInt(GameTags.SOUND_ON, 1);
                soundOn = 1;
            }


        }

        public void NotificationAction()
        {
            if (notificationOn == 1)
            {
                notificationBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/unselected_box");
                SecurePlayerPrefs.SetInt(GameTags.NOTIFICATION_ON, 0);
                notificationOn = 0;
            }
            else
            {
                notificationBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/new/selected_box");
                SecurePlayerPrefs.SetInt(GameTags.NOTIFICATION_ON, 1);
                notificationOn = 1;
            }
        }
    }
}