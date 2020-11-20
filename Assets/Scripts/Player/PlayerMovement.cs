using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalSpeed;
    public float speedX;
    public float verticalImpulse;
    public float thrust;
    public GameObject Button_l,Button_R,Button_J,Button_A,joystik;
    private VariableJoystick JoystikControl;

    public bool onSub = false;
    
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

    private int _playerObject,_collideObject, _mobObject;
    Animator anim;
    Rigidbody2D rb;
    Vector3 pos;

    
    
    void Start()
    {
        EventTrigger trigger= Button_J.GetComponent<EventTrigger>();
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData) data); });
        
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerExit;
        entry1.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData) data); });
        trigger.triggers.Add(entry);
        trigger.triggers.Add(entry1);
        //horizontalSpeed=0f;
        rb=GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        JoystikControl=joystik.gameObject.GetComponent<VariableJoystick>();
        _playerObject=LayerMask.NameToLayer("Player");
        _collideObject=LayerMask.NameToLayer("Platform");
        _mobObject=LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {

        Physics2D.IgnoreLayerCollision(_playerObject, _mobObject, true);
        if (rb.velocity.y>0f)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
        }
        else 
        {
            onSub = false;
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
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
    {   
        anim.SetBool("Ground", isGrounded);      

        if (Control==ControlOfCharacter.Keyboard) //Управление через клавиатуру
        {
            //Button_l.SetActive(false);
            //Button_R.SetActive(false);
            Button_J.SetActive(false);
            //Button_A.SetActive(false);
            joystik.SetActive(false);
            
            //Хотьба
            
            rb.velocity = new Vector2(moveInput * speedX, rb.velocity.y);
            moveInput = Input.GetAxis("Horizontal");
            
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
            anim.SetBool("Run",true);
            if(Mathf.Abs(moveInput)>0.001)
            {
                if(isGrounded) Run();
            }
            

            //Прыжок
            if (Input.GetKey(KeyCode.Space) && isGrounded)                                              
            {
                rb.velocity = Vector2.up * verticalImpulse;
                anim.SetTrigger("Jump");
            }

        }        
              
        else //Управление сенсором
        {
            //Button_l.SetActive(false);
            //Button_R.SetActive(false);
            Button_J.SetActive(true);
            //Button_A.SetActive(true);
            joystik.SetActive(true);

           //Хотьба
           rb.velocity = new Vector2(JoystikControl.Horizontal*7, rb.velocity.y); 
           if (JoystikControl.Horizontal>0.01f)
           {
               if(isGrounded) Run();
               anim.SetBool("Run",true);
               if (!facingRight) Flip();
               anim.SetFloat("Speed", 0.002f);
           }
           else if (JoystikControl.Horizontal<-0.01f)
           {
               if(isGrounded) Run();
               anim.SetBool("Run",true);
               if (facingRight) Flip();    
               anim.SetFloat("Speed", 0.002f); 
           }
           else
           {
               UpClick();
           }
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
    public void OnPointerExitDelegate(PointerEventData data)
    {
        JumpBDown=false;
    }
    

    public void OnPointerDownDelegate(PointerEventData data)
    {
        JumpBDown=true;
    }
    
    public void OnClickAttack()
    {
       
    }

    public void knockback()
    {
        //rb.velocity = Vector2.right*1000f;
        rb.AddForce(transform.right*thrust, ForceMode2D.Impulse);
    }

    private void Run()
    {
        ParticleSystem Particle = transform.GetChild(3).GetComponent<ParticleSystem>();
        Particle.Play();
    }
}
