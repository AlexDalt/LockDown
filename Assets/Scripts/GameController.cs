using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

    private Dictionary<int, Role> roles = new Dictionary<int, Role>();

    [SyncVar(hook = "OnInfiltratorScoreChange")]
    public int infiltratorScore = 0;
    [SyncVar]
    public int securityScore = 0;

    private int infiltrator = -1;
    private int security = -1;

    public NetworkController networkController;
    public UIController uiController;
	public GameObject map;
	public Camera mapCam;

    public bool RoleClaimed(Role role) {
        switch (role) {
            case Role.Infiltrator:
                return (infiltrator != -1);
            case Role.Security:
                return (security != -1);
            default:
                return false;
        }
    }

    public Role GetRole(int connectionId) {
        if (roles.ContainsKey(connectionId)) {
            return roles[connectionId];
        }
        else {
            return Role.None;
        }
    }

    public bool ClaimRole(Role role, Player player) {

        int connectionId = player.connectionToClient.connectionId;
        Debug.Log(connectionId + " is changing their role");
        if (GetRole(connectionId) == Role.None && !RoleClaimed(role)) {
            switch (role) {
                case Role.Infiltrator:
                    infiltrator = connectionId;
                    roles.Add(connectionId, role);
                    networkController.ChangeRole(player, role);
                    return true;
                case Role.Security:
                    security = connectionId;
                    roles.Add(connectionId, role);
                    networkController.ChangeRole(player, role);
                    return true;
                default:
                    return false;
            }
        }
        else {
            return false;
        }
    }

    public bool RequestControl(NetworkConnection conn, NetworkInstanceId objectId) {
        return networkController.RequestControl(conn, objectId);
    }
    
	/// <summary>
	/// Initiates the map cam, so the map fills the screen
	/// </summary>
	public void InitMapCam(){
		Bounds bounds = GetBounds (map);
		mapCam.transform.position = (new Vector3(bounds.center.x,15,bounds.center.z));
		float xsize = bounds.extents.x;
		float zsize = bounds.extents.z;
		if (zsize > xsize) {
			mapCam.orthographicSize = zsize;
		}
		else {
			mapCam.orthographicSize = xsize * Screen.height / Screen.width;
		}
	}

	/// <summary>
	/// Gets the bounds of a group of objects (for initating the Map Cam)
	/// </summary>
	/// <returns>The bounds.</returns>
	/// <param name="objeto">Objeto.</param>
	Bounds GetBounds(GameObject objeto){
		Bounds bounds;
		Renderer childRender;
		bounds = GetRenderBounds(objeto);
		if(bounds.extents.x == 0){
			bounds = new Bounds(objeto.transform.position,Vector3.zero);
			foreach (Transform child in objeto.transform) {
				childRender = child.GetComponent<Renderer>();
				if (childRender) {
					bounds.Encapsulate(childRender.bounds);
				}else{
					bounds.Encapsulate(GetBounds(child.gameObject));
				}
			}
		}
		return bounds;
	}

	/// <summary>
	/// Gets the render bounds of an object (helper function for getBounds)
	/// </summary>
	/// <returns>The render bounds.</returns>
	/// <param name="objeto">Objeto.</param>
	Bounds GetRenderBounds(GameObject objeto){
		Bounds bounds = new  Bounds(Vector3.zero,Vector3.zero);
		Renderer render = objeto.GetComponent<Renderer>();
		if(render!=null){
			return render.bounds;
		}
		return bounds;
	}

    /// <summary>
    /// Report an item as stolen and adjust the score appropriately
    /// </summary>
    /// <param name="item">Struct representing the item stolen</param>
    public void ItemStolen(Item item) {
        infiltratorScore += item.value;
        securityScore -= item.value;
    }

    /// <summary>
    /// Interact with a networked object
    /// </summary>
    /// <param name="netId">The netId of the object to interact with</param>
    /// <param name="role">The role of the player performing the interaction</param>
    /// <param name="option">The index of the option they wish to perform</param>
    /// <returns>Returns true if successful</returns>
    public bool InteractWith(NetworkInstanceId netId, Role role, int option) {
        if (isServer) {
            Debug.Log("Player is interacting with object "+netId);
            GameObject gameObject = NetworkServer.FindLocalObject(netId);
            if (gameObject != null) {
                Debug.Log("Found object " + netId);
                IInteractable interactable = gameObject.GetComponent<IInteractable>();
                if (interactable != null) {
                    Debug.Log("Found interactable on object " + netId);
                    return interactable.Interact(role, option);
                }
            }
        }
        return false;
    }

    void OnInfiltratorScoreChange(int updated) {
        uiController.UpdateInfiltratorScore(updated);
    }
}
