using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public delegate void ChooseRoleHandler(Role role);
    public event ChooseRoleHandler OnChooseRole;

    public delegate void ChooseCameraHandler(int id);
    public event ChooseCameraHandler OnChooseCamera;

    public GameObject roleUI;
    public GameObject cameraUI;

	public void ShowRoleSelector() {
        roleUI.SetActive(true);
    }

    public void HideRoleSelector() {
        roleUI.SetActive(false);
    }

    public void ShowCameraUI(bool active) {
        cameraUI.SetActive(active);
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

    public void ChooseCamera(int id) {
        Debug.Log("Chose camera " + id);
        if (OnChooseCamera != null) {
            OnChooseCamera(id);
        }
    }
}