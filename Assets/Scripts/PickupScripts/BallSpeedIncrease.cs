using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedIncrease : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;
    public float spinSpeed = 2;
    public bool isSpeedIncreasing = true;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null && isSpeedIncreasing)
        {
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
        else
        {
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
    }

    //So we can control this from game manager when spawning it
    public void IsSpeedIncreasing(bool value)
    {
        isSpeedIncreasing = value;
    }
}
