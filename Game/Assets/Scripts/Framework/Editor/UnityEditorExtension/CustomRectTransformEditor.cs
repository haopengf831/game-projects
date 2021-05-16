using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectTransform))]
public class CustomRectTransformEditor : DecoratorEditor
{
    public CustomRectTransformEditor() : base("RectTransformEditor")
    {
    }

    private RectTransform m_RectTransform;
    private bool m_IsCustomRectTransformFoldOut = false;
    private bool m_IsBasicAttributeFoldOut = false;
    private bool m_IsRectAttributeFoldOut = false;
    private bool m_IsOffsetFoldOut = false;
    private bool m_IsRectFoldOut = false;
    private bool m_IsRectCornerFoldOut = false;
    private bool m_IsRectCoordinateFoldOut = false;
    private bool m_IsDirectionAttributeFoldOut = false;
    private bool m_IsOtherAttributeFoldOut = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_RectTransform = target as RectTransform;
        if (m_RectTransform == null)
        {
            throw new ArgumentNullException(nameof(m_RectTransform));
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (m_RectTransform == null)
        {
            throw new ArgumentNullException(nameof(m_RectTransform));
        }

        EditorGUILayout.Space();
        m_IsCustomRectTransformFoldOut = EditorGUILayout.Foldout(m_IsCustomRectTransformFoldOut, "Custom RectTransform");
        if (!m_IsCustomRectTransformFoldOut)
        {
            return;
        }

        EditorGUILayout.Space();
        EditorGUI.indentLevel++;
        m_IsBasicAttributeFoldOut = EditorGUILayout.Foldout(m_IsBasicAttributeFoldOut, "Basic");
        if (m_IsBasicAttributeFoldOut)
        {
            EditorGUILayout.Vector3Field("LocalPosition", m_RectTransform.localPosition);
            EditorGUILayout.Vector3Field("WorldPosition", m_RectTransform.position);
            EditorGUILayout.Space();
            EditorGUILayout.Vector3Field("LocalEulerAngles", m_RectTransform.localEulerAngles);
            EditorGUILayout.Vector3Field("WorldEulerAngles", m_RectTransform.eulerAngles);
            EditorGUILayout.Space();
            EditorGUILayout.Vector3Field("LocalScale", m_RectTransform.localScale);
            EditorGUILayout.Vector3Field("WorldScale", m_RectTransform.lossyScale);
        }

        EditorGUI.indentLevel--;

        EditorGUILayout.Space();
        EditorGUI.indentLevel++;
        m_IsRectAttributeFoldOut = EditorGUILayout.Foldout(m_IsRectAttributeFoldOut, "RectTransform");
        if (m_IsRectAttributeFoldOut)
        {
            EditorGUILayout.Vector2Field("AnchoredPosition", m_RectTransform.anchoredPosition);
            EditorGUILayout.Vector3Field("AnchoredPosition3D", m_RectTransform.anchoredPosition3D);
            EditorGUILayout.Space();
            EditorGUILayout.Vector2Field("SizeDelta", m_RectTransform.sizeDelta);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            m_IsOffsetFoldOut = EditorGUILayout.Foldout(m_IsOffsetFoldOut, "Offset");
            if (m_IsOffsetFoldOut)
            {
                EditorGUILayout.Vector2Field("Min", m_RectTransform.offsetMin);
                EditorGUILayout.Vector2Field("Max", m_RectTransform.offsetMax);
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            m_IsRectFoldOut = EditorGUILayout.Foldout(m_IsRectFoldOut, "Rect");
            if (m_IsRectFoldOut)
            {
                var rect = m_RectTransform.rect;
                EditorGUILayout.Vector2Field("Center", rect.center);
                EditorGUILayout.Vector2Field("Size", rect.size);
                EditorGUILayout.Vector2Field("Position", rect.position);
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                m_IsRectCornerFoldOut = EditorGUILayout.Foldout(m_IsRectCornerFoldOut, "Corner");
                if (m_IsRectCornerFoldOut)
                {
                    EditorGUILayout.Vector2Field("Min", rect.min);
                    EditorGUILayout.Vector2Field("Max", rect.max);
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                m_IsRectCoordinateFoldOut = EditorGUILayout.Foldout(m_IsRectCoordinateFoldOut, "Coordinate");
                if (m_IsRectCoordinateFoldOut)
                {
                    EditorGUILayout.Vector2Field("Now", new Vector2(rect.x, rect.y));
                    EditorGUILayout.Vector2Field("Min", new Vector2(rect.xMin, rect.yMin));
                    EditorGUILayout.Vector2Field("Max", new Vector2(rect.xMax, rect.yMax));
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel--;

        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        m_IsDirectionAttributeFoldOut = EditorGUILayout.Foldout(m_IsDirectionAttributeFoldOut, "Direction");
        if (m_IsDirectionAttributeFoldOut)
        {
            EditorGUILayout.Vector3Field("Up", m_RectTransform.up);
            EditorGUILayout.Vector3Field("Down", m_RectTransform.Down());
            EditorGUILayout.Vector3Field("Left", m_RectTransform.Left());
            EditorGUILayout.Vector3Field("Right", m_RectTransform.right);
            EditorGUILayout.Vector3Field("Forward", m_RectTransform.forward);
            EditorGUILayout.Vector3Field("Back", m_RectTransform.Back());
        }

        EditorGUI.indentLevel--;

        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        m_IsOtherAttributeFoldOut = EditorGUILayout.Foldout(m_IsOtherAttributeFoldOut, "Other");
        if (m_IsOtherAttributeFoldOut)
        {
            EditorGUILayout.ObjectField("Root", m_RectTransform.root, typeof(Transform), true);
        }

        EditorGUI.indentLevel--;
    }
}