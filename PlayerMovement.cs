using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    public float groundDrag;
    public float playerHeight;
    public LayerMask groundMask;
    bool grounded;

    private void Start(){

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update(){

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        GetInput();
        SpeedLimit();

        //if(grounded){
        //    Debug.Log("grounded");
            rb.drag = groundDrag;

        //}else{
        //    Debug.Log("NOTgrounded");
        //    rb.drag = 0;
        //}
    }

    private void FixedUpdate(){

        MovePlayer();
    }

    private void GetInput(){

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){

        // move in the direction the player is facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    private void SpeedLimit(){

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVelocity.magnitude > speed){

            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
