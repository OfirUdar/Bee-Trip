
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]

public class ShadowEffect : MonoBehaviour
{
    public Vector2 offset;
    public float size=0.25f;
    [SerializeField] private Color _color= new Color(0, 0, 0, 0.1f);


    private GameObject _shadow;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _shadowSpriteRenderer;
    private void Start()
    {
        if (_shadow == null)
        {
            SetUp();
        }
        
    }

    public void SetUp()
    {
        _shadow = new GameObject("Shadow");
        _shadow.transform.parent = this.transform;


        // shadow.transform.localPosition = offset;
        _shadow.transform.position = (Vector2)transform.position + offset;
        _shadow.transform.localRotation = Quaternion.identity;

        _shadow.transform.localScale = Vector3.one+ Vector3.one*size; 


        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _shadowSpriteRenderer = _shadow.AddComponent<SpriteRenderer>();
        _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
        _shadowSpriteRenderer.color = _color;
        _shadowSpriteRenderer.flipX = _spriteRenderer.flipX;
        _shadowSpriteRenderer.flipY = _spriteRenderer.flipY;
        //_shadowSpriteRenderer.maskInteraction = _spriteRenderer.maskInteraction;


        _shadowSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
        _shadowSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 2;

       // CreateSpriteMask();
    }

    private void CreateSpriteMask()
    {
        _shadowSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        SpriteMask shadowMask=_shadow.AddComponent<SpriteMask>();
        shadowMask.sprite = _shadowSpriteRenderer.sprite;
        shadowMask.isCustomRangeActive = true;
        shadowMask.frontSortingLayerID = _shadowSpriteRenderer.sortingLayerID;
        shadowMask.backSortingLayerID = _shadowSpriteRenderer.sortingLayerID;  
        shadowMask.backSortingOrder = _shadowSpriteRenderer.sortingOrder;
    }

    private void LateUpdate()
    {
        // shadow.transform.localPosition = GetUpdatePosition() * offset;
        _shadow.transform.position = (Vector2)transform.position +offset;
    }

    public Vector2 GetUpdatePosition()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        return new Vector2(sin, cos);
    }

    public void UpdateColor()
    {
        SpriteRenderer shadowSpriteRenderer = _shadow.GetComponent<SpriteRenderer>();
        shadowSpriteRenderer.color = _color;
    }

    public void UpdateLocalScale()
    {
        _shadow.transform.localScale = transform.localScale + Vector3.one * size;
    }

}
