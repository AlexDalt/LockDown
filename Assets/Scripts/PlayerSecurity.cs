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
	
}
