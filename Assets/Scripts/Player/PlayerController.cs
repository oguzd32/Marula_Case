using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // cached components
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    internal void StartGame()
    {
        movement.StartGame();
    }

    public void GrowUp()
    {
        
    }
}
