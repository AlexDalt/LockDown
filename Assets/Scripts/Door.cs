using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject leftPanel;
    public GameObject rightPanel;
    public float displacement = 0;

    float distance;
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

        leftPanelInitialPosition = leftPanel.transform.position;
        rightPanelInitialPosition = rightPanel.transform.position;

        // Find the movement vector and normalise
        Vector3 rightDirection = (rightPanel.transform.position - leftPanel.transform.position);
        distance = rightDirection.magnitude;
        rightDirection.Normalize();

        leftPanelOpenDisplacement = ((-rightDirection) * distance);
        rightPanelOpenDisplacement = (rightDirection * distance);
    }

    void Update() {
        leftPanel.transform.position = leftPanelInitialPosition + (displacement * leftPanelOpenDisplacement);
        rightPanel.transform.position = rightPanelInitialPosition + (displacement * rightPanelOpenDisplacement);
    }

    public void ToggleDoor() {
        Open = !Open;
    }
}
