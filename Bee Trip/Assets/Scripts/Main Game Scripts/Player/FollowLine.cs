
using UnityEngine;

public class FollowLine : MonoBehaviour
{
    [SerializeField] private DrawLine _drawLine;

    [SerializeField] private float _speedMov = 5f;
    [SerializeField] private float _speedRot = 20f;
    [SerializeField] private float _disArrivedPoint=.1f;

    public bool isFollowing = false;
    public LineRenderer currentLineRenderer;





    private int _index;

    private void LateUpdate()
    {
        if(isFollowing)
        {
            //following the line
            LookAt(currentLineRenderer.GetPosition(_index));
            MoveTowardsLine(currentLineRenderer);
        }


    }

    private void MoveTowardsLine(LineRenderer lineRenderer)
    {
        Vector2 lineRendererPosByIndex = lineRenderer.GetPosition(_index);
        transform.position = Vector2.MoveTowards(transform.position, lineRendererPosByIndex, _speedMov * Time.deltaTime); // move towards the point of the draw

        if (Vector2.Distance(transform.position, lineRendererPosByIndex) < _disArrivedPoint)
            _drawLine.RemoveFirstPos();

        //if (Vector2.Distance(transform.position, lineRendererPosByIndex) < _disArrivedPoint) //check if the player has reached to the point of the draw
        //    _index++;
        //if(_index>1||(lineRenderer.positionCount<2&&_index>0)) // I added the index variable in order to get smooth removing positions from the linerenderer
        //{
        //    _index = 0;
        //    _drawLine.RemoveFirstPos();
        //}




    }

    private void LookAt(Vector3 target)
    {
        Vector2 dir = (target - transform.position).normalized;
        float angle = Vector2.Angle(dir, transform.up);

        int clockWise = 1;
        if (Vector3.Cross(dir, transform.up).z > 0)
            clockWise = -1;
        transform.Rotate(transform.forward, clockWise *angle * _speedRot * Time.deltaTime);
    }



}
