
using UnityEngine;

public class Bubble : OnCollected
{
    [SerializeField] private Animator _anim;

    public float delayStart = 1f;

    private bool isStartMove=false;

    private void Start()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).transform.position = transform.position;
        Invoke("StartMove", delayStart);
    }

    private void StartMove()
    {
        isStartMove = true;
    }

    private void LateUpdate()
    {
        if(_anim.GetCurrentAnimatorStateInfo(0).IsName("Active")&&isStartMove)
              transform.Translate(Vector2.up*0.8f * Time.deltaTime);
    }

    public override void OnTriggerd(string tag = null, GameObject colliderOB = null)
    {
        if(tag.Equals("Player") ||tag.Equals("Obstacle"))
        {
            try
            {
                Transform child = transform.GetChild(0);
                child.parent = null;
                child.localScale = Vector3.one;
            }
            catch
            {
                Debug.Log("No childern");
            }
            
            Instantiate(particlesCollected, transform.position, Quaternion.identity);
            SoundsManager.Instance.PlayBubble();
            base.OnTriggerd(tag, colliderOB);
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag.Equals("Obstacle"))
        {
            Transform child = transform.GetChild(0);
            child.parent = null;
            child.localScale = Vector3.one;
            Instantiate(particlesCollected, transform.position, Quaternion.identity);
            SoundsManager.Instance.PlayBubble();
            base.OnTriggerd(tag);
        }

    }
    

}
