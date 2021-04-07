using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    [SerializeField] private Text currentToolText;

    private Tool currentTool;

    public Tool GetCurrentTool()
    {
        return currentTool;
    }

    public void SetEraserTool()
    {
        currentTool = new EraserTool();
        currentToolText.text = "Current Tool\n" + currentTool.GetName();
    }

    public void SetBuildingTool(BuildingInfo info)
    {
        currentTool = new BuildingTool(info);
        currentToolText.text = "Current Tool\n" + currentTool.GetName();
    }

    //////////////////
    // Unity Functions

    private void Awake()
    {
        SetEraserTool();
    }
}
