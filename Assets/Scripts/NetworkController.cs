﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkManager {

    public UIController uiController;
    public GameController gameController;

    [Header("Player Prefabs")]
    public GameObject infiltratorPrefab;
    public GameObject securityPrefab;

    public List<Player> players = new List<Player>();

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {

        Debug.Log("Player " + playerControllerId + " has joined");
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        PlayerLobby controller = player.GetComponent<PlayerLobby>();
        controller.uiController = uiController;
        controller.gameController = gameController;

        players.Add(controller);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player) {

        base.OnServerRemovePlayer(conn, player);
        players.Remove(player.gameObject.GetComponent<Player>());

    }

    public void ChangeRole(Player player, Role role) {

        GameObject newPlayer;
        switch (role) {
            case Role.Infiltrator:
                newPlayer = Instantiate(infiltratorPrefab);
                break;
            case Role.Security:
                newPlayer = Instantiate(securityPrefab);
                break;
            default:
                return;
        }

        NetworkServer.ReplacePlayerForConnection(player.connectionToClient, newPlayer, player.playerControllerId);
        players[players.IndexOf(player)] = newPlayer.GetComponent<Player>();
        Destroy(player.gameObject);

        Debug.Log("Role changed to " + role);

    }

}
