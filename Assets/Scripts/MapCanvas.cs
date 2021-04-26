using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCanvas : MonoBehaviour
{
    [SerializeField] private Map defaultMap;
    [SerializeField] private SpriteRenderer mapRenderer;

    [Header("Tools")]
    [SerializeField] private ToolSelector toolSelector;
    [SerializeField] private SpriteRenderer toolGrid;
    [SerializeField] private SpriteRenderer toolPreviewer;
    private Vector2 clickCoordinates;

    private bool[,] obstructions;
    private Building[,] buildings;

    private void SetMap(Map map)
    {
        mapRenderer.sprite = map.sprite;

        // Delete all previous buildings
        if(buildings != null)
            for (int x = 0; x < buildings.GetLength(0); x++)
                for (int y = 0; y < buildings.GetLength(1); y++)
                    buildings[x, y].Delete();

        // Init variables
        buildings = new Building[map.size.x, map.size.y];
        obstructions = new bool[map.size.x, map.size.y];

        // Init obstructions
        if(map.obstructionMap != null)
            for(int x = 0; x < map.size.x; x++)
                for(int y = 0; y < map.size.y; y++)
                    obstructions[x, y] = map.obstructionMap.GetPixel(x, y) == Color.black;
    }

    private Vector2 GetMouseCoordinates()
    {
        Vector2 coordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coordinates.x = Mathf.Floor(coordinates.x);
        coordinates.y = Mathf.Floor(coordinates.y);

        return coordinates;
    }

    private void UseTool(Tool tool, Vector2 coordinate, Vector2 area)
    {
        if (Mathf.Abs(area.x) > 1 || Mathf.Abs(area.y) > 1)
            tool.OnHoldRelease(coordinate, area, ref buildings, obstructions);
        else
            tool.OnRelease(coordinate, ref buildings, obstructions);
    }

    //////////////////
    // Unity Functions

    private void Start()
    {
        SetMap(defaultMap);
    }

    private void Update()
    {
        Tool currentTool = toolSelector.GetCurrentTool();
        Vector2 mouseCoordinates = GetMouseCoordinates();

        if (Input.GetMouseButtonDown(0))
            clickCoordinates = mouseCoordinates;

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 offset;
            offset.x = (toolGrid.size.x < 0) ? -1 : 0;
            offset.y = (toolGrid.size.y < 0) ? -1 : 0;
            UseTool(currentTool, toolGrid.transform.position + (Vector3)offset, toolGrid.size);
        }

        if (Input.GetMouseButton(0))
        {
            // Update tool grid
            Vector2 toolGridSize = mouseCoordinates - clickCoordinates;
            toolGridSize.x += Mathf.Sign(toolGridSize.x);
            toolGridSize.y += Mathf.Sign(toolGridSize.y);
            toolGrid.size = toolGridSize;

            Vector2 toolGridPos = clickCoordinates;
            toolGridPos.x += (Mathf.Sign(toolGridSize.x) == -1) ? 1 : 0;
            toolGridPos.y += (Mathf.Sign(toolGridSize.y) == -1) ? 1 : 0;
            toolGrid.transform.position = toolGridPos;

            // Update tool previewer
            toolPreviewer.transform.position = clickCoordinates;
            toolPreviewer.sprite = currentTool.GetPreviewSprite();
        }
        else
        {
            // Update tool grid
            toolGrid.size = Vector2.one;
            toolGrid.transform.position = mouseCoordinates;

            // Update tool previewer
            toolPreviewer.transform.position = mouseCoordinates;
            toolPreviewer.sprite = currentTool.GetPreviewSprite();
        }
    }
}
