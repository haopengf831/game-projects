using UnityEditor;
using UnityEngine;

public class CanvasContextMenuEditor : Editor
{
    [MenuItem("CONTEXT/Canvas/AddUiBoxCollider")]
    public static void AddUiBoxCollider(MenuCommand command)
    {
        var canvas = command.context as Canvas;
        var boxCollider = canvas.RequireComponent<BoxCollider>();
        var rect = canvas.RequireComponent<RectTransform>().rect;
        boxCollider.size = new Vector2(rect.width, rect.height);
    }
}