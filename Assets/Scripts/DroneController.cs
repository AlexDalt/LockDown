
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls movement of drone and rotation of its camera using WASD, QE, RF
/// </summary>
public class DroneController : MonoBehaviour {

    private UIController uiController;
    private GameController gameController;
    private CameraOverlay mainCameraOverlay;

    public float rotationSpeed = -1.5f;
    public float height = 0f;
    public int id;
    public DroneCamController droneCamController;
    public Camera childCam; 


    void Update() {
        // { 
            transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
            if (Input.GetKey(KeyCode.Q)) 
                transform.Rotate(0,rotationSpeed,0,Space.World);
            if (Input.GetKey(KeyCode.E))
                transform.Rotate(0,-(rotationSpeed),0,Space.World);
            if (Input.GetKey(KeyCode.R))
                droneCamController.Up();
            if (Input.GetKey(KeyCode.F))
                droneCamController.Down();    
             //}
    }
}
