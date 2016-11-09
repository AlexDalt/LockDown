using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collectable item description
/// </summary>
public struct Item {
    public string name;
    public int value;

    /// <summary>
    /// Create a new collectable item description
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The value of that item</param>
    public Item(string name, int value) {
        this.name = name;
        this.value = value;
    }
}