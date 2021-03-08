using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTool : Tool
{
    private BuildingInfo info;

    public BuildingTool(BuildingInfo info)
    {
        this.info = info;
    }

    public override string GetName()
    {
        return info.name;
    }

    public override void OnRelease(Vector2 position, ref Building[,] buildings)
    {
        Debug.Log("On Release: " + GetName());

        Create(position, ref buildings);
    }

    public override void OnHoldRelease(Vector2 position, Vector2 selectionSize, ref Building[,] buildings)
    {
        Debug.Log("On Release Hold: " + GetName());

        for (int x = (int)position.x; x < position.x + selectionSize.x; x++)
            for (int y = (int)position.y; y < position.y + selectionSize.y; y++)
                Create(new Vector2(x, y), ref buildings);
    }

    private void Create(Vector2 position, ref Building[,] buildings)
    {
        // Check if there's nothing obstructing building
        bool isObstructed = false;
        for (int x = (int)info.obstructionOffset.x; x < info.obstructionSize.x + info.obstructionOffset.x && !isObstructed; x++)
        {
            for (int y = (int)info.obstructionOffset.y; y < info.obstructionSize.y + info.obstructionOffset.y && !isObstructed; y++)
            {
                isObstructed = buildings[(int)position.x + x, (int)position.y + y] != null;
            }
        }

        // Create building on no obstruction
        if (!isObstructed)
        {
            Building building = Building.Create(position, info);

            for (int x = (int)info.obstructionOffset.x; x < info.obstructionSize.x + info.obstructionOffset.x; x++)
                for (int y = (int)info.obstructionOffset.y; y < info.obstructionSize.y + info.obstructionOffset.y; y++)
                    buildings[(int)position.x + x, (int)position.y + y] = building;
        }
    }
}
