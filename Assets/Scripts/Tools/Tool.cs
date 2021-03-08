using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool
{
    public abstract string GetName();
    public abstract void OnRelease(Vector2 position, ref Building[,] buildings);
    public abstract void OnHoldRelease(Vector2 position, Vector2 selectionSize, ref Building[,] buildings);
}
