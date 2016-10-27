using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes an object that can be interacted with
/// </summary>
public interface IInteractable {

    /// <summary>
    /// Get the interaction options this object supports for the provided role
    /// </summary>
    /// <param name="role">The role to get interactions for</param>
    /// <returns>A list of options that can be performed on this object by the provided role</returns>
    string[] GetOptions(Role role);

    /// <summary>
    /// Perform the action represented by the index of the string array returned by GetOptions() for the provided role
    /// <para />Note: this should only be called server-side, and any changes passed to the clients via [ClientRpc] or [SyncVar]
    /// </summary>
    /// <param name="role">The role performing the interaction</param>
    /// <param name="option">The array index representing the action to perform</param>
    /// <returns>true if successful, false if not</returns>
    bool Interact(Role role, int option);
}
