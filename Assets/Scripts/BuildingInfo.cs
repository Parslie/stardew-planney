using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Stardew/Building")]
public class BuildingInfo : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public Vector2 obstructionOffset;
    public Vector2 obstructionSize;
    public Vector2 absoluteObstructionSize;
    public bool ignoreObstruction;
}
