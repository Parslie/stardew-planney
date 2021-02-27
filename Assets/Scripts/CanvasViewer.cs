using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CanvasViewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer canvasSprite;
    [SerializeField] private int canvasMargin;

    // Zoom is half of viewport height
    private float minZoom;
    private float maxZoom;
    private float prevZoom;

    private Vector2 minPosition;
    private Vector2 maxPosition;
    private Vector2 currentPosition;
    private Vector2 originalMousePosition;

    private new Camera camera;
    private float prevCameraAspect;

    private void CalculatePositionBounds()
    {
        Bounds canvasBounds = canvasSprite.bounds;
        Vector2 cameraHalfSize = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);

        minPosition.x = canvasBounds.min.x - canvasMargin + cameraHalfSize.x;
        maxPosition.x = canvasBounds.max.x + canvasMargin - cameraHalfSize.x;
        minPosition.y = canvasBounds.min.y - canvasMargin + cameraHalfSize.y;
        maxPosition.y = canvasBounds.max.y + canvasMargin - cameraHalfSize.y;
    }

    private void CalculateZoomBounds()
    {
        Bounds canvasBounds = canvasSprite.bounds;

        float maxZoomHorizontal = (canvasBounds.size.x / 2 + canvasMargin) / camera.aspect;
        float maxZoomVertical = canvasBounds.size.y / 2 + canvasMargin;

        minZoom = 2;
        maxZoom = Mathf.Min(maxZoomVertical, maxZoomHorizontal);
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

        prevZoom = camera.orthographicSize;
        prevCameraAspect = camera.aspect;
    }

    private void Update()
    {
        // TODO: change zoom speed depending on current zoom (faster the more zoomed out you are)
        // TODO: test having zoom bounds be based on Mathf.Max instead
        // Change current zoom
        if (prevCameraAspect != camera.aspect)
            CalculateZoomBounds();
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.mouseScrollDelta.y, minZoom, maxZoom);

        // TODO: test having bounds be directly on the corners of the canvas
        // Change current position
        if (prevZoom != camera.orthographicSize || prevCameraAspect != camera.aspect)
            CalculatePositionBounds();

        if (Input.GetMouseButtonDown(2))
            originalMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        else if (Input.GetMouseButton(2))
            currentPosition += originalMousePosition - (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);

        currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
        transform.position = currentPosition;

        // Prepare re-calculation
        prevZoom = camera.orthographicSize;
        prevCameraAspect = camera.aspect;
    }
}
