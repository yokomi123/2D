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
    
    public float fallMultiplier = 2.5f; //�����ٶȱ���
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
    public float dashSpeed = 15f; // ����ٶ�
    public float dashDuration = 0.2f; // ��̳���ʱ��
    //public float dashCooldown = 1f; // �����ȴʱ��
    private bool isDashing; // �Ƿ����ڳ��
    private float dashStartTime; // ��̿�ʼʱ��
    private bool canDash = true; // �Ƿ���Գ��
    private bool candoublejump;
    public int stepsLastGround; //�뿪�����ʱ�䣨��FixedUpdateִ�д������㣩
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
        



        //if(movex.x > 0 && movey.y > 0)//����
        //{
        //    dashDir = new Vector2(1, 1);
        //}
        //else if(movex.x >0 && movey.y < 0)//����
        //{
        //    dashDir = new Vector2(1,-1);
        //}
        //else if (movex.x <0 && movey.y > 0)//����
        //{
        //    dashDir = new Vector2(-1,1);
        //}
        //else if(movex.x <0 && movey.y < 0)//����
        //{
        //    dashDir = new Vector2(-1,-1);
        //}
        //else if(movex.x == 0 && movey.y>0)//���ϣ�ȡ�����ϣ�
        //{
        //    dashDir = new Vector2(0,1);
        //}
        if(movex.x == 0 && movey.y == 0)//�޷��򰴽�ɫ��Է�����
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
        //else if(movex.x == 0 && movey.y < 0)//����
        //{
        //    dashDir = new Vector2(0,-1);
        //}
        else if(movex.x > 0 && movey.y ==0)//����
        {
            dashDir = new Vector2(1,0);
        }
        else if(movex.x < 0  && movey.y == 0)//����
        {
            dashDir = new Vector2(-1,0);
        }
        //Debug.Log(dashDir);
        //Debug.Log(transform.right);

        //Debug.Log("ˮƽ����ֵ" + horizontalInput);
        //Debug.Log("movespeed��" + moveSpeed);
        //Debug.Log("�����y�ٶ���" + rb.velocity.y);
        //Debug.Log("�����x�ٶ���" + rb.velocity.x);
        //Debug.Log("������ٶ���" + rb.velocity);
        if(canMove)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);//move to left and right
        }
        
        isGround = Physics2D.OverlapCircle(GroundCheckPoint.position, .2f, whatIsGround);//����ɫ�Ƿ��ڵ���
        
       


        if(!isJumping)
        {
            if (Input.GetButtonDown("Jump"))//��Ծ
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
            if (Input.GetButtonDown("Jump"))//������Ծ
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


        if (!isDashing)//���
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
                Debug.Log("����ٶ���" + rb.velocity);
            } 
          
        }
       


        if(GameObject.Find("Attack1")  == null && GameObject.Find("Skill1") == null) 
        { 
            if (movex.x > 0 )//flip sprite
            {
                transform.localScale = Vector3.one * 3;//ͨ���������ı䳯�����ı���ײ��ĳ��򣬶���ֻ��sprite
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
        if (rb.velocity.y < 0) //�����ٶ�
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;//�ý�ɫ�������
        }
        //Debug.Log("����ʱ��" + stepsLastGround);
        if (canJump)
        {
            Debug.Log("������Ծ�ˣ���ʱ������ʱ��Ϊ=" + stepsLastGround);
            Debug.Log("�ڵ�����" + isGround);
            if(isGround || stepsLastGround < tulangSteps)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                isJumping = true;
                
                canJump = false;
                Debug.Log("tulang" + stepsLastGround);
                stepsLastGround += tulangSteps; //������ʱ���������󣬾Ͳ���������
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
        //����һ���ľ���
        var move = (Vector3)rb.velocity * Time.fixedDeltaTime;
        //����ǰ�����λ��
        var furthestPoint = transform.position + move;
        //���ǰ�����λ���м�⵽ָ���㣬˵������������ײ
        var hit = Physics2D.OverlapBox(furthestPoint, myCollider.bounds.size, 0, whatIsGround);

        if (hit)
        {
            //Զ���ϰ��ķ���
            var dir = (transform.position - hit.transform.position).normalized;
            //�ƶ�1��2������λ��
            var tryPos = furthestPoint + dir * move.magnitude + move;
            //�����λ��û����ײ��˵�����Խ���ƫ��
            //����Ҫ�ų��Ӵ����������£����������Ϊһֱ����ײ
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
