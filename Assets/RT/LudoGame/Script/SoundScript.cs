using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LudoGameTemplate
{
    public class SoundScript : MonoBehaviour
    {

        public AudioClip GotiSound;
        public AudioClip DiceSound;
        public AudioClip closeSound;
        public AudioClip TurnSound;
        public AudioClip ClockSound;
        public AudioClip ClappingSound;
        public AudioClip CutGotiSound;
        public AudioClip snakeSound;
        public AudioClip ladderSound;

        int soundOn;

        void Start()
        {
            soundOn = SecurePlayerPrefs.GetInt(GameTags.SOUND_ON);
            //// Debug.Log ("soundOn " + soundOn);
        }

        public void OnEnable()
        {

            GameDelegate.onMoveGoti += onMoveGoti;
            GameDelegate.onRollDice += onRollDice;
            GameDelegate.onCloseGoti += onCloseGoti;
            GameDelegate.onplayerTurn += onplayerTurn;
            GameDelegate.onTimerSound += onTimerSound;
            GameDelegate.onClappingSound += onClappingSound;
            GameDelegate.onCutGotiSound += onCutGotiSound;
            GameDelegate.onSnakeSound += onSnakeSound;
            GameDelegate.onLadderSound += onLadderSound;

        }

        public void OnDisable()
        {
            GameDelegate.onMoveGoti -= onMoveGoti;
            GameDelegate.onRollDice -= onRollDice;
            GameDelegate.onCloseGoti -= onCloseGoti;
            GameDelegate.onplayerTurn -= onplayerTurn;
            GameDelegate.onTimerSound -= onTimerSound;
            GameDelegate.onClappingSound -= onClappingSound;
            GameDelegate.onCutGotiSound -= onCutGotiSound;
            GameDelegate.onSnakeSound -= onSnakeSound;
            GameDelegate.onLadderSound -= onLadderSound;
        }

        void onMoveGoti(int number)
        {
            if (soundOn == 1)
            {
                GetComponent<AudioSource>().clip = GotiSound;
                // Debug.Log("sound on"+number);
                GetComponent<AudioSource>().Play();
            }
        }

        void onCloseGoti()
        {
            if (soundOn == 1)
            {
                GetComponent<AudioSource>().clip = closeSound;
                GetComponent<AudioSource>().Play();
            }
        }
        void onLadderSound(int number)
        {
            if (soundOn == 1)
            {
                GetComponent<AudioSource>().clip = ladderSound;
                // Debug.Log("ladder sound on in sound script"+number);
                GetComponent<AudioSource>().Play();
            }
        }

        void onSnakeSound(int number)
        {
            if (soundOn == 1)
            {
                GetComponent<AudioSource>().clip = snakeSound;
                // Debug.Log("snake sound on in sound script"+number);
                GetComponent<AudioSource>().Play();
            }
        }


        void onplayerTurn(string player)
        {
            //GetComponent<AudioSource> ().clip = TurnSound;
            //		GetComponent<AudioSource> ().Play ();
        }

        void onRollDice(string number)
        {
            if (soundOn == 1)
            {
                GetComponent<AudioSource>().clip = DiceSound;
                GetComponent<AudioSource>().Play();
            }
        }


        void onTimerSound()
        {
            if (soundOn == 1)
            {
                //// Debug.Log ("calling onTimerSound ");
                GetComponent<AudioSource>().clip = ClockSound;
                GetComponent<AudioSource>().Play();
            }
        }

        void onClappingSound()
        {
            if (soundOn == 1)
            {
                //// Debug.Log ("calling onTimerSound ");
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = ClappingSound;
                GetComponent<AudioSource>().Play();
            }
        }

        void onCutGotiSound()
        {
            if (soundOn == 1)
            {
                //// Debug.Log ("onCutGotiSound working ");
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = CutGotiSound;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}