using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalImpulse;
    //public Transform groundCheck;
    public LayerMask whatIsGround;
    //public float checkRadius;

    private float moveInput;
    //private bool isGrounded=true;
    private bool facingRight = true;

    float speedX;
    Rigidbody2D rb;
    
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();   
    }

    
    void FixedUpdate()
    {
        //Хотьба
        moveInput = Input.GetAxis("Horizontal");                                                               
        rb.velocity = new Vector2(moveInput * horizontalSpeed, rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
            Flip();
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
            Flip();

        //Прыжок
        //isGrounded = Physics2D.OverlapArea(new Vector2 (transform.position.x-0.5f,transform.position.y-0.6f),
        //new Vector2 (transform.position.x+0.5f,transform.position.y-0.51f), whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) /*&& isGroundedFunc()*/)                                              
        {
            rb.velocity = Vector2.up * verticalImpulse;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
    /*
    private bool isGroundedFunc()
    {
        float extraHeightText=.01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(BoxCollider2D.bounds.center,Vector2.down,BoxCollider2D.bounds.extents.y+extraHeightText);
        Color rayColor;
        if (raycastHit.collider!=null)
        {
            rayColor=Color.green;
        }
        else
        {
            rayColor=Color.red;
        }
        Debug.DrawRay(BoxCollider2D.bounds.center,Vector2.down*(BoxCollider2D.bounds.extents.y+extraHeightText));
        return raycastHit.collider !=null;
        
    }
       */
}
