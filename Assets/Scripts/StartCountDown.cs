using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    public GameManager GMS;

    public void SetCountDown()
    {
        GMS.StartGame();
        gameObject.SetActive(false);
    }
}
