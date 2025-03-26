using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static CheckPoint instance;

    public SpriteRenderer SR;
    public Sprite checkup, checkoff;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointController.instance.DeactivateCheckpoints();
            SR.sprite = checkup;

            CheckPointController.instance.setSpawnpoint(transform.position);
        }
    }

    public void Resetcheckpoint()
    {
        SR.sprite = checkoff;
    }
}
