using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject[] images;

    public void Start()
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i] = GetComponent<GameObject>();
        }
    }

    public void OnMouseOver()
    {
        images[0].SetActive(false);
        images[1].SetActive(true);
    }

    public void OnMouseExit()
    {
        images[0].SetActive(true);
        images[1].SetActive(false);
    }
}