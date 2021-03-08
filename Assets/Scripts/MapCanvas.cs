using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCanvas : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectionGrid;
    private Vector2 mouseCoordinates;
    private Vector2 clickCoordinates;

    [SerializeField] private ToolSelector toolSelector;
    [SerializeField] private float toolClickLimit;
    private float toolClickTimer;

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

        // Handle click events
        if (Input.GetMouseButtonDown(0))
        {
            toolClickTimer = Time.time;
            clickCoordinates = mouseCoordinates;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - toolClickTimer > toolClickLimit)
                currentTool.OnHoldRelease(clickCoordinates, mouseCoordinates - clickCoordinates, ref buildings); // TODO: fix sizing of selection
            else
                currentTool.OnRelease(clickCoordinates, ref buildings);
        }


        // Change position & size of selection grid
        if (Input.GetMouseButton(0))
        {
            Vector2 selectionSize = mouseCoordinates - clickCoordinates;
            selectionSize.x += Mathf.Sign(selectionSize.x);
            selectionSize.y += Mathf.Sign(selectionSize.y);
            selectionGrid.size = selectionSize;

            Vector2 selectionOffset;
            selectionOffset.x = (selectionSize.x < 0) ? 1 : 0;
            selectionOffset.y = (selectionSize.y < 0) ? 1 : 0;
            selectionGrid.transform.position = clickCoordinates + selectionOffset;
        }
        else
        {
            selectionGrid.size = Vector2.one;
            selectionGrid.transform.position = mouseCoordinates;
        }
    }
}
