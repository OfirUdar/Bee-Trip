using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScale : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;


    float height;
    float width;
    Sprite sprite;
    float unitWidth;
    float unitHeight;


    private void Start()
    {
        ScaleFitBackground();
    }


    private void ScaleFitBackground()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        sprite = _spriteRenderer.sprite;
        unitWidth = sprite.textureRect.width / sprite.pixelsPerUnit;
        unitHeight = sprite.textureRect.height / sprite.pixelsPerUnit;

        _spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
    }
}
