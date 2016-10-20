using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

    private UIController uiController;
    private GameController gameController;

    public float height = 0f;
    public int id;


    void Update() {
        //if (gameController.cameras[id].gameObject.activeSelf == true) { 
        transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
        if (Input.GetKey(KeyCode.Q)) 
            transform.Rotate(Vector3.down);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up);        
    // }
    }
}
