using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elements;
using static InteractionController;

public class Interactions : MonoBehaviour
{
    public static Interactions Instance;

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
        
    }

    public class EvaporationReaction
    {
        

        public void React(Elements.Eelement element1, Elements.Eelement element2)
        {
            Debug.Log($"{element1} + {element2} = ’Ù∑¢");
           
        }
    }

}
