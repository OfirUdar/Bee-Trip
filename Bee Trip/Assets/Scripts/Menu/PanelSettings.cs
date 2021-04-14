using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{

    [SerializeField]private Image _soundImage;
    [SerializeField] private Image _musicImage;


    [SerializeField] private Button _btnRemoveAds;


    private void Start()
    {   
        SetSoundsAndMusic();
    }
    private void OnEnable()
    {
        _btnRemoveAds.interactable = PlayerPrefs.GetInt("RemoveADS", 0) == 0;
    }


    private void SetSoundsAndMusic()
    {
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
            _soundImage.color = Color.gray;
        else
            _soundImage.color = Color.white;

        if (PlayerPrefs.GetInt("Music", 1) == 0)
            _musicImage.color = Color.gray;
        else
            _musicImage.color = Color.white;
    }


    public void ClosePanelSettings(GameObject openPanel)
    {
        AdsManager.Instance.ShowInterstitial_ByChance(3);
        TransitionManager.Instance.OpenPanel(openPanel);
        TransitionManager.Instance.ClosePanel(this.gameObject);
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.UdarGames.BeeTrip&hl=en-US&ah=Afkc9HLegbnlucRQrDEeMvJ36t4");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8140650053281162522&hl=en_US");
    }

    public void MailUs()
    {
        string email= "udar.apps@gmail.com";
        Application.OpenURL("mailto:" + email);
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/udargames/");
    }


    public void Sound()
    {
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
        {
            PlayerPrefs.SetInt("Sounds", 1);
            _soundImage.color = Color.white;
            SoundsManager.Instance.MuteSounds(false);
        }
        else
        {
            PlayerPrefs.SetInt("Sounds", 0);
            _soundImage.color = Color.gray;
            SoundsManager.Instance.MuteSounds(true);
        }

    }

    public void Music()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
            _musicImage.color = Color.white;
            BackgroundMusic.Instance.music.mute = false;
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            _musicImage.color = Color.gray;
            BackgroundMusic.Instance.music.mute = true;
        }
    }


}
