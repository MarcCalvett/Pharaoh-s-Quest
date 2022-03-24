﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
    public void SetDirection(Vector3 _direction)
    {
        direction = _direction;
        
    }
    public void DestroyTornado()
    {
        Destroy(gameObject);
    }
    public void SetParameters()
    {
        if(Rigidbody2D.velocity.x == 0)
        {
            Vector3 auxiliar = transform.localScale;
            transform.localScale = new Vector3(auxiliar.x * direction.x, auxiliar.y, auxiliar.z);
            Rigidbody2D.velocity = direction * speed;
            Debug.Log(direction);
            Debug.Log(Rigidbody2D.velocity);
        }
        
    }
}