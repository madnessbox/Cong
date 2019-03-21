using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSpaceToStart : MonoBehaviour
{
    public GameObject countDownObject;

    void Update()
    {
        if (Input.GetAxisRaw("Submit") > 0)
        {
            countDownObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
