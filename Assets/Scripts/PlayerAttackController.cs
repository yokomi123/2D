using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackController : MonoBehaviour
{

    public static PlayerAttackController instance;

    
    private int HitCounter = 0;
    private bool inHitStop;


    public bool isSkilling;
    public bool isAttacking;
    public float hitStopTime;
    public float AttackTime;
    public float SkillTime;
    public float CD;
    public bool isHit;
    
    private float cd;
    private float attackTime;
    private float skillTime;

    public Rigidbody2D rb;
    public SpriteRenderer SR;

    private Animator animator;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerAttack();

        if(cd <= 0)
        {
            PlayerSkill(); 
            
        }
        if(cd > 0)
        {
            cd -= Time.deltaTime;
        }
        if (skillTime > 0)
            {
                skillTime -= Time.deltaTime;
            }

        if(GameObject.Find("Attack1") && gameObject.GetComponent<PlayerController>().isGround)
        {

            rb.velocity = new Vector2(0 , rb.velocity.y);

        }


    }

    public void PlayerAttack()
    {

        if (skillTime <= 0)
        {


            if (Input.GetMouseButtonDown(0))
            {
                attackTime = AttackTime;
                isAttacking = true;
                
            }

            if(attackTime > 0)
            {
                attackTime -= Time.deltaTime;
            }




            if (isAttacking)
            {
                animator.SetBool("isAttack", true);
                animator.SetTrigger("attack");
                isAttacking = false;
            }
        }
        
        

        if(isHit)
        {
            //Debug.Log("成功进入ishit了");
            StartCoroutine(HitTimeScale());
            HitCounter++;
            isHit = false;
        }


    }

    public void PlayerSkill()
    {
        if(attackTime <= 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                skillTime = SkillTime;
                isSkilling = true;
               
            }

           

            if(isSkilling)
            {
                cd = CD;
                animator.SetBool("isSkill", true);
                animator.SetTrigger("skill");

                isSkilling= false;
            }
        }

        
        
    }

    IEnumerator HitTimeScale()
    {
        inHitStop = true;
        animator.speed = 0;
        
        yield return new WaitForSeconds(hitStopTime);
        animator.speed = 1;
        inHitStop = false;
    }


}
