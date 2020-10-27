using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalImpulse;
    public LayerMask whatIsGround;
    public bool isGrounded=true;
    public float speedX;


    private float moveInput;
    private bool facingRight = true;

    
    int playerObject,collideObject;
    Animator anim;
    Rigidbody2D rb;
    Touch PlayerTouch;
    Vector2 moveDirection=Vector2.zero;
    Vector3 pos;
    
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;

    
    
    void Start()
    {
        horizontalSpeed=0f;
        rb=GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        playerObject=LayerMask.NameToLayer("Player");
        collideObject=LayerMask.NameToLayer("Platform");
        //pos = transform.position;
        //PlayerTouch=Input.GetTouch(0);
    }

    void Update()
    {
        //прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)                                              
        {
           OnClickJump();
        }

        if (rb.velocity.y>0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        }

        //атака
        if (Input.GetKey(KeyCode.J))                                              
        {
            anim.SetBool("Attack",Input.GetKeyDown(KeyCode.J));
        }
        
        //телепорт из-за границ экрана
        /*
        pos.y=transform.position.y;
        if (transform.position.x>=4.26f)
            {
                pos.x=-4f;
                transform.position=pos;
            }
            else if (transform.position.x<=-4.26f)
            {
                pos.x=4f;
                transform.position=pos;
            };  
            */
        
    }
    
    void FixedUpdate()
    {   //Хотьба
        moveInput = Input.GetAxis("Horizontal");   
        anim.SetFloat("Speed", Mathf.Abs(moveInput));  
        anim.SetBool("Ground", isGrounded);      
                                                          
        //rb.velocity = new Vector2(moveInput * speedX, rb.velocity.y);
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

        
        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
          Flip();
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
          Flip();
        
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*=-1;
        transform.localScale=Scaler;
        
        //transform.Rotate(0f,180f,0f);
    }


    void OnTriggerStay2D(Collider2D col)
    {                                                           //если в тригере что то есть и у обьекта тег платформы
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") 
        isGrounded = true;                                      //то включаем переменную "на земле"
    }
     void OnTriggerExit2D(Collider2D col)
     {                                                          //если из триггера что то вышло и у обьекта тег платформы
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") 
        isGrounded = false;                                     //то вЫключаем переменную "на земле"
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {                                           
        if (collision.gameObject.tag=="1"|| collision.gameObject.tag == "2"|| collision.gameObject.tag == "3") 
        {
            this.transform.SetParent(collision.transform.parent);
        }   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {                                           
        if (collision.gameObject.tag=="1") 
        {
            this.transform.SetParent(null);
        }   
    }

    public void OnClickLeft()
    {
        anim.SetBool("Run",true);
        if (facingRight) Flip();
        if (horizontalSpeed>=0)
        {
            horizontalSpeed=-speedX;
        }
    }
    public void OnClickRight()
    {
        anim.SetBool("Run",true);
        if (!facingRight) Flip();
        if (horizontalSpeed<=0)
        {
            horizontalSpeed=speedX;
        }
    }

    public void UpClick()
    {
        anim.SetBool("Run",false);
        horizontalSpeed=0f;

    }

    public void OnClickJump()
    {
        if (isGrounded)                                              
        {
         rb.velocity = Vector2.up * verticalImpulse;
            anim.SetTrigger("Jump");
        }
    }
    public void OnClickAttack()
    {
       
    }

}
