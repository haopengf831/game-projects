using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 空心棱柱编辑器
/// </summary>
[CustomEditor(typeof(HollowPrismCollider))]
public class HollowPrismColliderEditor : Editor
{
    private HollowPrismCollider m_Collider;

    protected virtual void OnEnable()
    {
        m_Collider = target as HollowPrismCollider;
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