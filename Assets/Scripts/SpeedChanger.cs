using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{

    public float SpeedMultiplier = 0.8f;
    public float spinSpeed = 2;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }
}
