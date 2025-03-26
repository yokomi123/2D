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

    private void OnTriggerEnter2D(Collider2D other)//�����otherʵ���ǹ����ж��䣬��������ұ���
    {
        otherElement = other.gameObject.GetComponent<EnchantmentController>();//otherElementʵ���ǹ����ж��������
        InteractionReaction(selfElement, otherElement);

        if(other.gameObject.CompareTag("Attack"))//�����������У��Ҵ���������������������Բ�ͬ��ȡ����������ĸ�ħ״̬
        {
            Debug.Log("�ɻ�����Ʒ��������");
            if(otherElement != selfElement)
            {
                //�����ж�������Ի�������ұ仯���仯����������ֱ���޸���ң�MiYa��������
                GameObject.Find("MiYa").GetComponent<EnchantmentController>().element = Eelement.None;

            }
        }
    }

    public void InteractionReaction(EnchantmentController selfElement,EnchantmentController otherElement)
    {
        Debug.Log("selfelement" + selfElement.element);
        if (selfElement.element == Eelement.Wood && otherElement.element == Eelement.Fire)//ľ���Ա����վ�
        {
            gameObject.gameObject.SetActive(false);
            
        }
    }


    public interface IElementalReaction
    {
        
        void React(Elements.Eelement element1,Elements.Eelement eelement2);
    }
}


