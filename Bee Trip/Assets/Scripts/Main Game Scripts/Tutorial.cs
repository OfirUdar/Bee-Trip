using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameController gameController;
    public Animator anim;
    DrawLine drawLine;
    FollowLine followLine;

    public bool IsHideWhenTouch=true;

    private void Start()
    {
        Transform player = Helper.FindChildByType<PlayerController>(gameController.transform).transform;

        drawLine = player.GetComponent<DrawLine>();
        followLine = player.GetComponent<FollowLine>();
    }

    private void Update()
    {
        if(IsHideWhenTouch)
        {
            if (drawLine.isDrawing || followLine.isFollowing)
            {
                anim.SetTrigger("Idle");
                Destroy(this.gameObject);
            }
        }
        
           
    }

}
