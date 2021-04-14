using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawLine : MonoBehaviour
{


    [SerializeField] private string _tagCantDraw = "Static Object";

    private GameObject _line;

    public LineRenderer lineRenderer;




    private Camera _cam;
    private Vector2 _currentMousePos;

    public bool isDrawing;
    private float _minDisBtwPoints = .05f;
    private float _lengthLineRenderer;


    private bool _isDrawnHitPoint;

    private GameController _gameController;

    //Events
    public delegate void OnEndDrawListener(LineRenderer lineRenderer);
    public event OnEndDrawListener OnEndDraw;

    public Action OnDisableLine;

    public static Action OnPlayerEndDraw;
    public static Action OnPlayerEndFollow;




    private bool _isAndroidPlatform;
     private bool _countTouches = true;


    private void Start()
    {
        _cam = Camera.main;
        _gameController = transform.parent.GetComponent<GameController>();

        _isAndroidPlatform = Application.platform == RuntimePlatform.Android;

    }

    private void OnMouseDown()
    {
        if (_line == null||!_line.activeSelf)
        {
            if(_gameController.amountDraw>0)
            {
                StartDraw();
                isDrawing = true;
               
            }
            
        }

    }

    private void Update()
    {
        if (isDrawing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //end draw
                
                if (_lengthLineRenderer < 0.4f)//if the line pass over the minmum lenght which is 0.4f
                    DisableLine();
                else
                    EndDraw();

                GameUIManager.Instance.drawBar.gameObject.SetActive(false);

            }
            else
            {     
                if(_isAndroidPlatform)
                     _countTouches = Input.touchCount == 1;

                if (Input.GetMouseButton(0)&& _countTouches)
                {
                    _currentMousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                    Vector2? hit = ShootRaycast();
                    if (hit != null)
                    {
                        if (!_isDrawnHitPoint)
                        {
                            _isDrawnHitPoint = true;
                            Draw((Vector2)hit);                     
                        }
                        return;
                    }
                    Draw(_currentMousePos);
                    _isDrawnHitPoint = false;

                }

            }



        }
    }

   /* private Vector2? ShootRaycast()
    {
        Vector2 dir = (_currentMousePos- (Vector2)_lineRenderer.GetPosition(_lineRenderer.positionCount - 1));
        float distance = dir.magnitude;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), dir.normalized, distance);
        int i = 0;
        //if (_isDrawnHitPoint&&hits.Length>1)
        //    i = 1;

        for(int j=0;j<hits.Length;j++)
        {
            Debug.Log(hits[j].collider.gameObject.name);
        }
        for (; i < hits.Length; i++)
        {    
            if (hits[i].collider.CompareTag("Object"))
                return hits[i].point;
        }
        return null;
    }*/

    private Vector2? ShootRaycast()
    {
        int offest = 1;
        if (_isDrawnHitPoint&& lineRenderer.positionCount>1)
            offest = 2;
        Vector2 pos = lineRenderer.GetPosition(lineRenderer.positionCount - offest);
        Vector2 dir = (_currentMousePos - pos);
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, dir.normalized, dir.magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag(_tagCantDraw))
                return hits[i].point;
        }
        return null;
    }

    private void StartDraw()
    {
        _line = PoolManager.Instance.RequestLine();
        lineRenderer = _line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        _lengthLineRenderer = 0;
        GameUIManager.Instance.drawBar.gameObject.SetActive(true);
        GameUIManager.Instance.drawBar.value = 0;
    }

    private void Draw(Vector2 pos)
    {
        if (_lengthLineRenderer < 12)//limit the length of the lineRenderer to 12
        {
            float distnace = Vector2.Distance(pos, lineRenderer.GetPosition(lineRenderer.positionCount - 1));
            if (distnace > _minDisBtwPoints)
            {
                //add point to the draw
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
                _lengthLineRenderer += distnace;
            }
            GameUIManager.Instance.drawBar.value = _lengthLineRenderer;
        }
        //else
          //  EndDraw();
    }

    private void EndDraw()
    { 
        isDrawing = false;
        if (lineRenderer.positionCount <= 1)
        {
            _line.SetActive(false);
        }
        else
        if (OnEndDraw != null)
            OnEndDraw(lineRenderer);

        if (OnPlayerEndDraw != null)
            OnPlayerEndDraw();

       
    }

    public void RemoveFirstPos()
    {
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
        if (lineRenderer.positionCount - 1 > 0)
            lineRenderer.positionCount--;
        else
        {
            // it means the player has finished the line (draw) and he will wait to the next draw        
            DisableLine();
            if (OnPlayerEndFollow != null)
                OnPlayerEndFollow();
        }

    }
    public void DisableLine()
    {
        isDrawing = false;
        //Disable Line
        if (OnDisableLine != null)
            OnDisableLine();
        if(_line!=null)
        {
            _line.SetActive(false);
            _line = null;
        }
        
    }
}
