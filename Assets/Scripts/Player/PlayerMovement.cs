using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalSpeed;
    public float speedX;
    public float verticalImpulse;
    public float thrust;
    public GameObject Button_l,Button_R,Button_J,Button_A,joystik;
    private VariableJoystick JoystikControl;
    
    public bool isGrounded=true;

    public enum ControlOfCharacter
    {
        Keyboard = 0,
        Sensor1=1,
        Sensor2=2
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
        JoystikControl=joystik.gameObject.GetComponent<VariableJoystick>();
        _playerObject=LayerMask.NameToLayer("Player");
        _collideObject=LayerMask.NameToLayer("Platform");
        _bonusObject=LayerMask.NameToLayer("Bonus");
    }

    void Update()
    {
        if (rb.velocity.y>0f)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
            Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
            Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject, false);
        }
        //Physics2D.IgnoreLayerCollision(_playerObject, _bonusObject,  false);
        
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
            Button_l.SetActive(false);
            Button_R.SetActive(false);
            Button_J.SetActive(false);
            Button_A.SetActive(false);
            joystik.SetActive(false);
            
            //Хотьба
            rb.velocity = new Vector2(moveInput * speedX, rb.velocity.y);
            moveInput = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(moveInput)); 
            anim.SetBool("Run",true);

            //Прыжок
            if (Input.GetKey(KeyCode.Space) && isGrounded)                                              
            {
                rb.velocity = Vector2.up * verticalImpulse;
                anim.SetTrigger("Jump");
            }

        }        
        else if (Control==ControlOfCharacter.Sensor1) //Управление кнопками
        {
            Button_l.SetActive(true);
            Button_R.SetActive(true);
            Button_J.SetActive(true);
            Button_A.SetActive(true);
            joystik.SetActive(false);
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
        else //Управление сенсором
        {
            Button_l.SetActive(false);
            Button_R.SetActive(false);
            Button_J.SetActive(true);
            Button_A.SetActive(true);
            joystik.SetActive(true);

           //Хотьба
           /*
           if (JoystikControl.Horizontal>0.1f&&JoystikControl.Horizontal<0.5)
           {
               OnClickRight();
               anim.SetFloat("Speed", 0.002f);
               rb.velocity = new Vector2(horizontalSpeed/2, rb.velocity.y); 
           }
           else if (JoystikControl.Horizontal>=0.5f)
           {
                OnClickRight();    
                anim.SetFloat("Speed", 0.002f); 
                rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
           }
           else if (JoystikControl.Horizontal<-0.1f&&JoystikControl.Horizontal>-0.5f)
           {
                OnClickLeft();    
                anim.SetFloat("Speed", 0.002f); 
                rb.velocity = new Vector2(horizontalSpeed/2, rb.velocity.y); 
           }
           else if (JoystikControl.Horizontal<=-0.5f)
           {
                OnClickLeft();    
                anim.SetFloat("Speed", 0.002f); 
                rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
           }
           else
           {
                UpClick();
           }

           */
            rb.velocity = new Vector2(JoystikControl.Horizontal*7, rb.velocity.y); 
            if (JoystikControl.Horizontal>0.01f)
           {
               anim.SetBool("Run",true);
               if (!facingRight) Flip();
               anim.SetFloat("Speed", 0.002f);
           }
           else if (JoystikControl.Horizontal<-0.01f)
           {
                anim.SetBool("Run",true);
                if (facingRight) Flip();    
                anim.SetFloat("Speed", 0.002f); 
           }
           else
           {
                UpClick();
           }




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
        anim.SetBool("Attack",false);
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
       //атака
        anim.SetBool("Attack",true);
    }

    public void knockback()
    {
        //rb.velocity = Vector2.right*1000f;
        rb.AddForce(transform.right*thrust, ForceMode2D.Impulse);
    }
}
