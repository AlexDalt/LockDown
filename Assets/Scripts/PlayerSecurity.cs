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

        uiController.HideRoleSelector();
        uiController.ShowCameraUI(true);
        uiController.OnChooseCamera += ChooseCamera;

        ChooseCamera(0);
	}

    public void Init() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = gameController.uiController;
    }

    void ChooseCamera(int id) {

        Camera.main.gameObject.SetActive(false);
        gameController.cameras[id].gameObject.SetActive(true);

    }

    void OnDestroy() {
        if (isLocalPlayer) {
            uiController.ShowCameraUI(false);
            uiController.OnChooseCamera -= ChooseCamera;
        }
    }
	
}
