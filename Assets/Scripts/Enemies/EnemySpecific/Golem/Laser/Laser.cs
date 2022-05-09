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
    private float timeController;
    private float laserDownTimeController;
    [SerializeField]
    private float damage;

    AttackDetails attackDetails;    
    [SerializeField]
    FloatValue directionY;
    Vector2 direction;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        timeController = 0;
        laserDownTimeController = Time.time;

        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
        
    }    
    private void Update()
    {        
        direction = new Vector2(-1,directionY.RuntimeValue);
        attackDetails.position = transform.position;
        ShootLaser();
        if(Time.time - laserDownTimeController >= 0.1f)
        {
            laserDownTimeController = Time.time;
            direction.y -= 0.01f;
            directionY.RuntimeValue = direction.y;
        }

        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }
    void ShootLaser()
    {
        if(Physics2D.Raycast(m_transform.position, direction,defDistanceRay, whatIsPlayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, direction, defDistanceRay, whatIsPlayer);
            Draw2DRay(laserFirePoint.position, _hit.point);
            if(Time.time - timeController >= 0.05f)
            {
                timeController = Time.time;
                _hit.rigidbody.SendMessage("Damage", attackDetails);
            }

        }
        else if(Physics2D.Raycast(m_transform.position, direction, defDistanceRay, whatIsGround)){
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, direction, defDistanceRay, whatIsGround);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, direction * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);

        
    }
}
