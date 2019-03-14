using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public float ballSpeedIncrease = 0.2f;
    public GameManager gm;


    public Player latestBouncedPlayer { get; private set; }
    
    private float ballSpeedDelta = 0;
    private float initBallSpeed;
    private bool isMultiball = false;

    public Rigidbody2D rb;
    private TrailRenderer tr;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        initBallSpeed = ballSpeed;

        Launch();
    }

    void Update()
    {
        if ((transform.position - Vector3.zero).magnitude > 6f)
        {
            if (isMultiball)
            {
                Destroy(this.gameObject);
            }

            tr.enabled = false;
            tr.Clear();
            rb.velocity = Vector2.zero;
            transform.position = Vector3.zero;
            Launch();
            tr.enabled = true;
        }
    }

    void Launch()
    {
        gm.ResetScoreAndSpeedMultiplier();
        ballSpeedDelta = 0;
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        rb.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity *= 1 + ballSpeedIncrease;
        ballSpeedDelta += ballSpeedIncrease;

        if (collision.gameObject.GetComponent<Player>() != null)
        {
            latestBouncedPlayer = collision.gameObject.GetComponent<Player>();
            print(latestBouncedPlayer);
        }
        gm.IncreaseScore(1);
        gm.SetSpeedMultiplierText(ballSpeedDelta + 1);
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

    public void setMultiball(bool value)
    {
        isMultiball = value;
    }
}
