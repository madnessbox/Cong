using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    public Player playerObject;

    public void ChangePlayerSize()
    {
        Debug.Log("hej");
       playerObject.GetComponent<Player>().SetPlayerSizeMultiplier(10f);

    }

    


}
