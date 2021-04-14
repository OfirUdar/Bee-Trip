using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    public AudioSource sound;


    [SerializeField] private AudioClip _menuClickLow;
    [SerializeField] private AudioClip _menuClickHigh;
    [SerializeField] private AudioClip _bubble;
    [SerializeField] private AudioClip _playerDie;
    [SerializeField] private AudioClip _stageComplete;
    [SerializeField] private AudioClip _purchase; 
    [SerializeField] private AudioClip _swoosh;

    [SerializeField] private AudioClip[] _collects;
    private int _indexCollectSound = 0;

    public delegate void MuteUnMute(bool mute);
    public event MuteUnMute OnMuteUnMute;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
            sound.mute = true;
    }

    public void MuteSounds(bool mute)
    {
        sound.mute = mute;
        if (OnMuteUnMute != null)
            OnMuteUnMute(mute);
    }


    public void PlayMenuClick()
    {
        sound.clip = _menuClickLow;
        sound.Play();
    }
    public void PlayMenuClickHigh()
    {
        sound.clip = _menuClickHigh;
        sound.Play();
    }
    public void PlayBubble()
    {
        sound.clip = _bubble;
        sound.Play();
    }
    public void PlayCollect()
    {
        if (_indexCollectSound<_collects.Length)
        {
            sound.clip = _collects[_indexCollectSound];
            sound.Play();
            _indexCollectSound++;
        } 
    }
    public void PlayPlayerDie()
    {
        sound.clip = _playerDie;
        sound.Play();
    }
    public void PlayStageComplete()
    {
        sound.clip = _stageComplete;
        sound.Play();
    }
    public void PlayPurchase()
    {
        sound.clip = _purchase;
        sound.Play();
    }
    public void PlaySwoosh()
    {
        sound.clip = _swoosh;
        sound.Play();
    }


}
