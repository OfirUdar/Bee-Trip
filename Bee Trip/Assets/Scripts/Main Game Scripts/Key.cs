
using UnityEngine;
using System;
public class Key : OnCollected
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Color color;

    public delegate void CollectKey(Transform player);
    public event CollectKey OnCollectKey;

    private void Start()
    {
        color = _spriteRenderer.color;
    }


    public override void OnTriggerd(string tag, GameObject colliderOB)
    {
        if(tag.Equals("Player"))
        {
            if (OnCollectKey != null)
                OnCollectKey(colliderOB.transform);
            ParticleSystem particles;
            particles = Instantiate(particlesCollected, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            particles.startColor = color;
            base.OnTriggerd();
        }
        
    }
}
