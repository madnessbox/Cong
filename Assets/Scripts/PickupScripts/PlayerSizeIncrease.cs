﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeIncrease : MonoBehaviour
{

    public float playerSizeMultiplier = 2;
    public float duration = 15;
    public bool hasTimer = true;
    private float spinSpeed = 5;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Ball>() != null)
        {
            collision.GetComponentInParent<Ball>().latestBouncedPlayer?.SetPlayerSizeMultiplier(playerSizeMultiplier, hasTimer, duration);
            Destroy(gameObject);
        }
    }

}
