using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeChange : MonoBehaviour
{

    public float playerSizeMultiplier = 2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer?.SetPlayerSizeMultiplier(playerSizeMultiplier);
            Destroy(this.gameObject);
        }
    }

}
