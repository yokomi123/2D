using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform FarBackGround;
    public float MinY;
    public float MaxY;
   // public Transform MiddleBackGround;

    public bool InStage;


    public Vector2 LastPos;
    // Start is called before the first frame update
    void Start()
    {
        
        LastPos = transform.position;//the start position
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void LateUpdate()
    {
        //Vector2 AmountToMove = new Vector2(transform.position.x - LastPos.x, transform.position.y - LastPos.y);
        if (!InStage)
        {
            if(GameObject.Find("MiYa"))
            transform.position = new Vector3(target.position.x,Mathf.Clamp(target.position.y,MinY,MaxY), transform.position.z);

            //MiddleBackGround.position = MiddleBackGround.position + new Vector3(AmountToMove.x, AmountToMove.y)*0.5f;
        }

        FarBackGround.position = transform.position;
    }
}
