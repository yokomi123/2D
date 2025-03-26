using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    public static CheckPointController instance;


    public Vector3 Spawnpoint;
    private CheckPoint[] checkpoints;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<CheckPoint>();
        Spawnpoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].Resetcheckpoint();
        }
    }

    public void setSpawnpoint(Vector3 NewspwanPoint)
    {
        Spawnpoint = NewspwanPoint;
    }
}
