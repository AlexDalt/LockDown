using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Door class controls the movement of the door panels.
/// The displacement variable is controlled via Mecanim (Unity's animation system)
/// </summary>
public class Door : MonoBehaviour, IInteractable {
    public GameObject leftPanel;
    public GameObject rightPanel;
    public float displacement = 0;
    
    Animator animator;
    Vector3 leftPanelInitialPosition;
    Vector3 rightPanelInitialPosition;
    Vector3 leftPanelOpenDisplacement;
    Vector3 rightPanelOpenDisplacement;

    bool open;
    public bool Open {
        get { return open; }
        set { open = value; animator.SetBool("Open", value); }
    }
    
    void Start () {
        animator = GetComponent<Animator>();

        // Use the panels' relative intial positions to find their open positions
        leftPanelInitialPosition = leftPanel.transform.position;
        rightPanelInitialPosition = rightPanel.transform.position;

        Vector3 distanceVector = (rightPanel.transform.position - leftPanel.transform.position);

        leftPanelOpenDisplacement = -distanceVector;
        rightPanelOpenDisplacement = distanceVector;
    }

    void Update() {
        leftPanel.transform.position = leftPanelInitialPosition + (displacement * leftPanelOpenDisplacement);
        rightPanel.transform.position = rightPanelInitialPosition + (displacement * rightPanelOpenDisplacement);
    }

    /// <summary>
    /// Method only present for debugging
    /// </summary>
    public void ToggleDoor() {
        Open = !Open;
    }

    // IInteractable Method
    public string[] GetOptions(Role role) {
        return new string[] { "Open", "Close" };
    }

    // IInteractable Method
    public bool Interact(Role role, int index) {
        switch(index) {
            case 0:
                Open = true;
                break;
            case 1:
                Open = false;
                break;
            default:
                return false;
        }
        return true;
    }
}
