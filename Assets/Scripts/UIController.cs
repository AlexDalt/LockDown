using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public delegate void OnChooseRole(Role role);
    public event OnChooseRole ChooseRoleEvent;

    public GameObject roleUI;

	public void ShowRoleSelector() {
        roleUI.SetActive(true);
    }

    public void HideRoleSelector() {
        roleUI.SetActive(false);
    }

    public void ChooseRoleInfiltrator() {
        if (ChooseRoleEvent != null) {
            ChooseRoleEvent(Role.Infiltrator);
        }
    }

    public void ChooseRoleSecurity() {
        if (ChooseRoleEvent != null) {
            ChooseRoleEvent(Role.Security);
        }
    }
}