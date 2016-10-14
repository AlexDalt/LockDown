using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private Dictionary<int, Role> roles = new Dictionary<int, Role>();

    private int infiltrator = -1;
    private int security = -1;

    public NetworkController networkController;
    public UIController uiController;

    public List<Camera> cameras = new List<Camera>();

    public bool RoleClaimed(Role role) {
        switch (role) {
            case Role.Infiltrator:
                return (infiltrator != -1);
            case Role.Security:
                return (security != -1);
            default:
                return false;
        }
    }

    public Role GetRole(int connectionId) {
        if (roles.ContainsKey(connectionId)) {
            return roles[connectionId];
        }
        else {
            return Role.None;
        }
    }

    public bool ClaimRole(Role role, Player player) {

        int connectionId = player.connectionToClient.connectionId;
        Debug.Log(connectionId + " is changing their role");
        if (GetRole(connectionId) == Role.None && !RoleClaimed(role)) {
            switch (role) {
                case Role.Infiltrator:
                    infiltrator = connectionId;
                    roles.Add(connectionId, role);
                    networkController.ChangeRole(player, role);
                    return true;
                case Role.Security:
                    security = connectionId;
                    roles.Add(connectionId, role);
                    networkController.ChangeRole(player, role);
                    return true;
                default:
                    return false;
            }
        }
        else {
            return false;
        }
    }

    //private void ChangePlayerRole()
}
