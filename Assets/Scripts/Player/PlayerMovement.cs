using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalSpeed;
    public float speedX;
    public float verticalImpulse;
    
    public bool isGrounded=true;

    public enum ControlOfCharacter
    {
        Keyboard = 0,
        Sensor=1
    }
    public ControlOfCharacter Control=ControlOfCharacter.Keyboard; 

    private float moveInput;
    private bool facingRight = true;
    private bool JumpBDown=false;

    private int _playerObject,_collideObject, _bonusObject;
    Animator anim;
    Rigidbody2D rb;
    Vector3 pos;

    
    
    void Start()
    {
        //horizontalSpeed=0f;
        rb=GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        _playerObject=LayerMask.NameToLayer("Player");
        _collideObject=LayerMask.NameToLayer("Platform");
    }

    void Update()
    {
        
        if (rb.velocity.y>0)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
            //Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
            //Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject, false);
        }
        
        Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject,  false);

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
    {   
        anim.SetBool("Ground", isGrounded);      

        if (Control==ControlOfCharacter.Keyboard) //Управление через клавиатуру
        {
            //Хотьба
            rb.velocity = new Vector2(moveInput * speedX, rb.velocity.y);
            moveInput = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(moveInput)); 
            anim.SetBool("Run",true);

            //прыжок
            if (Input.GetKey(KeyCode.Space) && isGrounded)                                              
            {
                rb.velocity = Vector2.up * verticalImpulse;
                anim.SetTrigger("Jump");
            }

        }        
        else //Управление через сенсор
        {
            //Хотьба
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
            anim.SetFloat("Speed", 0.002f); 

            //Прыжок
            if (isGrounded&&JumpBDown)                                              
            {
                rb.velocity = Vector2.up * verticalImpulse;
                anim.SetTrigger("Jump");
            }
        }                                           


        //Разворот
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
    }


    void OnTriggerStay2D(Collider2D col)
    {         
        col.gameObject.TryGetComponent<ChildPlatform>(out var a);         //если в тригере что то есть и у обьекта тег платформы
        if (a) 
        isGrounded = true;                                                      //то включаем переменную "на земле"
    }
     void OnTriggerExit2D(Collider2D col)
     {   
        col.gameObject.TryGetComponent<ChildPlatform>(out var a);        //если из триггера что то вышло и у обьекта тег платформы
        if (a) 
        isGrounded = false;                                                     //то вЫключаем переменную "на земле"
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        collision.gameObject.TryGetComponent<ChildPlatform>(out var a);                                  
        if (a) 
        {
            this.transform.SetParent(collision.transform.parent);
        }   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {    
        collision.gameObject.TryGetComponent<ChildPlatform>(out var a);                                   
        if (a) 
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
        anim.SetFloat("Speed", 0f); 
        horizontalSpeed=0f;
    }
    public void UpClickJump()
    {
        JumpBDown=false;
    }
    

    public void OnClickJump()
    {
        JumpBDown=true;
    }
    
    public void OnClickAttack()
    {
       
    }

    public void knockback()
    {
        rb.velocity = Vector2.right*1000f;
    }
}
