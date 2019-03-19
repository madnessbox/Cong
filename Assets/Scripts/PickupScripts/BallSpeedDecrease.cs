using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedDecrease : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;
    public float spinSpeed = 2;
    public bool isSpeedIncreasing = true;

    private SpriteRenderer sr;

    private void Start()
    {

        sr = GetComponentInParent<SpriteRenderer>();

        //Randomly determine if its increasing or decreasing mehehe
        if (Random.value >= 0.5)
        {
            isSpeedIncreasing = false;
            sr.color = Color.green;
        }else
        {
            isSpeedIncreasing = true;
            sr.color = Color.red;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null && isSpeedIncreasing)
        {
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier + 0.4f);
            Destroy(this.gameObject);
        } else { 
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
    }

    //So we can control this from game manager when spawning it
    public void IsSpeedIncreasing (bool value)
    {
        isSpeedIncreasing = value;
    }
}
