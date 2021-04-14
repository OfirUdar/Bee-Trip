
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //singleton
   public static CameraManager Instance { get; private set; }

   private void Awake()
    {
        Instance = this;
    }


    public CameraFollow cameraFollowSC;
    [SerializeField] private Animator _anim;



    public void ShakeScreen()
    {
        _anim.SetTrigger("Shake");
    }

}
