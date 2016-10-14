using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLobby : Player {

    public GameController gameController;
    public UIController uiController;

    public override void OnStartLocalPlayer() {
        Debug.Log("Local Player Active");
        uiController.ShowRoleSelector();
        uiController.OnChooseRole += CmdChooseRole;
    }

    public void OnDestroy() {
        uiController.HideRoleSelector();
        uiController.OnChooseRole -= CmdChooseRole;
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
