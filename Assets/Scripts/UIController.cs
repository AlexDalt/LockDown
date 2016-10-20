using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

    public delegate void ChooseRoleHandler(Role role);
    public event ChooseRoleHandler OnChooseRole;

    public delegate void ChooseCameraHandler(int id);
    public event ChooseCameraHandler OnChooseCamera;

    public GameObject roleUI;
    public GameObject cameraUI;

    public List<Camera> cameras = new List<Camera>();

    public void ShowRoleUI(bool active) {
        roleUI.SetActive(active);
    }

    /*public void SwapCameras() {
        cameras[i].rect = new Rect((i % 2) * 0.5f, 0.5f - ((i / 2) * 0.5f), 0.5f, 0.5f);
        cameras[i].gameObject.SetActive(true);
        cameras[i].targetDisplay = 1;
    }*/

    public void ShowSecurityUI(bool active) {
        Debug.Log("Screens detected: " + Display.displays.Length);

        if (active) {
            cameraUI.SetActive(true);

            if (Display.displays.Length > 1) {
                Display.displays[1].Activate();
                Display.displays[1].SetRenderingResolution(1920, 1080);
                for (int i = 0; i < cameras.Count; i++) {
                        cameras[i].rect = new Rect((i % 2) * 0.5f, 0.5f - ((i / 2) * 0.5f), 0.5f, 0.5f);
                        cameras[i].gameObject.SetActive(true);
                        cameras[i].targetDisplay = 1;
                }
            }
            else {
                Camera.main.GetComponent<Camera>().rect = new Rect(0, 0, 0.75f, 1);
                for (int i = 0; i < cameras.Count; i++) {
                    cameras[i].rect = new Rect(0.75f, 0.75f - (i * 0.25f), 0.25f, 0.25f);
                    cameras[i].gameObject.SetActive(true);
                }
            }
        }
        else {
            cameraUI.SetActive(false);
            Camera.main.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
            foreach (Camera camera in cameras) {
                camera.gameObject.SetActive(false);
            }
        }
    }

    public void CameraGridClick(BaseEventData data) {
        if (data.GetType() == typeof(PointerEventData)) {
            PointerEventData pointerData = (PointerEventData)data;
            //Vector2 point = pointerData.enterEventCamera.ScreenToViewportPoint(pointerData.position);
            //pointerData.lastPress.GetComponent<RectTransform>().
            Debug.Log("Clicked at " + pointerData);
        }
        
    }

    public void ChangeCamera(int id) {
        Camera.main.transform.position = cameras[id].transform.position;
        Camera.main.transform.rotation = cameras[id].transform.rotation;
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