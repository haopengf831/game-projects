using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform))]
public class CustomTransformInspector : DecoratorEditor
{
    public CustomTransformInspector() : base("TransformInspector")
    {
    }

    private Transform m_Transform;
    private bool m_IsCustomTransformFoldOut = false;
    private bool m_IsBasicAttributeFoldOut = false;
    private bool m_IsDirectionAttributeFoldOut = false;
    private bool m_IsOtherAttributeFoldOut = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_Transform = target as Transform;
        if (m_Transform == null)
        {
            throw new ArgumentNullException(nameof(m_Transform));
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (m_Transform == null)
        {
            throw new ArgumentNullException(nameof(m_Transform));
        }

        EditorGUILayout.Space();
        m_IsCustomTransformFoldOut = EditorGUILayout.Foldout(m_IsCustomTransformFoldOut, "Custom Transform");
        if (!m_IsCustomTransformFoldOut)
        {
            return;
        }

        EditorGUILayout.Space();
        EditorGUI.indentLevel++;
        m_IsBasicAttributeFoldOut = EditorGUILayout.Foldout(m_IsBasicAttributeFoldOut, "Basic");
        if (m_IsBasicAttributeFoldOut)
        {
            EditorGUILayout.Vector3Field("LocalPosition", m_Transform.localPosition);
            EditorGUILayout.Vector3Field("WorldPosition", m_Transform.position);
            EditorGUILayout.Space();
            EditorGUILayout.Vector3Field("LocalEulerAngles", m_Transform.localEulerAngles);
            EditorGUILayout.Vector3Field("WorldEulerAngles", m_Transform.eulerAngles);
            EditorGUILayout.Space();
            EditorGUILayout.Vector3Field("LocalScale", m_Transform.localScale);
            EditorGUILayout.Vector3Field("WorldScale", m_Transform.lossyScale);
        }

        EditorGUI.indentLevel--;

        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        m_IsDirectionAttributeFoldOut = EditorGUILayout.Foldout(m_IsDirectionAttributeFoldOut, "Direction");
        if (m_IsDirectionAttributeFoldOut)
        {
            EditorGUILayout.Vector3Field("Up", m_Transform.up);
            EditorGUILayout.Vector3Field("Down", m_Transform.Down());
            EditorGUILayout.Vector3Field("Left", m_Transform.Left());
            EditorGUILayout.Vector3Field("Right", m_Transform.right);
            EditorGUILayout.Vector3Field("Forward", m_Transform.forward);
            EditorGUILayout.Vector3Field("Back", m_Transform.Back());
        }

        EditorGUI.indentLevel--;

        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        m_IsOtherAttributeFoldOut = EditorGUILayout.Foldout(m_IsOtherAttributeFoldOut, "Other");
        if (m_IsOtherAttributeFoldOut)
        {
            EditorGUILayout.ObjectField("Root", m_Transform.root, typeof(Transform), true);
        }

        EditorGUI.indentLevel--;
    }

    protected override void CallInspectorMethod(MethodInfo method)
    {
        if (m_Transform != null && m_Transform.ContainsComponent<RectTransform>())
        {
            base.CallInspectorMethod(method);
        }
    }
}