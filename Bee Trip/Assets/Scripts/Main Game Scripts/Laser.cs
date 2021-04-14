
using UnityEngine;

public class Laser : Blink
{


    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _shotPoint;

    [SerializeField] private float _maxDistance;

    [SerializeField] private GameObject _particlesPlayerDied;


    private RaycastHit2D[] hits;


    private void Start()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _shotPoint.position);
    }


    protected override void Update()
    {
        base.Update();
        if (_lineRenderer.enabled)
            ShootRaycast();
    }


    private void ShootRaycast()
    {
        hits = Physics2D.RaycastAll(_shotPoint.position, _shotPoint.transform.right, _maxDistance);

        _lineRenderer.SetPosition(0, _shotPoint.position);
        _lineRenderer.SetPosition(1, _shotPoint.position + _shotPoint.transform.right * _maxDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.isTrigger)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Instantiate(_particlesPlayerDied, hit.point, Quaternion.identity);
                        Destroy(hit.collider.gameObject);
                        break;
                    }
                    else
                    {
                        _lineRenderer.SetPosition(1, hit.point);
                        return;
                    }
                }
            }
        }
    }

    public override void EndPlay()
    {
        _lineRenderer.enabled = true;
        base.EndPlay();
    }
    protected override void Play()
    {
        _lineRenderer.enabled = false;
        Invoke("EndPlay", timeBtwPlay);
        base.Play();
    }
}
