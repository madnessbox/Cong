using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallScript : MonoBehaviour
{
    public GameObject ball;
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            if (Random.Range(0, 3) == 1)
            {
                gm.SpawnBall();
                gm.SpawnBall();
                gm.SpawnBall();
                gm.SpawnBall();
            }
            else
            {
                gm.SpawnBall();
                gm.SpawnBall();
            }
            
            Destroy(this.gameObject);
        }
    }
}
