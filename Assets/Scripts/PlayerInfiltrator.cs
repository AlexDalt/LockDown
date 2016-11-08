using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfiltrator : Player {

    private GameController gameController;
    private UIController uiController;

    private int interactableLayer;

    public override void OnStartLocalPlayer() {
        Init();
        uiController.ShowRoleUI(false);
    }

    public void Init() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = gameController.uiController;
        interactableLayer = LayerMask.GetMask(new string[] { "Interactable" });
    }

    void Update() {
        if (isLocalPlayer) {
            transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);

            //Move camera into object
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;
            //Shift camera up to eyes
            Camera.main.transform.Translate(0, 1.55f, 0);

            RaycastHit hit;

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactableLayer)) {
                Debug.Log("Raycast hit: " + hit.collider.name);
            }
        }
    }

}
