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
    private Vector2 mouseCoordinates;
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

    //////////////////
    // Unity Functions

    private void Start()
    {
        SetMap(defaultMap);
    }

    private void Update()
    {
        Tool currentTool = toolSelector.GetCurrentTool();

        // Calculate mouse coordinates
        mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseCoordinates.x = Mathf.Floor(mouseCoordinates.x);
        mouseCoordinates.y = Mathf.Floor(mouseCoordinates.y);

        // Handle tool preperations
        if (Input.GetMouseButtonDown(0))
            clickCoordinates = mouseCoordinates;
        
        // Handle tool usages
        if (Input.GetMouseButtonUp(0))
        {
            if (toolGrid.bounds.size.x > 1 || toolGrid.bounds.size.y > 1)
                currentTool.OnHoldRelease(toolGrid.bounds.min, toolGrid.bounds.size, ref buildings, obstructions);
            else
                currentTool.OnRelease(clickCoordinates, ref buildings, obstructions);
        }

        // Update tool previewer
        toolPreviewer.transform.position = mouseCoordinates;
        toolPreviewer.sprite = currentTool.GetPreviewSprite();

        // Update tool grid
        if (Input.GetMouseButton(0))
        {
            Vector2 selectionSize = mouseCoordinates - clickCoordinates;
            selectionSize.x += Mathf.Sign(selectionSize.x);
            selectionSize.y += Mathf.Sign(selectionSize.y);
            toolGrid.size = selectionSize;

            Vector2 selectionOffset;
            selectionOffset.x = (selectionSize.x < 0) ? 1 : 0;
            selectionOffset.y = (selectionSize.y < 0) ? 1 : 0;
            toolGrid.transform.position = clickCoordinates + selectionOffset;
        }
        else
        {
            toolGrid.size = Vector2.one;
            toolGrid.transform.position = mouseCoordinates;
        }
    }
}
