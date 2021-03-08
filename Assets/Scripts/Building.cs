using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    private Vector2 obstructionAreaOrigin;
    private Vector2 obstructionAreaSize;
    private new SpriteRenderer renderer;

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void PickUp()
    {

    }

    public void PutDown()
    {

    }

    public static Building Create(Vector2 position, BuildingInfo info)
    {
        Building building = new GameObject(info.name, typeof(Building)).GetComponent<Building>();
        SpriteRenderer buildingRenderer = building.GetComponent<SpriteRenderer>();

        building.transform.position = position;
        building.obstructionAreaOrigin = building.transform.position + (Vector3)info.obstructionOffset;
        building.obstructionAreaSize = info.obstructionSize;

        buildingRenderer.sprite = info.sprite;
        buildingRenderer.sortingOrder = -(int)position.y;
        buildingRenderer.sortingLayerName = "Foreground";

        return building;
    }

    private bool IsHoveringBehind()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool isHoveringOver = mousePosition.x >= obstructionAreaOrigin.x &&
            mousePosition.x <= obstructionAreaOrigin.x + obstructionAreaSize.x &&
            mousePosition.y >= obstructionAreaOrigin.y &&
            mousePosition.y <= obstructionAreaOrigin.y + obstructionAreaSize.y;
        if (isHoveringOver)
            return false;

        return mousePosition.x >= renderer.bounds.min.x &&
            mousePosition.x <= renderer.bounds.max.x &&
            mousePosition.y >= renderer.bounds.min.y &&
            mousePosition.y <= renderer.bounds.max.y;
    }

    //////////////////
    // Unity Functions

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsHoveringBehind())
            renderer.color = new Color(1, 1, 1, 0.5f);
        else
            renderer.color = new Color(1, 1, 1, 1);
    }
}
