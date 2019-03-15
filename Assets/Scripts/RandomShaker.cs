using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShaker : MonoBehaviour
{
    public float shakeSpeed;
    public float shakeAmount;


    Vector3 initPos;
    Vector2 newVec;

    void Start()
    {
        initPos = transform.position;
        newVec = (Random.insideUnitSphere * shakeAmount) + initPos;
    }

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, newVec, shakeSpeed * Time.deltaTime);
        if ((newVec - (Vector2)transform.position).magnitude < 1f)
        {
            newVec = (Random.insideUnitSphere * shakeAmount) + initPos;
        }

        
    }
}
