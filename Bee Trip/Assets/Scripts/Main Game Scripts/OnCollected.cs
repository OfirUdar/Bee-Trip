
using UnityEngine;

public class OnCollected: MonoBehaviour
{

    public GameObject particlesCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         OnTriggerd(collision.tag,collision.gameObject);                 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerd(collision.collider.tag, collision.gameObject);
    }

    public virtual void OnTriggerd(string tag=null,GameObject colliderOB = null)
    {
        Destroy(this.gameObject);
    }

}
