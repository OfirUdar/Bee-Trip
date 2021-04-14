using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PanelPause : MonoBehaviour
{
    [SerializeField] private StagesManager _stagesManager;

    [Space]
    [SerializeField] private Animator _anim;
    [SerializeField] private TextMeshProUGUI _stageText;

    [SerializeField] private Image _soundImage;
    [SerializeField] private Image _musicImage;

    private void Start()
    {
        _stageText.text = "Stage " + _stagesManager.currentStage;
        SetSoundsAndMusic();
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


    //Buttons

    public void RestartStage()
    {
        Time.timeScale = 1;
        GameUIManager.Instance.RestartScene();
    }

    public void ClosePanel()
    {
        Time.timeScale = 1;
        Blink.stopBlink = false;
        _anim.SetTrigger("Close");
    }

    public void LoadStages()
    {
        Time.timeScale = 1;
        GameUIManager.Instance.LoadStages();
    }

    public void OpenAnimationFinished()
    {
        Time.timeScale = 0;
        Blink.stopBlink = true ;
    }
    public void CloseAnimationFinished()
    {   
        this.gameObject.SetActive(false);
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
