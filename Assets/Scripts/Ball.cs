﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public float ballSpeedIncrease = 0.2f;
    public GameManager gm;
    public bool rotateTowardsVelocity;
    public Player latestBouncedPlayer { get; private set; }

    [SerializeField]
    private Sprite[] ballSprites;

    private float ballSpeedDelta = 0;
    private float initBallSpeed;

    private int bounces = 0;

    public Rigidbody2D rb;
    private TrailRenderer tr;
    private SpriteRenderer sr;

    public AudioClip[] pongSounds;
    public AudioClip[] humanSounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        sr = GetComponentInChildren<SpriteRenderer>();
        initBallSpeed = ballSpeed;

        Launch();
    }

    void Update()
    {
        if (rotateTowardsVelocity)
        {
            Vector3 velDirection = rb.velocity.normalized;
            float angle = Mathf.Atan2(velDirection.y, velDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

            Debug.DrawLine(transform.position, transform.position + velDirection * 5);
        }

        if ((transform.position - Vector3.zero).magnitude > 6f)
        {

            AudioHandler.instance.SoundQueue(AudioHandler.instance.queue02, humanSounds);
            gm.DecreaseBallsActive();
            Destroy(this.gameObject);

        }
    }

    void Launch()
    {
        ballSpeedDelta = 0;
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        rb.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;

        if (bounces % 4 == 0)
        {
            if (bounces / 4 < ballSprites.Length)
            {
                sr.sprite = ballSprites[bounces / 4];
            }
        }
        

        rb.velocity *= 1 + ballSpeedIncrease;
        ballSpeedDelta += ballSpeedIncrease;

        if (collision.gameObject.GetComponent<Player>() != null)
        {
            latestBouncedPlayer = collision.gameObject.GetComponent<Player>();

            AudioHandler.instance.SoundQueue(AudioHandler.instance.queue01, pongSounds);
        }
        gm.IncreaseScore(1);
        gm.SetSpeedMultiplierText(ballSpeedDelta + 1);
        gm.IncreaseBps(1);
        gm.AnimatePortrait(latestBouncedPlayer);
    }

    public void SetBps()
    {
        gm.SetBpsMultiplierText();
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        rb.velocity = newVelocity;
        ballSpeedDelta = 1 - (newVelocity - rb.velocity).magnitude;
        gm.SetSpeedMultiplierText(ballSpeedDelta + 1);
    }

    public void MultiplyVelocity(float multiplier)
    {
        rb.velocity *= multiplier;
        ballSpeedDelta += (multiplier - 1);
        gm.SetSpeedMultiplierText(ballSpeedDelta + 1);
    }

    public void StopVelocity()
    {
        rb.velocity = Vector3.zero;
        ballSpeedDelta = 1;
        gm.SetSpeedMultiplierText(1);
    }
}
