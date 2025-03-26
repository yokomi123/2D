using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMonsterController : MonoBehaviour
{
    public static FireBallMonsterController Instance;

    private float time;

    public float flySpeed;
    public GameObject prefab;
    public GameObject MiYa;
    public EnchantmentController enchantmentController;
    public float AttackInterval;
    public bool isCanCreat
    {
        get
        {
            return time <= 0f;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = AttackInterval;
        MiYa = GameObject.Find("MiYa");
        enchantmentController = MiYa.GetComponent<EnchantmentController>();
    }


    void creatPrefab()
    {

        GameObject clone;
        clone = Instantiate(prefab,transform.position + (-transform.right),transform.rotation);//实例化一个prefab
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = (-transform.right) * flySpeed;
        time = AttackInterval;//使time变量归位
    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;//减去每一帧所消耗的时间
        }
        if (isCanCreat)
        {
            creatPrefab();
        }

    }

    private void OnDisable()
    {
        //被消灭时给予玩家武器火属性附魔
        enchantmentController.element = Elements.Eelement.Fire;
    }
}
