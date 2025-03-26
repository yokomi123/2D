using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public static PlayerController instance;
    
    public Rigidbody2D rb;
    public SpriteRenderer SR;
    public Transform GroundCheckPoint;
    public Collider2D myCollider;
    
    public float fallMultiplier = 2.5f; //下落速度倍数
    public CameraShake CameraShake;
    public float moveSpeed;
    public float JumpForce;
    //public float sprintForce;
    //private bool canJump = true;
    public bool canJump;
    public bool isJumping;
    public bool onWall;
    public bool canMove = true;
    public int JumpFrame;

    private int jumpFrame;
    public float RealVelocity;
    public float dashSpeed = 15f; // 冲刺速度
    public float dashDuration = 0.2f; // 冲刺持续时间
    //public float dashCooldown = 1f; // 冲刺冷却时间
    private bool isDashing; // 是否正在冲刺
    private float dashStartTime; // 冲刺开始时间
    private bool canDash = true; // 是否可以冲刺
    private bool candoublejump;
    public int stepsLastGround; //离开地面的时间（以FixedUpdate执行次数来算）
    public int tulangSteps = 10;
    private bool LookAtRight;
    private bool isDoubleJump;


    Vector3 flipScale = new Vector3(-1, 1, 1);

    public LayerMask whatIsGround;
    public bool isGround;
    
    private Animator animator;

    Vector2 movey;
    Vector2 movex;
    Vector2 dashDir;

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        CameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        
    }

    // Update is called once per frame
    void Update()
    {

        

        //Debug.Log(rb.gravityScale);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        float h = (int)Input.GetAxis("Horizontal");
        float v = (int)Input.GetAxis("Vertical");

        if(!isDashing)
        {
            movex = new Vector2(horizontalInput,0);
            movey = new Vector2(0,verticalInput);

        }
        



        //if(movex.x > 0 && movey.y > 0)//右上
        //{
        //    dashDir = new Vector2(1, 1);
        //}
        //else if(movex.x >0 && movey.y < 0)//右下
        //{
        //    dashDir = new Vector2(1,-1);
        //}
        //else if (movex.x <0 && movey.y > 0)//左上
        //{
        //    dashDir = new Vector2(-1,1);
        //}
        //else if(movex.x <0 && movey.y < 0)//左下
        //{
        //    dashDir = new Vector2(-1,-1);
        //}
        //else if(movex.x == 0 && movey.y>0)//正上（取消正上）
        //{
        //    dashDir = new Vector2(0,1);
        //}
        if(movex.x == 0 && movey.y == 0)//无方向按角色面对方向冲刺
        {
            if(LookAtRight)
            {
                dashDir = new Vector2(1,0);
            }
            else if(!LookAtRight)
            {
                dashDir = new Vector2(-1, 0);
            }
            
        }
        //else if(movex.x == 0 && movey.y < 0)//正下
        //{
        //    dashDir = new Vector2(0,-1);
        //}
        else if(movex.x > 0 && movey.y ==0)//向右
        {
            dashDir = new Vector2(1,0);
        }
        else if(movex.x < 0  && movey.y == 0)//向左
        {
            dashDir = new Vector2(-1,0);
        }
        //Debug.Log(dashDir);
        //Debug.Log(transform.right);

        //Debug.Log("水平轴数值" + horizontalInput);
        //Debug.Log("movespeed是" + moveSpeed);
        //Debug.Log("刚体的y速度是" + rb.velocity.y);
        //Debug.Log("刚体的x速度是" + rb.velocity.x);
        //Debug.Log("刚体的速度是" + rb.velocity);
        if(canMove)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);//move to left and right
        }
        
        isGround = Physics2D.OverlapCircle(GroundCheckPoint.position, .2f, whatIsGround);//检测角色是否在地面
        
       


        if(!isJumping)
        {
            if (Input.GetButtonDown("Jump"))//跳跃
            {
                canJump = true;
                
            }
            if(!isGround && stepsLastGround > tulangSteps)
            {
                canJump = false;
            }
        }

        if(Input.GetButtonDown("Jump") && !canJump)
        {
            jumpFrame = JumpFrame;
        }

        if(!isJumping && jumpFrame > 0)
        {
            canJump = true;
        }
        
        if (isJumping && !isGround)
        {
            isDoubleJump = true;
            if (Input.GetButtonDown("Jump"))//二段跳跃
            {
                candoublejump = true;
                isJumping = false;
                isDoubleJump = false;
            }
        }

        if(isDoubleJump && isGround)
        {
            
            isJumping = false;
            isDoubleJump = false;
        }


        if (!isDashing)//冲刺
        {
            if(isGround)
            {
                canDash = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                rb.gravityScale = 0;
                isDashing = true;
                dashStartTime = dashDuration; 
                canDash = false;
                CameraShake.enabled = true;
                canMove = false;
            }  
            
        }
        else
        {
            dashStartTime -= Time.deltaTime;
            if(dashStartTime <= 0)
            {
                isDashing = false;
                rb.gravityScale = 1;
                CameraShake.enabled = false;
                canMove = true;
            }
            else
            {
               
                rb.velocity = dashDir * dashSpeed;
                Debug.Log("冲刺速度是" + rb.velocity);
            } 
          
        }
       


        if(GameObject.Find("Attack1")  == null && GameObject.Find("Skill1") == null) 
        { 
            if (movex.x > 0 )//flip sprite
            {
                transform.localScale = Vector3.one * 3;//通过缩放来改变朝向，这会改变碰撞体的朝向，而不只是sprite
                LookAtRight = true;
            
            }
            else if(movex.x < 0 )
            {

                transform.localScale = flipScale * 3;
                LookAtRight = false;

            }
        }
        
        //animator.SetBool("isGrounded", isGround);
        //animator.SetFloat ("isRun", Mathf.Abs (rb.velocity.x));

        //if(rb.velocity.y > 0 )
        //{
        //    animator.SetFloat("isJumpUp",rb.velocity.y);
        //}

        //if(rb.velocity.y < 0 )
        //{
        //    animator.SetFloat("isJumpDown", rb.velocity.y);
           
        //}
        RealVelocity = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        if(jumpFrame >= 0)
        {
            jumpFrame--;
        }


        stepsLastGround = isGround ? 0 : stepsLastGround + 1;
        if (rb.velocity.y < 0) //下落速度
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;//让角色下落更快
        }
        //Debug.Log("土狼时间" + stepsLastGround);
        if (canJump)
        {
            Debug.Log("进入跳跃了，此时的土狼时间为=" + stepsLastGround);
            Debug.Log("在地面吗" + isGround);
            if(isGround || stepsLastGround < tulangSteps)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                isJumping = true;
                
                canJump = false;
                Debug.Log("tulang" + stepsLastGround);
                stepsLastGround += tulangSteps; //在土狼时间内跳过后，就不能再跳了
            }
           
        }


        if (candoublejump)
        {
            
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            candoublejump = false;

        }



        //rb.MovePosition(rb.position + moveDir * Time.deltaTime * moveSpeed);
    }

    private void LateUpdate()
    {
        
    }

    void EdgeDetection()
    {
        //迈出一步的距离
        var move = (Vector3)rb.velocity * Time.fixedDeltaTime;
        //继续前进后的位置
        var furthestPoint = transform.position + move;
        //如果前进后的位置有检测到指定层，说明即将发生碰撞
        var hit = Physics2D.OverlapBox(furthestPoint, myCollider.bounds.size, 0, whatIsGround);

        if (hit)
        {
            //远离障碍的方向
            var dir = (transform.position - hit.transform.position).normalized;
            //移动1、2步骤后的位置
            var tryPos = furthestPoint + dir * move.magnitude + move;
            //如果新位置没有碰撞，说明可以进行偏移
            //这里要排除接触地面的情况下，否则会误认为一直有碰撞
            if (!isGround && !Physics2D.OverlapBox(tryPos, myCollider.bounds.size, 0, whatIsGround))
            {
                transform.position = transform.position + dir * move.magnitude;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") && !isGround)
        {
            onWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") && onWall)
        { 
            onWall = false; 
        }
    }


}
