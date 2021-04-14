
using UnityEngine;
using System;
public class GoalPoint : OnCollected
{

    public GameObject player;
    public Action OnPlayerReachedGoalPoint;

    public override void OnTriggerd(string tag, GameObject colliderOB)
    {
        if (player == colliderOB)
        {
            particlesCollected = player.GetComponent<PlayerController>().particlesPlayerInHive;
            Debug.Log("Player reached his goalpoint");
            OnPlayerReachedGoalPoint?.Invoke();
            Destroy(player);
        }

    }
}
