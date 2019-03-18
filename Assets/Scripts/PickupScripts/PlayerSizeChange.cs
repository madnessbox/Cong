using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeChange : MonoBehaviour
{

    public float playerSizeMultiplier = 2;
    public float duration = 15;
    public bool hasTimer = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer?.SetPlayerSizeMultiplier(playerSizeMultiplier, duration, hasTimer);
            Destroy(this.gameObject);
        }
    }

}
