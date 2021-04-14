
using UnityEngine;
using System;
public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Key _key;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioClip _collectKey;
    [SerializeField] private AudioClip _doorOpen;
    [SerializeField] private AudioClip _doorClose;
    [SerializeField] private Transform _door1;
    [SerializeField] private Transform _door2;


    [Space]
    [SerializeField] private float _distanceToOpen = 1f;


    private Transform _player;
    private DrawLine _drawLinePlayer;

    private bool _hasKey;
    // private bool _isSetTags;
    private bool _isSetTagObject;
    private delegate void PlayerClose();
    private event PlayerClose _OnPlayerClose;

    private delegate void PlayerFarAway();
    private event PlayerFarAway _OnPlayerFarAway;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Sounds", 1) == 0)
            _sound.mute = true;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    private void OnEnable()
    { 
        _key.OnCollectKey += OnCollectKey;
        _OnPlayerClose += OnPlayerClose;
        // _OnPlayerFarAway += OnPlayerFarAway;
        SoundsManager.Instance.OnMuteUnMute += OnMuteSound;

    }
    private void OnDisable()
    {
        _key.OnCollectKey -= OnCollectKey;
        _OnPlayerClose -= OnPlayerClose;
        //_OnPlayerFarAway -= OnPlayerFarAway;
        SoundsManager.Instance.OnMuteUnMute -= OnMuteSound;
    }



    private void LateUpdate()
    {
        if (_player != null)
            DetectPlayerIsCloseToDoor();


    }
   private void OnCollectKey(Transform player)
    {
        this._player = player;
        _drawLinePlayer = _player.GetComponent<DrawLine>();
        _hasKey = true;
        _anim.SetBool("HasKey", _hasKey);
        PlayCollectKey();
    }

    private void DetectPlayerIsCloseToDoor()
    {
        bool isPlayerClose = Vector2.Distance(transform.position, _player.position) <= _distanceToOpen;
        _anim.SetBool("IsPlayerClose", isPlayerClose);

        if (_hasKey)
        {
            if (isPlayerClose)
            {
                if (_OnPlayerClose != null)
                     OnPlayerClose();

            }
            else
                if (_OnPlayerFarAway != null)
                     OnPlayerFarAway();
        }
           
       if(_hasKey && _drawLinePlayer.isDrawing)
        {
            if(!_isSetTagObject)
            {
                _door1.tag = "Object";
                _door2.tag = "Object";
                _isSetTagObject = true;
            }
            
        }
       else
        {
            if(_isSetTagObject)
            {
                _door1.tag = "Static Object";
                _door2.tag = "Static Object";
                _isSetTagObject = false;
            }
          
        }

       /* if (_hasKey&&!_isSetTags)
        {
            if (!isPlayerClose)
            {
                for(int i=0;i<2;i++)
                    transform.GetChild(i).tag = "Object";
                _isSetTags = true;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                    transform.GetChild(i).tag = "Static Object";
            }
        }*/
    }

    //EVENTS

    private void OnPlayerClose()
    {
        PlayDoorOpen();
        _OnPlayerClose -= OnPlayerClose;
        _OnPlayerFarAway += OnPlayerFarAway;
    }
    private void OnPlayerFarAway()
    {
        PlayDoorClose();
        _OnPlayerFarAway -= OnPlayerFarAway;
        _OnPlayerClose += OnPlayerClose;
    }


    private void OnMuteSound(bool mute)
    {
        _sound.mute = mute;
    }

    //SOUNDS

    private void PlayCollectKey()
    {
        _sound.clip = _collectKey;
        _sound.Play();
    }

    private void PlayDoorOpen()
    {
        _sound.clip = _doorOpen;
        _sound.Play();
    }

    private void PlayDoorClose()
    {
        _sound.clip = _doorClose;
        _sound.Play();
    }


}
