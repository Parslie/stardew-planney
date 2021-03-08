using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolSelector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentToolText;

    private Tool currentTool;

    public Tool GetCurrentTool()
    {
        return currentTool;
    }

    public void SetEraserTool()
    {
        currentTool = new EraserTool();
        currentToolText.text = "Current Tool: " + currentTool.GetName();
    }

    public void SetBuildingTool(BuildingInfo info)
    {
        currentTool = new BuildingTool(info);
        currentToolText.text = "Current Tool: " + currentTool.GetName();
    }

    //////////////////
    // Unity Functions

    private void Awake()
    {
        SetEraserTool();
    }
}
