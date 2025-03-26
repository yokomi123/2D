using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthyController : MonoBehaviour
{
    public static EnemyHealthyController Instance;


    public int EnemyHealth;
    public int CurrentHealth;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = EnemyHealth;
        if(EnemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        if (EnemyHealth <= 0)
        {
           gameObject.SetActive(false);

        }
    }
}
