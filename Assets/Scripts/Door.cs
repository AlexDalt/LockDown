using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject leftPanel;
    public GameObject rightPanel;
    public float speed;
    public float distance;
    public float proximity;

    Vector3 leftPanelInitialPosition;
    Vector3 rightPanelInitialPosition;
    Vector3 leftPanelOpenPosition;
    Vector3 rightPanelOpenPosition;

    bool open = false;

	// Use this for initialization
	void Start () {
        leftPanelInitialPosition = leftPanel.transform.position;
        rightPanelInitialPosition = rightPanel.transform.position;

        // Find the movement vector and normalise
        Vector3 rightDirection = (rightPanel.transform.position - leftPanel.transform.position);
        rightDirection.Normalize();

        leftPanelOpenPosition = leftPanelInitialPosition + ((-rightDirection) * distance/2);
        rightPanelOpenPosition = rightPanelInitialPosition + (rightDirection * distance/2);
    }

    void Update() {
        float step = speed * Time.deltaTime;
        Vector3 leftTarget;
        Vector3 rightTarget;

        if (open) {
            leftTarget = leftPanelOpenPosition;
            rightTarget = rightPanelOpenPosition;
        } else {
            leftTarget = leftPanelInitialPosition;
            rightTarget = rightPanelInitialPosition;
        }

        leftPanel.transform.position = Vector3.MoveTowards(leftPanel.transform.position, leftTarget, step);
        rightPanel.transform.position = Vector3.MoveTowards(rightPanel.transform.position, rightTarget, step);
    }

    void openDoor() {
        open = true;
    }

    void closeDoor() {
        open = false;
    }

    void toggleDoor() {
        open = !open;
    }	
}
