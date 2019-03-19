using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    public float SpeedMultiplier = 0.8f;
    public float spinSpeed = 2;
    public PlayerSize Size;
    public Player playerObject;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            playerObject.GetComponent<Player>().SetPlayerSizeMultiplier(10f);
            
            collision.GetComponentInParent<Ball>().MultiplyVelocity(SpeedMultiplier);
            Destroy(this.gameObject);
        }
    }
}
