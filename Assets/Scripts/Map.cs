using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Stardew/Map")]
public class Map : ScriptableObject
{
    public new string name;
    public Vector2Int size;
    public Sprite sprite;
    public Texture2D obstructionMap;
}
