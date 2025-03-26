using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthyController : MonoBehaviour
{
    public static PlayerHealthyController instance;


    public float MaxHealth;
    public float CurrentHealth;

    public float InvincibleLength;
    public float InvincibleCounter;

    //public GameObject DeathEffect;

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
 
            if(CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            //Debug.Log(gameObject.activeSelf);
            GameObject.Find("MiYa").GetComponent<DestroyOverTime>().enabled = true;
            Debug.Log("成功将destroyovertime设置为true");
            }

    }

    private void OnDestroy()
    {

        SceneManager.LoadScene("Scence1");
    }


}
