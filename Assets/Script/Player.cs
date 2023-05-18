using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed, jumpHeigth;
    private float velX; //variable que almacena el valor del eje X
    private float velY; //Variable que alamcena informacion de el eje Y
    Rigidbody2D rb;
    Animator anim;
    
    
    //Variable que chequea si se esta tocando el suelo
    public Transform groundcheck;
    
    //Variable booleana que nos dice si esta tocando el suelo ("IsGrounded" = true) o no ("IsGrounded" = false)
    public bool isGrounded;
   
    //Radio de deteccion al suelo   
    public float groundCheckRadius;
    
    //Se crean unas series de capas (Ground, Player, Background, etc) en Unity para detectar cual es el piso
    public LayerMask whatIsGround;

    void Start()

    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
        
        
    }

   
    void Update()
    {
        //Se programa en el update porque no tiene que ver con las fisicas (movimiento, salto, etc.), sino que es deteccion

      /*El OverlapCircle se utiliza con 3 parametros. El 1ero es el elemento que va a tocar el piso en este caso es el groundcheck.position
      el 2do cual es el radio (groundCheckRadius) y el 3er que es lo que debe tocar, whatIsGround. Si estan 3 cosas se cumplen es true 
      y si es verdadero (vamos a la funcion "Jump")*/
        isGrounded = Physics2D.OverlapCircle(groundcheck.position ,groundCheckRadius ,whatIsGround);

        if(isGrounded) 
        {
            anim.SetBool("Jump", false);
        }
        else 
        {
            anim.SetBool("Jump", true);
        }
        
        FlipCharacter();
        Attack();


    }
    

    public void FlipCharacter()
    {

        //Tomamos el valor "Horizontal" (que va entre 1 y -1) del la variable, previamente definida en la funcion movement
        if (velX > 0) //derecha
        {
            transform.localScale = new Vector3(2, 2, 2);

        }
        else if (velX < 0) //izquierda
        {
            transform.localScale = new Vector3(-2, 2, 2);

        }

    }
    
    public void Movement() 
    {
        //Dentro de Input se encuentra la funcion .GetAxisRaw. Si le agrego como valor la palabra "Horizontal" me va a dar el valor
        //que se encuentra, en este caso, en el eje X. Va a ser 1 si es hacia la derecha o -1 si es hacia la izquierda.
        velX = Input.GetAxisRaw("Horizontal");
        
        
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed , velY);

        if(rb.velocity.x != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {     
            anim.SetBool("Attack", true);
        
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void Jump() 
    {
        if (Input.GetButton("Jump") && isGrounded) 
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpHeigth);
               
        }
            
    }
    
    
    
    //Se programa en el fixed todo lo que sean fisicas (saltos, correr, etc)
    private void FixedUpdate()
    {
        Movement();
        Jump();
    }
    

}

