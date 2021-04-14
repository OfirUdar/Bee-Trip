
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool isRotating=true;
    public bool rotateClockWise=true;
    public float speed=20f;

    public float angle = 360f;

    private float angleStored;
    private void LateUpdate()
    {
        if(isRotating)
        {
            Rotate();
            if (angle != 360)              
            { 
                if(angleStored>=angle)
                {
                    speed = -speed;
                    angleStored = 0;
                }
            }
        }
    }

    private void Rotate()
    {
        if (rotateClockWise)
            transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        angleStored += Mathf.Abs(speed * Time.deltaTime);
    }

    
}
