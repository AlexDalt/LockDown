using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private Dictionary<short, Role> roles = new Dictionary<short, Role>();

    private short infiltrator = -1;
    private short security = -1;

    public NetworkController networkController;

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

    public Role GetRole(short playerId) {
        if (roles.ContainsKey(playerId)) {
            return roles[playerId];
        }
        else {
            return Role.None;
        }
    }

    public bool ClaimRole(Role role, Player player) {

        short playerId = player.playerControllerId;
        Debug.Log(playerId + " is changing their role");
        if (GetRole(playerId) == Role.None && !RoleClaimed(role)) {
            switch (role) {
                case Role.Infiltrator:
                    infiltrator = playerId;
                    roles.Add(playerId, role);
                    networkController.ChangeRole(player, role);
                    return true;
                case Role.Security:
                    security = playerId;
                    roles.Add(playerId, role);
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
