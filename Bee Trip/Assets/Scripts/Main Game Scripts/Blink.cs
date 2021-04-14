
using UnityEngine;


public class Blink :MonoBehaviour
{
    public static bool stopBlink = false;
    [Header("Blink")]
    public bool isBlink=true;
    public float timeBtwPlay;
    public float delayStart;
    public AudioSource sound;
    public bool isStaticAnim;

    protected float timer;

    private bool _isPlaying=false;


    private Camera _cam;
    [Tooltip("max distance of voulme sound of this object")]
    public float maxDistance = 4.5f;


    private void Start()
    {
        _cam = Camera.main;
        stopBlink = false;
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
            sound.mute = true;
        timer = delayStart;
    }
    private void OnEnable()
    {
        SoundsManager.Instance.OnMuteUnMute += OnMuteSound;
    }
    private void OnDisable()
    {
        SoundsManager.Instance.OnMuteUnMute -= OnMuteSound;
    }

    private void OnMuteSound(bool mute)
    {
        sound.mute = mute;
    }

    protected virtual void Play()
    {
        _isPlaying = true;
    }
    public virtual void EndPlay()
    {
        timer = Time.time + timeBtwPlay;
        _isPlaying = false;
        sound.Stop();
    }


    protected virtual void Update()
    {
        SetVolumeByDistance();
        if(isBlink)
        {
            if (!_isPlaying && timer < Time.time)
            {
                Play();
                if(!sound.isPlaying)
                    sound.Play();
            }
        }
        if(stopBlink)
              sound.Stop();
       
    }

    private void SetVolumeByDistance()
    {
        float distance=Vector2.Distance(_cam.transform.position, this.transform.position);
        float volume = 1 - (distance / maxDistance);
        sound.volume = volume; 
    }
}
