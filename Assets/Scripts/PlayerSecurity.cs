using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Object representing a Security player
/// </summary>
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
        if (isLocalPlayer && uiController) {
            uiController.ShowSecurityUI(false);
            //uiController.OnChooseCamera -= ChooseCamera;
        }
    }

    void Update() {
        if (uiController.IsDroneSelected) {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && AttemptControl(uiController.SelectedCamera)) {
                ((DroneController)uiController.SelectedCamera).Move();
            }
            if (Input.GetKey(KeyCode.Q) && AttemptControl(uiController.SelectedCamera)) {
                ((DroneController)uiController.SelectedCamera).RotateLeft();
            }
            if (Input.GetKey(KeyCode.E) && AttemptControl(uiController.SelectedCamera)) {
                ((DroneController)uiController.SelectedCamera).RotateRight();
            }
            if (Input.GetKey(KeyCode.R) && AttemptControl(uiController.SelectedCamera)) {
                ((DroneController)uiController.SelectedCamera).LookUp();
            }
            if (Input.GetKey(KeyCode.F) && AttemptControl(uiController.SelectedCamera)) {
                ((DroneController)uiController.SelectedCamera).LookDown();
            }
        }
    }

    bool AttemptControl(ViewController controller) {
        if (controller.hasAuthority) {
            return true;
        }
        else {
            Debug.Log("Requesting authority over " + controller.name);
            CmdRequestControl(controller.netId);
            return false;
        }
    }

    [Command]
    void CmdRequestControl(NetworkInstanceId objectId) {
        gameController.RequestControl(connectionToClient, objectId);
    }
	
}
