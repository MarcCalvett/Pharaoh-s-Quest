using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float defDistanceRay = 100;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private LayerMask whatIsGround;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    public Transform laserFirePoint;


    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }
    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        if(Physics2D.Raycast(m_transform.position, Vector2.left,defDistanceRay, whatIsPlayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, Vector2.left, defDistanceRay, whatIsPlayer);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else if(Physics2D.Raycast(m_transform.position, Vector2.left, defDistanceRay, whatIsGround)){
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, Vector2.left, defDistanceRay, whatIsGround);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, Vector2.left * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
