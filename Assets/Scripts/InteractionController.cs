using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elements;
using static InteractionController;

public class InteractionController : MonoBehaviour
{
    public static InteractionController instance;
    
    
    public EnchantmentController otherElement;
    public EnchantmentController selfElement;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        selfElement = GetComponent<EnchantmentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)//这里的other实际是攻击判定箱，而不是玩家本身
    {
        otherElement = other.gameObject.GetComponent<EnchantmentController>();//otherElement实际是攻击判定箱的属性
        InteractionReaction(selfElement, otherElement);

        if(other.gameObject.CompareTag("Attack"))//如果被玩家命中，且此物体属性与玩家武器属性不同，取消玩家武器的附魔状态
        {
            Debug.Log("可互动物品被命中了");
            if(otherElement != selfElement)
            {
                //攻击判定箱的属性会随着玩家变化而变化，所以这里直接修改玩家（MiYa）的属性
                GameObject.Find("MiYa").GetComponent<EnchantmentController>().element = Eelement.None;

            }
        }
    }

    public void InteractionReaction(EnchantmentController selfElement,EnchantmentController otherElement)
    {
        Debug.Log("selfelement" + selfElement.element);
        if (selfElement.element == Eelement.Wood && otherElement.element == Eelement.Fire)//木属性被火烧尽
        {
            gameObject.gameObject.SetActive(false);
            
        }
    }


    public interface IElementalReaction
    {
        
        void React(Elements.Eelement element1,Elements.Eelement eelement2);
    }
}


