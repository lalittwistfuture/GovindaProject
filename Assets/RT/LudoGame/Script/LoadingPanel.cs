using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LudoGameTemplate{
public class LoadingPanel : MonoBehaviour
{

    public GameObject loading;
    public GameObject msgText;
    public Image diceLoading;
    List<string> message;
    public static bool serverConnected = true;

    // Use this for initialization
    void Start()
    {

        message = new List<string>();
        message.Add("Waiting for opponent.");
        message.Add("Stay tuned while we connect you to another real player.");
        message.Add("Thanks for waiting, Your opponent will join you soon.");
        msgText.GetComponent<Text>().text = "";
        if (GameConstantData.GameType == GameConstantData.OneToOne)
        {
            InvokeRepeating("changeText", 5.0f, 5.0f);
        }
        serverConnected = true;
    }

    void changeText()
    {
        msgText.GetComponent<Text>().text = message[Random.Range(0, message.Count)];
    }

    private void OnEnable()
    {
        GameDelegate.onSocketConnectionChange += onSocketConnectionChange;

    }

    private void OnDisable()
    {
        GameDelegate.onSocketConnectionChange -= onSocketConnectionChange;
    }

    void onSocketConnectionChange(bool connected)
    {
        serverConnected = connected;
    }


    void FixedUpdate()
    {

        //msgText.GetComponent<Text> ().text = GameController.Message;
        if (serverConnected)
        {
            if (diceLoading)
            {
                try
                {
                    int month = Random.Range(1, ImageController.getInstance.DiceRotation.Length);

                    // PlayerDice.GetComponent<Image>().sprite = Resources.Load<Sprite>("Dices/d-" + month);
                    diceLoading.sprite = ImageController.getInstance.DiceRotation[month - 1];
                    // diceLoading.sprite = Resources.Load<Sprite> ("Dices/d-" + month);
                }
                catch (System.Exception ex)
                {
                    // Debug.Log ("FixedUpdate Exception " + ex.Message);
                }

            }
            if (loading.activeSelf)
            {
                loading.transform.Rotate(Vector3.back, 5, Space.World);

            }
        }
        else
        {
            msgText.GetComponent<Text>().text = "Unable to Connect with server. Please Try again.";


        }
    }




}
}