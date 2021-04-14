
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    [Header("Properties")]   
    [SerializeField] private Vector2 _offset=new Vector2(1f,2f);
    [SerializeField] private float _smoothSpeed=5f;
    public bool moveY = true;
    public bool moveX = true;

    public GameController gameController;





    private Transform _target;
    private Vector2 _targetSize;

    private DrawLine _targetDrawLine;

    private Transform _furthestOB; // the furestOB

    private bool _isFollow;

    private void Start()
    {
        _target = Helper.FindChildByTag(gameController.transform, "Player").transform;
       // _target = GameObject.FindGameObjectWithTag("Player").transform;
        _targetDrawLine = _target.GetComponent<DrawLine>();

        //_furthestOB= Helper.FindChildByType<HiveManager>(gameController.transform).transform;
        _furthestOB = GetFurthestObject(gameController.transform).transform;

        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));      
        var targetSpriteRnd = _target.GetChild(0).GetComponent<SpriteRenderer>();
        _targetSize = targetSpriteRnd.size;
        _offset -= _targetSize/2;
        _offset.x = Mathf.Clamp(_offset.x, 0, screenSize.x);
        _offset.y = Mathf.Clamp(_offset.y, 0, screenSize.y);



        SetupCameraPos();

    }

    private void SetupCameraPos()
    {
        Vector3 newPos = transform.position;
        if (moveX)
            newPos.x = _furthestOB.position.x;
        if (moveY)
            newPos.y = _furthestOB.position.y;

        transform.position = newPos;
    }

    private GameObject GetFurthestObject(Transform parnet) //find and retrun the furthest object from the player (_target)
    {
        float maxDistance = -1;
        GameObject furthestOB=null;
        foreach(Transform child in parnet)
        {
            float currentDistance = Vector2.Distance(_target.position, child.position);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                furthestOB = child.gameObject;
            }    
        }
        return furthestOB;
    }

    private void LateUpdate()
    {
        if(_isFollow)
        {
            CamFollow();
        }
        else
        {
            // when the scene start the camera move slowly from hive to player
            CamFollowStartAnimation();
            if (Input.GetMouseButtonDown(0))
                _isFollow = true;
        }



    }

    private void CamFollowStartAnimation() // when the scene start the camera move slowly from hive to player
    {
        Vector3 newPos = transform.position;
        Vector2 dir = (_target.position - transform.position).normalized;
        if (moveY)
            newPos.y += dir.y * Time.deltaTime * 7f;
        if (moveX)
            newPos.x += dir.x * Time.deltaTime * 7f;

        transform.position = newPos;

        if(moveX)
        {
            _isFollow = false;
            if (Mathf.Abs(transform.position.x - _target.position.x) < 0.1f)
                _isFollow = true;
        }
        if (moveY)
        {
            _isFollow = false;
            if (Mathf.Abs(transform.position.y - _target.position.y) < 0.1f)
                _isFollow = true;
        }
    }


    private void CamFollow()
    {
        if (_targetDrawLine.isDrawing)
        {
            Vector2 drawPos = _targetDrawLine.lineRenderer.GetPosition(_targetDrawLine.lineRenderer.positionCount - 1);
            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 newPos = transform.position;
            if (moveY)
                newPos.y = CheckForY(newPos, drawPos, _smoothSpeed / 3f);
            if (moveX)
                newPos.x = CheckForX(newPos, drawPos, _smoothSpeed / 3f);

            transform.position = newPos;
            return;
        }
        if (_target != null)
        {

            Vector3 newPos = transform.position;
            if (moveY)
                newPos.y = CheckForY(newPos, _target.position, _smoothSpeed);
            if (moveX)
                newPos.x = CheckForX(newPos, _target.position, _smoothSpeed);

            transform.position = newPos;
        }
    }

    private float CheckForY(Vector2 newPos,Vector2 targetPos,float smoothSpeed)
    {
        //for y 
        if (transform.position.y + _offset.y < targetPos.y)
        {
            float newY = transform.position.y + (targetPos.y - transform.position.y - _offset.y);
            newPos.y = Mathf.Lerp(transform.position.y, newY, smoothSpeed * Time.deltaTime);
        }
        if (transform.position.y - _offset.y > targetPos.y)
        {
            float newY = transform.position.y - (transform.position.y - _offset.y - targetPos.y);
            newPos.y = Mathf.Lerp(transform.position.y, newY, smoothSpeed * Time.deltaTime);
        }
        return newPos.y;
    }

    private float CheckForX(Vector2 newPos,Vector2 targetPos, float smoothSpeed)
    {
        //for x
        if (transform.position.x + _offset.x < targetPos.x)
        {
            float newX = transform.position.x + (targetPos.x - transform.position.x - _offset.x);
            newPos.x = Mathf.Lerp(transform.position.x, newX, smoothSpeed * Time.deltaTime);
        }
        if (transform.position.x - _offset.x > targetPos.x)
        {
            float newX = transform.position.x - (transform.position.x - _offset.x - targetPos.x);
            newPos.x = Mathf.Lerp(transform.position.x, newX, smoothSpeed * Time.deltaTime);
        }
        return newPos.x;
    }
}
