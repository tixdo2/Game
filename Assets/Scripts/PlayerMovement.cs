using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalImpulse;
    public LayerMask whatIsGround;

    private float moveInput;
    private bool isGrounded=true;
    private bool facingRight = true;

    float speedX;
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
        rb=GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        playerObject=LayerMask.NameToLayer("Player");
        collideObject=LayerMask.NameToLayer("Platform");
        //pos = transform.position;
        //PlayerTouch=Input.GetTouch(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)                                              
        {
            rb.velocity = Vector2.up * verticalImpulse;
            anim.SetTrigger("Jump");
        }

        if (rb.velocity.y>0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        }

        //телепорт из-за границ экрана
        pos.y=transform.position.y;
        if (transform.position.x>=3.17f)
            {
                pos.x=-3.1f;
                transform.position=pos;
            }
            else if (transform.position.x<=-3.3f)
            {
                pos.x=3.15f;
                transform.position=pos;
            };  
        
    }
    
    void FixedUpdate()
    {
        /*
        #if UNITY_IOS||UNITY_ANDROID
            if (Input.touchCount>0)
            {
                PlayerTouch=Input.GetTouch(0);
                if (PlayerTouch.phase==TouchPhase.Moved)
                {
                    Vector2 positionChange=PlayerTouch.deltaPosition;
                    positionChange.y=-positionChange.y;
                    moveDirection=positionChange.normalized;
                }
            }
        #endif
        this.transform.position+=(Vector3)moveDirection*-10f*Time.deltaTime;
        



        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }

        if (directionChosen)
        {
            // Something that uses the chosen direction...
            //direction=0;
        }

        rb.velocity = new Vector2(moveInput * horizontalSpeed, rb.velocity.y);

        */


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


    void OnTriggerStay2D(Collider2D col){                                           //если в тригере что то есть и у обьекта тег платформы
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") isGrounded = true;      //то включаем переменную "на земле"
    }
     void OnTriggerExit2D(Collider2D col){                                          //если из триггера что то вышло и у обьекта тег платформы
        if (col.tag == "1"||col.tag == "2"||col.tag == "3") isGrounded = false;     //то вЫключаем переменную "на земле"
    }

}
