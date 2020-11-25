using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speedX;
    public float verticalImpulse;
    public float thrust;
    public GameObject Button_J,joystik;
    public bool isGrounded=true;
    public AudioSource FootStep;
    public enum ControlOfCharacter
    {
        Keyboard = 0,
        Sensor=1,
    }
    public ControlOfCharacter Control=ControlOfCharacter.Keyboard; 

    private float moveInput;
    private bool facingRight = true,JumpBDown=false,inWave=false;
    private int _playerObject,_collideObject, _bonusObject;
    private VariableJoystick JoystikControl;
    private float horizontalSpeed;
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
    }
    
    void FixedUpdate()
    {   
        anim.SetBool("Ground", isGrounded);      

        if (Control==ControlOfCharacter.Keyboard) //Управление через клавиатуру
        {
            Button_J.SetActive(false);
            joystik.SetActive(false);
            //Хотьба
            if (!inWave)
            {
                rb.velocity = new Vector2(moveInput * speedX, rb.velocity.y);
            }
            moveInput = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(moveInput)); 
            anim.SetBool("Run",true);

            //Прыжок
            if (Input.GetKey(KeyCode.Space) && isGrounded&&!inWave)                                              
            {
                rb.velocity = Vector2.up * verticalImpulse;
                anim.SetTrigger("Jump");
            }

        }            
        else if (Control==ControlOfCharacter.Sensor) //Управление сенсором
        {
            Button_J.SetActive(true);
            joystik.SetActive(true);

            if (!inWave)
            {
                rb.velocity = new Vector2(JoystikControl.Horizontal*7, rb.velocity.y); 
            }
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
            if (isGrounded&&JumpBDown&&!inWave)                                              
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

    private IEnumerator PWaves ()
    {
        yield return new WaitForSeconds(0.2f);   
        
        if (inWave)  inWave = false;

        //yield return new WaitForSeconds(0.3f);  
        StopCoroutines(); 
    }

    private void StopCoroutines()
    {
        StopAllCoroutines();
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
        col.gameObject.TryGetComponent<ChildPlatform>(out var a);         //если в тригере что то есть и у обьекта script
        col.gameObject.TryGetComponent<EnemyRecorder>(out var b);    
        if (a||b)
        isGrounded = true;    
        if (b&&col.isTrigger == true)
        {inWave=true;}                                             //то включаем переменную "на земле"
    }
     void OnTriggerExit2D(Collider2D col)
     {   
        col.gameObject.TryGetComponent<ChildPlatform>(out var a);        //если из триггера что то вышло и у обьекта тег платформы
        col.gameObject.TryGetComponent<EnemyRecorder>(out var b);  
        if (a||b) 
        isGrounded = false;    
        if (b&&col.isTrigger == true)
        {StartCoroutine(PWaves());}                                                   //то вЫключаем переменную "на земле"
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
