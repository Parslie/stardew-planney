using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserTool : Tool
{
    public override string GetName()
    {
        return "Eraser";
    }

    public override Sprite GetPreviewSprite()
    {
        return null;
    }

    public override void OnRelease(Vector2 position, ref Building[,] buildings, bool[,] obstructions)
    {
        Building building = buildings[(int)position.x, (int)position.y];

        if (building != null)
            building.Delete();

        buildings[(int)position.x, (int)position.y] = null; // TODO: go over all of this object's obstruction area (if that pos contains building) (if not already null)
    }

    public override void OnHoldRelease(Vector2 position, Vector2 selectionSize, ref Building[,] buildings, bool[,] obstructions)
    {
        int deltaX = (int)Mathf.Sign(selectionSize.x);
        int deltaY = (int)Mathf.Sign(selectionSize.y);

        for (int x = (int)position.x; x != position.x + selectionSize.x; x += deltaX)
        {
            for (int y = (int)position.y; y != position.y + selectionSize.y; y += deltaY)
            {
                Building building = buildings[x, y];

                if (building != null)
                    building.Delete();

                buildings[x, y] = null; // TODO: go over all of this object's obstruction area (if that pos contains building) (if not already null)
            }
        }
    }
}
