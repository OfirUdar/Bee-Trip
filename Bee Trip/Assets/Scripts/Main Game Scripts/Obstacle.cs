
using UnityEngine;

public class Obstacle : Blink
{
   [SerializeField] private Animator _anim;
   



    public override void EndPlay()
    {
        if(!isStaticAnim)
             base.EndPlay();
    }
    protected override void Play()
    {
        Attack();
        if (!isStaticAnim)
            base.Play();
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }


}
