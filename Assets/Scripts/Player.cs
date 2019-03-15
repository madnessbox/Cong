﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 2;
    private float initMoveSpeed;

    [SerializeField]
    private int playerIndex = 0;

    [Header("Stats")]
    [SerializeField]
    private float velocity = 0;


    private Rigidbody2D rb;
    private float angle = 0;
    private int invertVal = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        initMoveSpeed = moveSpeed;

        switch (playerIndex)
        {
            case 0:
                angle = Mathf.PI;
                break;
            case 1:
                angle = 0;
                break;
            case 2:
                angle = Mathf.PI / 2;
                break;
            case 3:
                angle = 3 * (Mathf.PI / 2);
                break;
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
            (playerIndex == 1 && Input.GetAxisRaw("Player2Horizontal") > 0) ||
            (playerIndex == 2 && Input.GetAxisRaw("Player3Horizontal") > 0) ||
            (playerIndex == 3 && Input.GetAxisRaw("Player4Horizontal") > 0))
        {
            angle += moveSpeed * Time.deltaTime * invertVal;
        }
        else if((playerIndex == 0 && Input.GetAxisRaw("Player1Horizontal") < 0) ||
                (playerIndex == 1 && Input.GetAxisRaw("Player2Horizontal") < 0) ||
                (playerIndex == 2 && Input.GetAxisRaw("Player3Horizontal") < 0) ||
                (playerIndex == 3 && Input.GetAxisRaw("Player4Horizontal") < 0))
        {
            angle -= moveSpeed * Time.deltaTime * invertVal;
        }


        float newX = Mathf.Cos(angle) * 4.75f;
        float newY = Mathf.Sin(angle) * 4.75f;

        Vector3 temp = new Vector3(newX, newY, 0);

        transform.position = temp;

    }

    public void SetPlayerWidthMultiplier(float multiplier)
    {
        transform.localScale = new Vector3(transform.localScale.x * multiplier, transform.localScale.y, transform.localScale.z);
    }

    public void SetPlayerHeightMultiplier(float multiplier)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * multiplier, transform.localScale.z);
    }

    public void SetPlayerSizeMultiplier(float multiplier)
    {
        transform.localScale = new Vector3(transform.localScale.x * multiplier, transform.localScale.y * multiplier, transform.localScale.z);
    }

    public void ResetPlayerSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetPlayerSpeedMultiplier(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    public void SetPlayerSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetPlayerSpeed()
    {
        moveSpeed = initMoveSpeed;
    }

    public void SetPlayerControlsInverted(bool isInverted)
    {
        if (isInverted)
        {
            invertVal = -1;
        }
        else
        {
            invertVal = 1;
        }
    }
}
