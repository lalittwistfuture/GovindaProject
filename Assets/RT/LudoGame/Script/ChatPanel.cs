using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate
{
    public class ChatPanel : MonoBehaviour
    {

        public GameObject SendBtn;
        public GameObject MyBtn;
        public GameObject Entermsg;
        public GameObject ClosePanel;
        public Transform CurrentPos;
        private TouchScreenKeyboard keyboard;
        private bool openKeyBoard = false;
        private bool MsgKeyboard = false;

        // Use this for initialization
        void Start()
        {
            SendBtn.AddComponent<Button>();
            SendBtn.GetComponent<Button>().onClick.AddListener(SendMessage);
            //		MyBtn.SetActive (false);
            //		Entermsg.SetActive (false);
            //		SendBtn.SetActive (false);

        }

        // Update is called once per frame
        void Update()
        {
            if (!openKeyBoard && TouchScreenKeyboard.visible)
            {
                openKeyBoard = true;
            }

            if (keyboard != null)
            {
                if (MsgKeyboard)
                    Entermsg.GetComponent<InputField>().text = keyboard.text;
            }

            if (keyboard != null && keyboard.done && openKeyBoard)
            {
                SendMessage();
                openKeyBoard = false;
            }
        }

        public void DownPanel()
        {
            iTween.MoveTo(transform.gameObject, CurrentPos.position, 0.5f);
            //isMovePanel = true;
        }

        public void DownPanelUp()
        {
            iTween.MoveTo(transform.gameObject, new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), 0.5f);

            //isMovePanel = false;
        }


        public void ShowPanel()
        {
            //Entermsg.GetComponent<InputField> ().text = "";
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            MsgKeyboard = true;
            //DownPanel ();
            //MyBtn.GetComponent<Button> ().interactable = true;
        }

        public void MsgAction()
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.ASCIICapable);
            MsgKeyboard = true;
            //DownPanelUp ();
            MyBtn.GetComponent<Button>().interactable = false;
        }

        public bool ValidateMsg()
        {
            if (Entermsg.GetComponent<InputField>().text.Length != 0)
            {
                // Debug.Log ("msg " + Entermsg.GetComponent<InputField> ().text);
                return true;
            }
            else
            {
                //GameConstantData.showToast (transform,"Enter message");
                return false;

            }
            //return false;
        }

        public void SendMessage()
        {

            if (ValidateMsg())
            {
                //// Debug.Log ("senMessage working "+UserController.getInstance.Name+" "+Entermsg.GetComponent<InputField> ().text);
                appwarp.SendUserChat(UserController.getInstance.Name, Entermsg.GetComponent<InputField>().text);
                StartCoroutine(HideChat());

            }
        }

        IEnumerator HideChat()
        {

            yield return new WaitForSeconds(0.2f);
            ClosePanel.SetActive(false);
            transform.gameObject.SetActive(false);
        }


    }
}