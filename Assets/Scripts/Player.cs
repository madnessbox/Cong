using System.Collections;
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


    private float beforeMoveSpeed = 0;
    private Vector3 beforeScale;

    private Rigidbody2D rb;
    private float angle = 0;
    private int invertVal = 1;
    private delegate void EndPickupDelegate();
    private bool haveSizeIncrease = false;

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

    //CODE NOT BEING USED:
    //public void SetPlayerWidthMultiplier(float multiplier, float affectedTime, bool hasTimer)
    //{
    //    beforeScale = transform.localScale;
    //    transform.localScale = new Vector3(transform.localScale.x * multiplier, transform.localScale.y, transform.localScale.z);

    //    if (hasTimer)
    //    {
    //        EndPickupDelegate del = ResetPlayerSize;
    //        StartCoroutine(PickupTimer(affectedTime, del));
    //    }
    //}

    //public void SetPlayerHeightMultiplier(float multiplier, float affectedTime, bool hasTimer)
    //{
    //    beforeScale = transform.localScale;
    //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * multiplier, transform.localScale.z);

    //    if (hasTimer)
    //    {
    //        EndPickupDelegate del = ResetPlayerSize;
    //        StartCoroutine(PickupTimer(affectedTime, del));
    //    }
    //}

    public void SetPlayerSizeMultiplier(float multiplier, float affectedTime, bool hasTimer)
    {
        if (!haveSizeIncrease)
        {
        haveSizeIncrease = true;
        beforeScale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x * multiplier, transform.localScale.y * multiplier, transform.localScale.z);

        if (hasTimer)
        {
            EndPickupDelegate del = EndPlayerSizePickup;
            StartCoroutine(PickupTimer(affectedTime, del));
        }
        }
    }

    private void EndPlayerSizePickup()
    {
        haveSizeIncrease = false;
        transform.localScale = beforeScale;
    }

    public void ResetPlayerSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }



    public void SetPlayerSpeedMultiplier(float multiplier, float affectedTime, bool hasTimer)
    {
        beforeMoveSpeed = moveSpeed;
        moveSpeed *= multiplier;

        if (hasTimer)
        {
            EndPickupDelegate del = EndPlayerSpeedPickup;
            StartCoroutine(PickupTimer(affectedTime, del));
        }
    }

    public void SetPlayerSpeed(float newSpeed, float affectedTime, bool hasTimer)
    {
        beforeMoveSpeed = moveSpeed;
        moveSpeed = newSpeed;

        if (hasTimer)
        {
            EndPickupDelegate del = EndPlayerSpeedPickup;
            StartCoroutine(PickupTimer(affectedTime, del));
        }
    }

    private void EndPlayerSpeedPickup()
    {
        moveSpeed = beforeMoveSpeed;
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

    IEnumerator PickupTimer(float timeAffected, EndPickupDelegate method)
    {
        yield return new WaitForSeconds(timeAffected);
        method();
    }
}
