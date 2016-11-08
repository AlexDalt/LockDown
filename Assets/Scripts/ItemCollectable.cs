using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Script to control player interactions with an object that can be stolen
/// </summary>
[RequireComponent(typeof(Collider))]
public class ItemCollectable : NetworkBehaviour, IInteractable {

    [SerializeField]
    private int value = 0;

    private GameController gameController;

    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
    
    public string[] GetOptions(Role role) {
        if (role == Role.Infiltrator) {
            return new string[] {
                "Steal " + name + " (+$" + value + ")"
            };
        }
        else {
            return new string[0];
        }
    }

    public bool Interact(Role role, int option) {
        Debug.Log("Being stolen");
        if (isServer) {
            gameController.ItemStolen(new Item(name, value));
            NetworkServer.Destroy(gameObject);
            return true;
        }
        else {
            return false;
        }
    }
}
