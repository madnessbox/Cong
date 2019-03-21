using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSpaceToStart : MonoBehaviour
{
    public GameObject countDownObject;
    public GameManager gm;

    void Update()
    {
        if (Input.GetAxisRaw("Submit") > 0)
        {
            gm.ClearPickups();
            countDownObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
