using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {

    private Dictionary<int, Role> roles = new Dictionary<int, Role>();

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
}
