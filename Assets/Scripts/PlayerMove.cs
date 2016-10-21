using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

    public float speed = 3.0F; //Speed of player
    public float xRotationSpeed = 3.0F; //X-look sensitivity
    public float yRotationSpeed = 3.0F; //Y-look sensitivity

    //Y-axis look limits
    public float minimumY = -60.0F;
    public float maximumY =  60.0F;

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
            //Movement code
            CharacterController controller = GetComponent<CharacterController>();

            //Get forward and back speeds, controller compatible.
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float forwardSpeed = speed * Input.GetAxisRaw("Vertical");

            Vector3 right = transform.TransformDirection(Vector3.right);
            float strafeSpeed = speed * Input.GetAxisRaw("Horizontal");

            controller.SimpleMove(right * strafeSpeed);
            controller.SimpleMove(forward * forwardSpeed);

            //X-rotation for player, camera moves as it is parented to the transform of the player
            transform.Rotate(0, Input.GetAxis("Mouse X") * xRotationSpeed, 0);

            //Y-rotation for main camera
            rotationY += Input.GetAxis("Mouse Y") * yRotationSpeed;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            Camera.main.transform.localRotation = originalRotation * yQuaternion;
        }

    }

    //Taken from MouseLook.cs in Unity Standard Assets
    //Prevents the player from looking too far up or down
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
