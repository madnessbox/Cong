using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{

    private GameManager GMS;

    public void SetCountDown()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManager>();
        GMS.GameStart();
    }
    

    
}
