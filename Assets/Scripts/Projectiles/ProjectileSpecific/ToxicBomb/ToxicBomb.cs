using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBomb : Projectile
{
    [SerializeField]
    private GameObject toxicGas;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 aux;

        if (hasHitGround)
        {
            aux = transform.position;
            aux.y += 0.8f;
            GameObject toxic = GameObject.Instantiate(toxicGas, aux, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (hasHitPlayer)
        {
            aux = transform.position;
            aux.y += 0.5f;
            GameObject toxic = GameObject.Instantiate(toxicGas, aux, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
