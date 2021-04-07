using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolMenu : MonoBehaviour
{
    [SerializeField] private float openingDuration = 0.5f;
    private float openingT;

    private bool isOpen = false;

    public void ToggleOpen()
    {
        isOpen = !isOpen;
    }

    //////////////////
    // Unity Functions

    private void Update()
    {
        openingT += (isOpen) ? Time.deltaTime / openingDuration : -Time.deltaTime / openingDuration;
        openingT = Mathf.Clamp(openingT, 0, 1);

        transform.localScale = new Vector3(Mathf.SmoothStep(0, 1, openingT), 1, 1);
    }
}
