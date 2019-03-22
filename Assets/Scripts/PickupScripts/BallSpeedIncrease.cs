using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedIncrease : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;

    public AudioClip[] clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);

            AudioHandler.instance.SoundQueue(AudioHandler.instance.queue04, clip);
            Destroy(this.gameObject);
        }
    }
}
