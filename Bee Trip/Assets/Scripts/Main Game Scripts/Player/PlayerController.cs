using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private DrawLine _drawLine;
    [SerializeField] private FollowLine _followeLine;
    public GoalPoint goalPoint;

    [Space]
    [Header("Components")]
    [SerializeField] private Animator _playerAnim;

    [SerializeField] private GameObject _particlesPlayerDie;
    public GameObject particlesPlayerInHive;




    private void OnEnable()
    {
        SubscribeEvents(true);
    }

    private void OnDisable()
    {
        SubscribeEvents(false);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Object"))
        {
            Debug.Log("stop move and disable Line");
           // _drawLine.DisableLine();
            //return;
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            PlayerDie();
            return;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerDie();
            return;
        }
    }


    private void PlayerDie()
    {
        Debug.Log("player die, instanitate particles and disable player");
        _drawLine.DisableLine();
        SoundsManager.Instance.PlayPlayerDie();
        CameraManager.Instance.ShakeScreen();
        Instantiate(_particlesPlayerDie, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);

        Invoke("GameOver", 1.3f);
    }

    private void GameOver()
    {
        GameUIManager.Instance.RestartScene();
    }


    #region Events

    private void SubscribeEvents(bool sub)
    {
        if (sub)
        {
            _drawLine.OnEndDraw += StartFollow;
            _drawLine.OnDisableLine += EndFollow;
            goalPoint.OnPlayerReachedGoalPoint += PlayerReachedGoalPoint;
        }
        else
        {
            _drawLine.OnEndDraw -= StartFollow;
            _drawLine.OnDisableLine -= EndFollow;
            goalPoint.OnPlayerReachedGoalPoint -= PlayerReachedGoalPoint;
        }
    }


    //Events
    private void StartFollow(LineRenderer lineRenderer)
    {
        _followeLine.currentLineRenderer = lineRenderer;
       // _playerAnim.SetBool("IsMoving", true);
        _followeLine.isFollowing = true;
    }
    private void EndFollow()
    {
        //_playerAnim.SetBool("IsMoving", false);
        _followeLine.isFollowing = false;
    }

    private void PlayerReachedGoalPoint()
    {
        Debug.Log("Player has reached goal");
        _drawLine.DisableLine();
    }

    #endregion
}
