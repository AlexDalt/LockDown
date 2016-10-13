using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameController gameController;
    public UIController uiController;
    public Role role = Role.None;

    public override void OnStartLocalPlayer() {
        Debug.Log("Local Player Active");
        uiController.ShowRoleSelector();
        uiController.ChooseRoleEvent += CmdChooseRole;
    }

    public void OnDestroy() {
        uiController.HideRoleSelector();
    }

    [Command]
    public void CmdChooseRole(Role role) {
        if (gameController.ClaimRole(role, playerControllerId)) {
            this.role = role;
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
