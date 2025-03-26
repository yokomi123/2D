using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Vector3 shakeDir = Vector3.one;



    public float shakeTime = 1.0f;
    private float currentTime = 0.0f;
    private float totalTime = 0.0f;


    public void Trigger()
    {
        totalTime = shakeTime;
        currentTime = shakeTime;
    }

    public void Stop()
    {
        currentTime = 0.0f;
        totalTime = 0.0f;
    }

    public void UpdateShake()
    {
        if (currentTime > 0.0f && totalTime > 0.0f)
        {
            float percent = currentTime / totalTime;
            Vector3 shakePos = Vector3.zero;
            shakePos.x = transform.position.x + UnityEngine.Random.Range(-Mathf.Abs(shakeDir.x) * percent, Mathf.Abs(shakeDir.x) * percent);
            shakePos.y = transform.position.y + UnityEngine.Random.Range(-Mathf.Abs(shakeDir.y) * percent, Mathf.Abs(shakeDir.y) * percent);
            shakePos.z = transform.position.z;
            Camera.main.transform.position = shakePos;
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
            totalTime = 0.0f;
        }


    }
    void LateUpdate()
    {
        UpdateShake();
    }


    void OnEnable()
    {
        Trigger();
    }


}
