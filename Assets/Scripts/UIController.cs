using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public delegate void ChooseRoleHandler(Role role);
    public event ChooseRoleHandler OnChooseRole;

    public GameObject roleUI;

	public void ShowRoleSelector() {
        roleUI.SetActive(true);
    }

    public void HideRoleSelector() {
        roleUI.SetActive(false);
    }

    public void ChooseRoleInfiltrator() {
        if (OnChooseRole != null) {
            OnChooseRole(Role.Infiltrator);
        }
    }

    public void ChooseRoleSecurity() {
        if (OnChooseRole != null) {
            OnChooseRole(Role.Security);
        }
    }
}