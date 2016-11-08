using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public delegate void ChooseRoleHandler(Role role);
    public event ChooseRoleHandler OnChooseRole;

    public delegate void ChooseCameraHandler(int id);
    public event ChooseCameraHandler OnChooseCamera;

    public GameObject roleUI;
    public GameObject cameraUI;
    public GameObject infiltratorUI;

    public GameObject interactionText;
    public GameObject infiltratorScoreText;

    public GameObject cameraOverlayPrefab;
    public GameObject cameraGridUI;

    public GameObject mainUI;

    private CameraOverlay mainCameraOverlay;

    public List<ViewController> cameras = new List<ViewController>();

    private int selectedCameraID = 0;
    public int SelectedCameraID {
        get {
            return selectedCameraID;
        }
        set {
            selectedCameraID = value;
            SelectedCamera = cameras[value];
        }
    }
    public ViewController SelectedCamera { get; private set; }
    public bool IsDroneSelected { get; private set; }

    private bool active = false;

    private List<CameraOverlay> cameraOverlays = new List<CameraOverlay>();

    public void ShowRoleUI(bool active) {
        roleUI.SetActive(active);
    }

    public void ShowInfiltratorUI(bool active) {
        infiltratorUI.SetActive(active);
    }
    /*public void SwapCameras() {
        cameras[i].rect = new Rect((i % 2) * 0.5f, 0.5f - ((i / 2) * 0.5f), 0.5f, 0.5f);
        cameras[i].gameObject.SetActive(true);
        cameras[i].targetDisplay = 1;
    }*/

    public void ShowSecurityUI(bool active) {
        Debug.Log("Screens detected: " + Display.displays.Length);

        this.active = active;
        if (active) {
            //cameraUI.SetActive(true);
            //if (true) {
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
                    cameras[i].camera.rect = new Rect((i % 2) * 0.5f, 0.5f - ((i / 2) * 0.5f), 0.5f, 0.5f);
                    cameras[i].camera.gameObject.SetActive(true);
                    cameras[i].camera.targetDisplay = 1;

                    GameObject newOverlay = Instantiate(cameraOverlayPrefab, cameraGridUI.transform);
                    CameraOverlay newOverlayScript = newOverlay.GetComponent<CameraOverlay>();

                    newOverlayScript.Init(i, cameras[i].name, i, 2, 2);
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
                    cameras[i].camera.rect = new Rect(0.75f, 0.75f - (i * 0.25f), 0.25f, 0.25f);
                    cameras[i].camera.gameObject.SetActive(true);

                    GameObject newOverlay = Instantiate(cameraOverlayPrefab, cameraGridUI.transform);
                    CameraOverlay newOverlayScript = newOverlay.GetComponent<CameraOverlay>();

                    newOverlayScript.Init(i, cameras[i].name, i);
                    newOverlayScript.OnClick += ChangeCamera;

                    cameraOverlays.Add(newOverlayScript);
                }
            }

            ChangeCamera(0);
        }
        else {
            cameraUI.SetActive(false);
            Camera.main.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
            foreach (ViewController controller in cameras) {
                controller.camera.gameObject.SetActive(false);
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
        SelectedCameraID = id;
        MimicCamera(id);

        if (cameras[id].GetComponent<DroneController>()) {
            IsDroneSelected = true;
        }
        else {
            IsDroneSelected = false;
        }

        mainCameraOverlay.CameraName = cameras[id].name;
    }

    private void MimicCamera(int id) {
        Camera.main.transform.position = cameras[id].camera.transform.position;
        Camera.main.transform.rotation = cameras[id].camera.transform.rotation;
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
    
    void Update() {
        if (active) {
            MimicCamera(SelectedCameraID);
        }
    }

    public void ShowInteractionText(string[] options) {
        if (options != null && options.Length > 0) {
            interactionText.SetActive(true);
            interactionText.GetComponent<Text>().text = options[0];
        }
        else {
            interactionText.SetActive(false);
        }
    }

    public void UpdateInfiltratorScore(int value) {
        infiltratorScoreText.GetComponent<Text>().text = "$" + value;
    }
}