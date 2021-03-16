using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCanvas : MonoBehaviour
{
    [SerializeField] private SpriteRenderer toolGrid;
    [SerializeField] private SpriteRenderer toolPreviewer;
    private Vector2 mouseCoordinates;
    private Vector2 clickCoordinates;

    [SerializeField] private ToolSelector toolSelector;

    private Building[,] buildings;

    //////////////////
    // Unity Functions

    private void Awake()
    {
        buildings = new Building[420,420]; // TODO: remove, this is for debug purposes
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
                currentTool.OnHoldRelease(toolGrid.bounds.min, toolGrid.bounds.size, ref buildings);
            else
                currentTool.OnRelease(clickCoordinates, ref buildings);
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
