using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedDecrease : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
    }
}
