using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Enemy : MonoBehaviour
{

    [SerializeField] private string enemyName;
    public float healthPoints;
    //[SerializeField] private float speed;
    Rigidbody2D rb;
    Animator anim;
    private float walkLimitLeft, walkLimitRight;
    [SerializeField] float walkingSpeed;
    private int walkingDirection = 1;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        walkLimitLeft = transform.position.x - GetComponent<CircleCollider2D>().radius;
        walkLimitRight = transform.position.x + GetComponent<CircleCollider2D>().radius;

    }


    void Update()

    {

        rb.velocity = new Vector2(walkingSpeed * walkingDirection, rb.velocity.y);
    
        if(transform.position.x < walkLimitLeft) walkingDirection = 1;

        if (transform.position.x > walkLimitRight) walkingDirection = -1;
        transform.localScale = new Vector3(2 * walkingDirection, 2, 2);
        
        
    }

    
}
