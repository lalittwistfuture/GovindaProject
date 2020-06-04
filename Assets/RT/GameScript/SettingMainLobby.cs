using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingMainLobby : MonoBehaviour
{
    public Image Vibration;
    public Image Sound;
    public Sprite OnImage, OffImage;


    void Start()
    {
        updateUI();
    }

    void updateUI()
    {
        Vibration.sprite = LudoGameTemplate.SecurePlayerPrefs.GetInt(LudoGameTemplate.GameTags.MUSIC_ON) == 1 ? OnImage : OffImage;
        Sound.sprite = LudoGameTemplate.SecurePlayerPrefs.GetInt(LudoGameTemplate.GameTags.SOUND_ON) == 1 ? OnImage : OffImage;

    }

    public void ClosePanel()
    {
        transform.gameObject.SetActive(false);
    }

    public void VibrationAction()
    {

        LudoGameTemplate.SecurePlayerPrefs.SetInt(LudoGameTemplate.GameTags.MUSIC_ON, LudoGameTemplate.SecurePlayerPrefs.GetInt(LudoGameTemplate.GameTags.MUSIC_ON) == 1 ? 0 : 1);
        updateUI();
    }

    public void SoundAction()
    {
        LudoGameTemplate.SecurePlayerPrefs.SetInt(LudoGameTemplate.GameTags.SOUND_ON, LudoGameTemplate.SecurePlayerPrefs.GetInt(LudoGameTemplate.GameTags.SOUND_ON) == 1 ? 0 : 1);
        updateUI();


    }

}
