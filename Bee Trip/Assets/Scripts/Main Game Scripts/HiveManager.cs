using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveManager : MonoBehaviour
{

    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _playerReachGoalPointSound;
    public GoalPoint goalPoint;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
            _playerReachGoalPointSound.mute = true;
    }

    private void OnEnable()
    {
        goalPoint.OnPlayerReachedGoalPoint += PlayerReachedGoalPoint;
        SoundsManager.Instance.OnMuteUnMute += OnMuteSound;
    }
    private void OnDisable()
    {
        goalPoint.OnPlayerReachedGoalPoint -= PlayerReachedGoalPoint;
        SoundsManager.Instance.OnMuteUnMute -= OnMuteSound;
    }

    private void PlayerReachedGoalPoint()
    {
        _anim.SetTrigger("PlayerReachedHive");
    }

    public void GenerateParticles()
    {
        CameraManager.Instance.ShakeScreen();
        Instantiate(goalPoint.particlesCollected, goalPoint.transform.position, Quaternion.identity);
    }




    public void PlayPlayerInGoalPointSound()
    {
        _playerReachGoalPointSound.Play();
    }

    private void OnMuteSound(bool mute)
    {
        _playerReachGoalPointSound.mute = mute;
    }

}
