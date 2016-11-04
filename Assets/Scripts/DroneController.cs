
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls movement of drone and rotation of its camera using WASD, QE, RF
/// </summary>
public class DroneController : ViewController {

    public UIController uiController;
    private GameController gameController;
    private CameraOverlay mainCameraOverlay;

    public float rotationSpeed = -1.5f;
    public float height = 0f;
    public int id;

    public int minAngle = 0;
    public int maxAngle = 90;

    public void LookUp() {
        if ((camera.transform.localEulerAngles.x >= minAngle + 1) && (camera.transform.localEulerAngles.x <= maxAngle)) {
            camera.transform.Rotate(Vector3.left);
        }
    }

    public void LookDown() {
        if ((camera.transform.localEulerAngles.x >= minAngle) && (camera.transform.localEulerAngles.x <= maxAngle - 1)) {
            camera.transform.Rotate(Vector3.right);
        }
    }

    public void Move() {
        transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
    }

    public void RotateLeft() {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }

    public void RotateRight() {
        transform.Rotate(0, -(rotationSpeed), 0, Space.World);
    }
}
