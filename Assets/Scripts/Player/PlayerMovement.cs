using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float xMoveSpeed = 3f;
    [SerializeField] private float clampX = 1.7f;
    
    // cached components
    private CharacterController cc;
    
    // private variables
    private bool isStarted = false;

    private Vector3 speed;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    internal void StartGame()
    {
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStarted) return;
        
        speed.x = TouchInput.MoveDirectionX * xMoveSpeed;
        speed.z = forwardSpeed;
        cc.SimpleMove(speed);
        
        Clamp();
    }

    private void Clamp()
    {
        Vector3 tempPos = transform.position;

        tempPos.x = Mathf.Clamp(tempPos.x, -clampX, clampX);

        transform.position = tempPos;
    }
}
