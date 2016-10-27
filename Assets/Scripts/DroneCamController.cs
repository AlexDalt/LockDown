using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates drone camera up and down without rotating the drone itself
/// </summary>

public class DroneCamController : MonoBehaviour {
    public int minAngle = 0;
    public int maxAngle = 90;
	public void Up()

    {
        if((gameObject.transform.localEulerAngles.x >= minAngle +1) && (gameObject.transform.localEulerAngles.x <= maxAngle))
        {
            transform.Rotate(Vector3.left);
        }
        
    }
    
    public void Down()
    {
        if ((gameObject.transform.localEulerAngles.x >= minAngle) && (gameObject.transform.localEulerAngles.x <= maxAngle -1)) { 
            transform.Rotate(Vector3.right);
        }
    }
}
