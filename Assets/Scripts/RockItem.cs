using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockItem : MonoBehaviour
{
    [Header("Rock Properties")]
    public string rockName = "Rock";
    public int stackSize = 5;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Play sound when rock hits something
        Debug.Log("Rock collided with: " + collision.gameObject.name);
    }
}
