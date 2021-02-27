using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CanvasViewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer canvasSprite;
    [SerializeField] private int canvasMargin;

    // Zoom is half of viewport height
    private float minZoom = 2;
    private float maxZoom;
    private float currentZoom;

    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        Bounds canvasBounds = canvasSprite.bounds;

        // Set default position
        transform.position = canvasBounds.center;

        // Set default zoom
        maxZoom = canvasBounds.size.y / 2 + canvasMargin;
        currentZoom = maxZoom;
    }

    private void Update()
    {
        float deltaZoom = -Input.mouseScrollDelta.y;
        currentZoom = Mathf.Clamp(currentZoom + deltaZoom, minZoom, maxZoom);

        camera.orthographicSize = currentZoom;
    }
}
