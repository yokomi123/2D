using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentController : MonoBehaviour


{
    public static EnchantmentController instance;

    public Elements.Eelement element;//0 non-element;1 fire element
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }    
   
    }
    namespace Elements
    {
        public enum Eelement
        {
            None,
            Gold,
            Wood,
            Water,
            Fire,
            Earth
        }
    
    }