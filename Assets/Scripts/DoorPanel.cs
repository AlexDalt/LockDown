using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script simply provides each door with a public variable 'displacement'
/// The value of displacement ranges from 0 (initial position) to 100 (Open position)
/// 
/// The displacement -> position mapping is done by the door controller.
/// </summary>
public class DoorPanel : MonoBehaviour {
    public int displacement = 0;
}
