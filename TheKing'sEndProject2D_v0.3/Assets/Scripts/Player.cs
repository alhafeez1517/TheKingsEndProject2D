using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movement_Speed = 0f;
    Rigidbody2D rigid2d;

    
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal_Movement = Input.GetAxis("Horizontal");

        transform.position += horizontal_Movement * transform.right * (Time.deltaTime * movement_Speed);
        
       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
           
        }
    }

    



}
