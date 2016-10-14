using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLobby : Player {

    public GameController gameController;
    public UIController uiController;

    public override void OnStartServer() {
        Init();
    }

    public override void OnStartLocalPlayer() {
        Init();
        Debug.Log("Local Player Active");
        uiController.ShowRoleSelector();
        uiController.OnChooseRole += ChooseRole;
    }

    public void Init() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = gameController.uiController;
    }

    public void OnDestroy() {
        uiController.OnChooseRole -= ChooseRole;
    }

    public void ChooseRole(Role role) {
        CmdChooseRole(role);

    }

    [Command]
    public void CmdChooseRole(Role role) {
        if (gameController.ClaimRole(role, this)) {
            Debug.Log("Role is now: " + role);
        }
        else {
            Debug.Log("Role already claimed");
        }
    }

    public void ChooseRoleInfiltrator() {
        CmdChooseRole(Role.Infiltrator);
    }

    public void ChooseRoleSecurity() {
        CmdChooseRole(Role.Security);
    }
    
}
