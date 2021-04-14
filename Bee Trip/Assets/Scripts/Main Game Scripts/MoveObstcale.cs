
using UnityEngine;

public class MoveObstcale : MonoBehaviour
{

    
    [SerializeField] private LineRenderer _lineRenderer;

    [Space]
    [Space]

    [SerializeField] private Transform _obMove;
    public float speedMove=5f;

    private Transform[] _arrayPoints;

    private int _currentIndex;

    private void Start()
    {
        _arrayPoints = new Transform[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            _arrayPoints[i] = transform.GetChild(i).transform;
        }

        _lineRenderer.positionCount = _arrayPoints.Length+1;
        for (int i = 0; i < _arrayPoints.Length; i++)
            _lineRenderer.SetPosition(i, _arrayPoints[i].position);
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _arrayPoints[0].position);


        _obMove.position = _arrayPoints[_currentIndex].position;
    }

    private void LateUpdate()
    {
        float distance = Vector2.Distance(_arrayPoints[_currentIndex].position, _obMove.position);
        if (distance < 0.1f)
        {
            if (_currentIndex < _arrayPoints.Length - 1)
                _currentIndex++;
            else
                _currentIndex = 0;
        }

        _obMove.position = Vector2.MoveTowards(_obMove.position, _arrayPoints[_currentIndex].position, speedMove * Time.deltaTime);
    }
}
