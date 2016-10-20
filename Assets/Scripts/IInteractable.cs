using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    string[] getOptions();
    void interact(int option);
}
