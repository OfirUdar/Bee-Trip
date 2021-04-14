using System;
using UnityEngine;
using UnityEngine.UI;
    
public class Flower : OnCollected
{
    public static Action OnFlowerCollected;
    public Transform canvas;


   

    [Space]
    public bool hasTimer;
    public float timeToEnd = 10f;
    public float delayStart = 0f;

    private Image _imageTimer;
    private float _startTime;

    private void Start()
    {
        if(hasTimer)
        {
            canvas.gameObject.SetActive(true);
            _imageTimer = canvas.GetChild(0).GetComponent<Image>();
            _startTime = Time.time;
            timeToEnd += delayStart;
        }
    }

    public override void OnTriggerd(string tag, GameObject colliderOB)
    {
        if(tag.Equals("Player"))
        {
            if (OnFlowerCollected != null)
                OnFlowerCollected();
            SoundsManager.Instance.PlayCollect();
            GameUIManager.Instance.FlowerCollected();
            Instantiate(particlesCollected, transform.position, Quaternion.identity);
            base.OnTriggerd();
        }

    }

    private void Update()
    {
        if(hasTimer)
        {
            _imageTimer.fillAmount = 1-( (Time.time-_startTime) / timeToEnd);
            if(_imageTimer.fillAmount<=0)
            {
                 Instantiate(particlesCollected, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
