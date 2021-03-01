using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Stardew/Building")]
public class BuildingInfo : ScriptableObject
{
    public Sprite sprite;
    public Vector2 obstructionArea;
    public Vector2 obstructionOffset;
    public Vector2 absoluteObstructionArea;
    public bool ignoreObstruction;
}
