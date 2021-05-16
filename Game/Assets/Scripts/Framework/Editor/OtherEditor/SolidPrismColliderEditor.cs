using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 实心棱柱编辑器
/// </summary>
[CustomEditor(typeof(SolidPrismCollider))]
public class SolidPrismColliderEditor : Editor
{
    private SolidPrismCollider m_Collider;

    protected virtual void OnEnable()
    {
        m_Collider = target as SolidPrismCollider;
        if (m_Collider == null)
        {
            throw new ArgumentNullException(nameof(m_Collider));
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear Children"))
        {
            if (m_Collider != null)
            {
                m_Collider.ClearChildren();
            }
        }

        if (GUILayout.Button("Clear Collider"))
        {
            if (m_Collider != null)
            {
                m_Collider.ClearCollides();
            }
        }

        if (GUILayout.Button("Bake Collider"))
        {
            if (m_Collider != null)
            {
                m_Collider.CreateCollides();
            }
        }

        GUILayout.EndHorizontal();
    }
}