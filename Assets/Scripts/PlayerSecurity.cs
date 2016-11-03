using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSecurity : NetworkBehaviour {

    private UIController uiController;
    private GameController gameController;

    public override void OnStartServer() {
        Init();
    }
    public override void OnStartLocalPlayer () {
        Init();

        uiController.ShowRoleUI(false);
        uiController.ShowSecurityUI(true);
        //uiController.OnChooseCamera += ChooseCamera;

        uiController.ChangeCamera(0);
	}

    public void Init() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = gameController.uiController;
    }


    void OnDestroy() {
        if (isLocalPlayer) {
            uiController.ShowSecurityUI(false);
            //uiController.OnChooseCamera -= ChooseCamera;
        }
    }

    void Update() {
        if (uiController.IsDroneSelected) {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                ((DroneController)uiController.SelectedCamera).Move();
            }
            if (Input.GetKey(KeyCode.Q)) {
                ((DroneController)uiController.SelectedCamera).RotateLeft();
            }
            if (Input.GetKey(KeyCode.E)) {
                ((DroneController)uiController.SelectedCamera).RotateRight();
            }
            if (Input.GetKey(KeyCode.R)) {
                ((DroneController)uiController.SelectedCamera).LookUp();
            }
            if (Input.GetKey(KeyCode.F)) {
                ((DroneController)uiController.SelectedCamera).LookDown();
            }
        }
    }
	
}
