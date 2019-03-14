using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;
    public float spinSpeed = 2;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer?.SetPlayerSizeMultiplier(5);
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
    }
}
