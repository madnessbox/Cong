using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallScript : MonoBehaviour
{
    public float spinSpeed = 5;
    public GameObject ball;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            var ballInstance = Instantiate(ball);
            ballInstance.GetComponent<Ball>().setMultiball(true);
            Debug.Log("MULTIBALL!!");
            Destroy(this.gameObject);
        }
    }
}
