using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallScript : MonoBehaviour
{
    public float spinSpeed = 5;
    public GameObject ball;
    public GameManager gm;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            if (Random.Range(0, 3) == 1)
            {
                gm.SpawnBall(true);
                gm.SpawnBall(true);
                gm.SpawnBall(true);
                gm.SpawnBall(true);
                Debug.Log("4x MULTIBALL!!");
            }
            else
            {
                gm.SpawnBall(true);
                gm.SpawnBall(true);
                Debug.Log("2x MULTIBALL!!"); 
            }
            
            Destroy(this.gameObject);
        }
    }
}
