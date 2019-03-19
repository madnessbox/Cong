using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public float ballSpeedIncrease = 0.2f;
    public GameManager gm;
    public bool rotateTowardsVelocity;
    public bool testing = true;
    public Player latestBouncedPlayer { get; private set; }
    
    private float ballSpeedDelta = 0;
    private float initBallSpeed;
    public bool isMultiball = false;

    private float currentTime = 0f;
    private float currentBps = 0;

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
        if (rotateTowardsVelocity)
        {
            Vector3 velDirection = rb.velocity.normalized;
            float angle = Mathf.Atan2(velDirection.y, velDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

            Debug.DrawLine(transform.position, transform.position + velDirection * 5);
        }

        if ((transform.position - Vector3.zero).magnitude > 6f)
        {
            
            if (isMultiball)
            {
                Destroy(this.gameObject);
            }
            if (testing && !isMultiball)
            {
                Destroy(this.gameObject);
                gm.SpawnBall(false);
            }

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
        rb.velocity *= 1 + ballSpeedIncrease;
        ballSpeedDelta += ballSpeedIncrease;

        if (collision.gameObject.GetComponent<Player>() != null)
        {
            latestBouncedPlayer = collision.gameObject.GetComponent<Player>();
        }
        gm.IncreaseScore(1);
        gm.SetSpeedMultiplierText(ballSpeedDelta + 1);
        gm.IncreaseBps(1);
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
