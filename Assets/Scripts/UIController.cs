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

    public GameObject cameraOverlayPrefab;
    public GameObject cameraGridUI;

    public GameObject mainUI;

    private CameraOverlay mainCameraOverlay;

    public List<Camera> cameras = new List<Camera>();

    private List<CameraOverlay> cameraOverlays = new List<CameraOverlay>();

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
            //cameraUI.SetActive(true);

            if (Display.displays.Length > 1) {
                //If a second display is present, push camera grid to it

                Display.displays[1].Activate();
                Display.displays[1].SetRenderingResolution(1920, 1080);

                cameraGridUI.SetActive(true);
                cameraGridUI.GetComponent<Canvas>().targetDisplay = 1;

                //Display main camera under canvas 0
                mainCameraOverlay = Instantiate(cameraOverlayPrefab, mainUI.transform).GetComponent<CameraOverlay>();
                mainCameraOverlay.Init("Main Camera", true);

                for (int i = 0; i < cameras.Count; i++) {
                    cameras[i].rect = new Rect((i % 2) * 0.5f, 0.5f - ((i / 2) * 0.5f), 0.5f, 0.5f);
                    cameras[i].gameObject.SetActive(true);
                    cameras[i].targetDisplay = 1;

                    GameObject newOverlay = Instantiate(cameraOverlayPrefab, cameraGridUI.transform);
                    CameraOverlay newOverlayScript = newOverlay.GetComponent<CameraOverlay>();

                    newOverlayScript.Init(i, cameras[i].name, 2, 2, i);
                    newOverlayScript.OnClick += ChangeCamera;

                    cameraOverlays.Add(newOverlayScript);
                }
            }
            else {
                //If there's only a single display, display cameras in column on right

                Camera.main.GetComponent<Camera>().rect = new Rect(0, 0, 0.75f, 1);
                cameraGridUI.SetActive(true);

                //Add main camera
                mainCameraOverlay = Instantiate(cameraOverlayPrefab, cameraGridUI.transform).GetComponent<CameraOverlay>();
                mainCameraOverlay.Init("Main Camera", false);

                for (int i = 0; i < cameras.Count; i++) {
                    cameras[i].rect = new Rect(0.75f, 0.75f - (i * 0.25f), 0.25f, 0.25f);
                    cameras[i].gameObject.SetActive(true);

                    GameObject newOverlay = Instantiate(cameraOverlayPrefab, cameraGridUI.transform);
                    CameraOverlay newOverlayScript = newOverlay.GetComponent<CameraOverlay>();

                    newOverlayScript.Init(i, cameras[i].name, i);
                    newOverlayScript.OnClick += ChangeCamera;

                    cameraOverlays.Add(newOverlayScript);
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

        mainCameraOverlay.CameraName = cameras[id].name;
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