using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public static Bounce instance;



    public Collider2D BounceCollider;
    public Rigidbody2D rb;
    public PlayerController playerController;

    public float bounceForce;
    public float bounceStartTime;
    public bool isBounce;
    public float canBounceSpeed;
    public float currentSpeed;

    private float bounceTime;
    private Vector3 bounceDir;
    private bool CanBounce;
    private Vector3 ReflexAngle;


    private void LateUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb = other.gameObject.GetComponent<Rigidbody2D>();
        BounceCollider = other.gameObject.GetComponent<Collider2D>();
        playerController = other.gameObject.GetComponent<PlayerController>();

        currentSpeed = playerController.RealVelocity;
        if(currentSpeed > canBounceSpeed)
        {
            CanBounce = true;
        }
        //Vector3 reflexAngle = Vector3.Reflect(rb.velocity, other.GetContact(0).normal);


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        bounceTime = bounceStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        bounceDir = transform.right;
    }

    private void FixedUpdate()
    {

        if (CanBounce)
        {
            bounceTime -= Time.deltaTime;
            if (bounceTime > 0 )
            {
                isBounce = true;
                rb.velocity = bounceDir * bounceForce;
                Debug.Log("冲刺方向是" + bounceDir);
                if(playerController.onWall)
                {
                    bounceTime = 0;
                }
                Debug.Log("反弹时的速度="+ rb.velocity);
            }
            else if (bounceTime <= 0 || playerController.isGround)
            {
                isBounce = false;
                rb.velocity = Vector2.zero;
               
                bounceTime = bounceStartTime;
                CanBounce = false;
                if(rb !=  null)
                {
                    rb = null;
                }
                if(BounceCollider != null)
                {
                    BounceCollider = null;
                }
            }




        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    rb = other.GetComponent<Rigidbody2D>();
        //    BounceCollider = other.GetComponent<Collider2D>();
        //    canBounceSpeed = rb .velocity.magnitude;
        //    Debug.Log(rb.velocity.magnitude);
        //    Debug.Log(rb);
        //}

        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    if(rb != null)
        //    {
        //        rb = null;
        //    }

        //    if(BounceCollider != null)
        //    {
        //        BounceCollider = null;
        //    }
        //}

        //void BounceOther(Collider2D other)
        //{
        //    rb.velocity = bounceDir * bounceForce;
        //}
    }
}
