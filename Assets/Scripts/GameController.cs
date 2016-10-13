using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public short infiltrator = -1;
    public short security = -1;

    public bool HasRole(Role role) {
        switch (role) {
            case Role.Infiltrator:
                return (infiltrator != -1);
            case Role.Security:
                return (security != -1);
            default:
                return false;
        }
    }

    public bool ClaimRole(Role role, short playerId) {
        if (!HasRole(role)) {
            switch (role) {
                case Role.Infiltrator:
                    infiltrator = playerId;
                    return true;
                case Role.Security:
                    security = playerId;
                    return true;
                default:
                    return false;
            }
        }
        else {
            return false;
        }
    }
}
