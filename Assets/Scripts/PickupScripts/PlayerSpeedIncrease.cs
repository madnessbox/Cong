using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedIncrease : MonoBehaviour
{
    private float spinSpeed = 5;

    [SerializeField]
    private float newSpeed = 1.5f;
    [SerializeField]
    public float duration = 10;
    public bool hasTimer = true;

    public AudioClip[] clip;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)                     
    {
        if (collision.GetComponentInParent<Ball>() != null)                 
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer?.SetPlayerSpeedMultiplier(newSpeed, hasTimer, duration);

            AudioHandler.instance.SoundQueue(AudioHandler.instance.queue04, clip);
            Destroy(gameObject);    
        }
    }
}
