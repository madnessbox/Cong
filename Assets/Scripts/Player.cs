using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 2;

    [SerializeField]
    private float maxVelocity = 2;

    [SerializeField]
    private int playerIndex = 0;


    [Header("Stats")]
    [SerializeField]
    private float velocity = 0;


    private Rigidbody2D rb;
    private float angle = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (playerIndex == 0)
        {
            angle = Mathf.PI;
        }
        else
        {
            angle = 0;
        }
    }

    void Update()
    {
        Move();
        KeepRotated();
    }

    void KeepRotated()
    {
        Vector2 difVector = Vector3.zero - transform.position;
        difVector.Normalize();
        float angle = Mathf.Atan2(difVector.y, difVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Move()
    {



        if ((playerIndex == 0 && Input.GetAxisRaw("Player1Horizontal") > 0) ||
            (playerIndex == 1 && Input.GetAxisRaw("Player2Horizontal") > 0))
        {
            angle += moveSpeed * Time.deltaTime;
        }
        else if((playerIndex == 0 && Input.GetAxisRaw("Player1Horizontal") < 0) ||
           (playerIndex == 1 && Input.GetAxisRaw("Player2Horizontal") < 0))
        {
            angle -= moveSpeed * Time.deltaTime;
        }


        float newX = Mathf.Cos(angle) * 4.75f;
        float newY = Mathf.Sin(angle) * 4.75f;

        Vector3 temp = new Vector3(newX, newY, 0);

        transform.position = temp;

    }
}
