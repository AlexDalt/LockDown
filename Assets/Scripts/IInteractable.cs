using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    string[] GetOptions();
    void Interact(int option);
}
