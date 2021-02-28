using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    private Vector2 obstructionAreaOrigin;
    private Vector2 obstructionAreaSize;
    private new SpriteRenderer renderer;

    // TODO: implement as a object pool item
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

    // TODO: implement as a object pool item
    public void Create(Vector2 position, BuildingInfo info)
    {
        transform.position = position;
        renderer.sprite = info.sprite;
        obstructionAreaOrigin = transform.InverseTransformPoint(info.obstructionOffset);
        obstructionAreaSize = info.obstructionArea;
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

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        // TODO: REMOVE; THIS IS FOR DEBUGGING ONLY!
        obstructionAreaOrigin = transform.position - new Vector3(1, 1, 0);
        obstructionAreaSize = Vector2.one * 3;
    }

    private void Update()
    {
        if (IsHoveringBehind())
            renderer.color = new Color(1, 1, 1, 0.5f);
        else
            renderer.color = new Color(1, 1, 1, 1);
    }
}
