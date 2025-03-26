using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public Transform GroundCheckPoint;


    public bool isGround;
    public int EnemyHealth;


    public GameObject EnemyDeathEffect;
    public LayerMask whatIsGround;
    public DestroyOverTime des;



    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        des = EnemyDeathEffect.GetComponent<DestroyOverTime>();
        GroundCheckPoint = EnemyDeathEffect.transform.Find("groundPoint");
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(GroundCheckPoint.position, .2f, whatIsGround);
        EnemyDeath();
        Debug.Log(EnemyHealth);

    }

    private void EnemyDeath()
    {
        if(EnemyHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(EnemyDeathEffect, transform.position,transform.rotation);
            if(isGround)
            {
                Debug.Log("Ö´ÐÐ");
                des.gameObject.SetActive(true);
            }
        }
    }
}
