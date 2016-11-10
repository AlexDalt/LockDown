using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfiltrator : Player {

    private GameController gameController;
    private UIController uiController;

    private int interactableLayer;

    private bool initialised;
    private float debounce = 0f;


    public override void OnStartServer() {
        Init();
    }

    public override void OnStartLocalPlayer() {
        Init();
        uiController.ShowRoleUI(false);
        uiController.ShowInfiltratorUI(true);

        if (isLocalPlayer)
        {
            //Move camera into object
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;
            //Shift camera up to eyes
            Camera.main.transform.Translate(0, 1.0f, 0);
            Camera.main.transform.SetParent(this.transform);
        }
    }

    public void Init() {
        if (!initialised) {
            initialised = true;
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            uiController = gameController.uiController;
            interactableLayer = LayerMask.GetMask(new string[] { "Interactable" });
        }
    }

    [Command]
    void CmdInteractWith(NetworkInstanceId netId, int option) {
        Debug.Log("GameController is null: " + (gameController == null));
        Debug.Log("Choosing option " + option + " on object " + netId);
        gameController.InteractWith(netId, Role.Infiltrator, option);
    }

    void Update() {
        if (isLocalPlayer) {

            RaycastHit hit;

            //Detect interactable object
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactableLayer)) {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null) {
                    string[] options = interactable.GetOptions(Role.Infiltrator);
                    uiController.ShowInteractionText(options);
                    if (options.Length > 0) {

                        if (Input.GetAxis("Fire1") > 0 && debounce < (Time.time - 0.25f)) {
                            CmdInteractWith(hit.collider.GetComponent<NetworkIdentity>().netId, 0);
                            debounce = Time.time;
                        }
                    }
                }
                else {
                    uiController.ShowInteractionText(null);
                }
            }
            else {
                uiController.ShowInteractionText(null);
            }
        }
    }

}
