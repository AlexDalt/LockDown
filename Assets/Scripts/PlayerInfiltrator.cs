using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfiltrator : Player {

    void Update() {
        if (isLocalPlayer) {
            transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
        }
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
    }

}
