using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Attack Instance;

    public int damage;

    GameObject MiYa;
    GameObject SkillArea;

    public EnchantmentController selfElement;

    private PlayerAttackController playerAttackController;

    private InteractionController interactionController;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameObject.Find("MiYa"));
        MiYa = GameObject.Find("MiYa");
        SkillArea = GameObject.Find("SkillArea");
        playerAttackController = MiYa.GetComponent<PlayerAttackController>();
        //Debug.Log(selfElement);
    }

    // Update is called once per frame
    void Update()
    {
        selfElement = MiYa.GetComponent<EnchantmentController>();
        
        SkillArea.transform.Find("Skill1").GetComponent<EnchantmentController>().element = selfElement.element;
        if(GameObject.Find("Attack1") != null)
        {
            GameObject.Find("Attack1").GetComponent<EnchantmentController>().element = selfElement.element;
        }
        
        
    }

    public void DamageFireBallEnemy(Collider2D other)
    {
        other.gameObject.GetComponent<EnemyHealthyController>().EnemyHealth -= damage;
            //EnemyHealthyController.Instance.EnemyHealth -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            DamageFireBallEnemy(other);
        }

        //interactionController.InteractionReaction(selfElement, other.gameObject.GetComponent<EnchantmentController>());

        if((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Interactable Items")))
        {
            
            playerAttackController.isHit = true;//如果玩家攻击且命中了敌人或是可互动物体，判定为命中（isHit）
            //Debug.Log(playerAttackController.isHit);
        }
        
        
    }
}
