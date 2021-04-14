using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [Space]
    [Tooltip(" the player who can move through")]
    public CreatePlayer PlayerCanMoveThrough;// the player who can move through
   

    private DrawLine _player;

    private bool _isSetTagObject;
    private void Start()
    {
        _sprite.color = PlayerCanMoveThrough.circleColor;
    }

    private void OnEnable()
    {
        PlayerCanMoveThrough.OnDestroyCreatePlayer += GetPlayer;
    }
 
    private void OnDisable()
    {
        PlayerCanMoveThrough.OnDestroyCreatePlayer -= GetPlayer;
    }

    private void GetPlayer(GameObject player)
    {
        _player = player.GetComponent<DrawLine>();
    }


    private void LateUpdate()
    {
       
        if (_player != null)
        {
            if (_player.isDrawing)
            {
                if (!_isSetTagObject)
                {
                    transform.tag = "Object";
                    _isSetTagObject = true;
                }
            }
            else
            {
                if (_isSetTagObject)
                {
                    transform.tag = "Static Object";
                    _isSetTagObject = false;
                }
            }
        }


    }
}
