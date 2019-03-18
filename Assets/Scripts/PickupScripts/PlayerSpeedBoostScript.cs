using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedBoostScript : MonoBehaviour
{
    private float spinSpeed = 5;
    private float newSpeed = 3;
    public float duration = 10;
    public bool hasTimer = true;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)                     
    {
        if (collision.GetComponentInParent<Ball>() != null)                 
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer.SetPlayerSpeed(newSpeed, duration, hasTimer);
            Debug.Log("Player BOOST!!");                                   
            Destroy(gameObject);    
        }
    }
}
