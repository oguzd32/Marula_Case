using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities;

public class PlayerCollisonHandler : MonoBehaviour
{
    // cached components
    private PlayerController controller;
    private PlayerMovement movement;

    // private variables
    private GameObject lastTriggeredObj = default;
    
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        // check same object multiple trigger
        if(lastTriggeredObj == otherObj) return;
        
        lastTriggeredObj = otherObj;
        
        switch (otherObj.tag)
        {
            case "Finish":
                
                _GameManager.EndGame(true, controller.heightAmount);
                movement.enabled = false;
                break;
            
            case "Obstacle":
                
                break;
            
            case "Collectable":
                
                controller.GrowUp();
                break;
        }
    }
}
