using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other != null)
        {
            if(PlayerHealthyController.instance != null)
            {
                PlayerHealthyController.instance.CurrentHealth -= damage;
            }
            
            Debug.Log("�����㹥���ж��ɹ�");
        }
    }
}
