using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByCollision : MonoBehaviour
{

    public LayerMask Collision1;
    public LayerMask Collision2;
    public LayerMask Collision3;
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
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Interactable Items"))
        {
            gameObject.SetActive(false);
            Debug.Log("发生碰撞，飞行物消失");
        }

    }
}
