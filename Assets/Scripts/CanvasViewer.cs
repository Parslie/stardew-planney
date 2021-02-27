using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CanvasViewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer canvasSprite;
    [SerializeField] private int canvasMargin;

    // Zoom is half of viewport height
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    private float maxZoom;

    private Vector2 minPosition;
    private Vector2 maxPosition;
    private Vector2 currentPosition;
    private Vector2 originalMousePosition;

    private new Camera camera;
    private float prevCameraAspect;

    private void CalculatePositionBounds()
    {
        Bounds canvasBounds = canvasSprite.bounds;
        minPosition.x = canvasBounds.min.x;
        maxPosition.x = canvasBounds.max.x;
        minPosition.y = canvasBounds.min.y;
        maxPosition.y = canvasBounds.max.y;
    }

    private void CalculateZoomBounds()
    {
        Bounds canvasBounds = canvasSprite.bounds;

        float maxZoomHorizontal = (canvasBounds.size.x / 2 + canvasMargin) / camera.aspect;
        float maxZoomVertical = canvasBounds.size.y / 2 + canvasMargin;

        maxZoom = Mathf.Max(maxZoomVertical, maxZoomHorizontal);
    }

    //////////////////
    // Unity Functions

    private void Start()
    {
        camera = GetComponent<Camera>();

        CalculateZoomBounds();
        camera.orthographicSize = maxZoom;

        CalculatePositionBounds();
        currentPosition = canvasSprite.bounds.center;

        prevCameraAspect = camera.aspect;
    }

    private void Update()
    {
        // Change current zoom
        if (prevCameraAspect != camera.aspect)
            CalculateZoomBounds();

        // Cheat way of handling dynamic zoom speed (works for low values of zoomSpeed)
        float zoomDelta = Input.mouseScrollDelta.y * zoomSpeed * camera.orthographicSize;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomDelta, minZoom, maxZoom);

        // Change current position
        if (Input.GetMouseButtonDown(2))
            originalMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        else if (Input.GetMouseButton(2))
            currentPosition += originalMousePosition - (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);

        currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
        transform.position = currentPosition;

        // Prepare re-calculation
        prevCameraAspect = camera.aspect;
    }
}
