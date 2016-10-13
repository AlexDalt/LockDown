using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkManager {

    public UIController uiController;
    public GameController gameController;

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {

        Debug.Log("Player " + playerControllerId + " has joined");
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        PlayerController controller = player.GetComponent<PlayerController>();
        controller.uiController = uiController;
        controller.gameController = gameController;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

}
