﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{

    public float speed = 3.0F; //Speed of player
    public float xRotationSpeed = 3.0F; //X-look sensitivity
    public float yRotationSpeed = 3.0F; //Y-look sensitivity
    public float gravity = 10.0F;


    private bool lockCursor = true;

    private Vector3 moveDirection = Vector3.zero;

    //Y-axis look limits
    public float minimumY = -60.0F;
    public float maximumY = 60.0F;

    private float rotationY;
    private Quaternion originalRotation;

    public override void OnStartLocalPlayer()
    {

        originalRotation = Camera.main.transform.localRotation;
    }
    void Update()
    {
        if (isLocalPlayer)
        {

            if (Input.GetKeyDown(KeyCode.Escape)) {
                lockCursor = !lockCursor;
            }

            if (lockCursor) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }


            //Movement code: From Unity Scripting Manual
            CharacterController controller = GetComponent<CharacterController>();

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //Constant gravity pull
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);


            //X-rotation for player, camera moves as it is parented to the transform of the player
            transform.Rotate(0, Input.GetAxis("Mouse X") * xRotationSpeed * Time.deltaTime, 0);

            //Y-rotation for main camera
            rotationY += Input.GetAxis("Mouse Y") * yRotationSpeed * Time.deltaTime;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            Camera.main.transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
