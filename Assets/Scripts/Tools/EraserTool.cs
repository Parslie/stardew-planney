using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserTool : Tool
{
    public override string GetName()
    {
        return "Eraser";
    }

    public override void OnRelease(Vector2 position, ref Building[,] buildings)
    {
        Debug.Log("On Release: " + GetName());

        Building building = buildings[(int)position.x, (int)position.y];

        if (building != null)
            building.Delete();

        buildings[(int)position.x, (int)position.y] = null;
    }

    public override void OnHoldRelease(Vector2 position, Vector2 selectionSize, ref Building[,] buildings)
    {
        Debug.Log("On Release Hold: " + GetName());

        for (int x = (int)position.x; x < position.x + selectionSize.x; x++)
        {
            for (int y = (int)position.y; y < position.y + selectionSize.y; y++)
            {
                Building building = buildings[x, y];

                if (building != null)
                    building.Delete();

                buildings[x, y] = null;
            }
        }
    }
}
