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
    private bool isGrounded=true;
    private bool facingRight = true;

    Animator anim;
    float speedX;
    Rigidbody2D rb;
    
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)                                              
        {
            rb.velocity = Vector2.up * verticalImpulse;
            anim.SetTrigger("Jump");
        }
    }
    
    void FixedUpdate()
    {
        //Хотьба
        moveInput = Input.GetAxis("Horizontal");   
        anim.SetFloat("Speed", Mathf.Abs(moveInput));  
        anim.SetBool("Ground", isGrounded);      
                                                          
        rb.velocity = new Vector2(moveInput * horizontalSpeed, rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
            Flip();
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
            Flip();
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

    void OnTriggerStay2D(Collider2D col){               //если в тригере что то есть и у обьекта тег "ground"
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") isGrounded = true;      //то включаем переменную "на земле"
    }
     void OnTriggerExit2D(Collider2D col){              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") isGrounded = false;     //то вЫключаем переменную "на земле"
    }

}
