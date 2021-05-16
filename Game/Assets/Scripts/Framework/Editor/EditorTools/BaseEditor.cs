using UnityEditor;
using UnityEngine;

public abstract class BaseEditor : Editor
{
    private Object m_MonoScript;

    protected virtual void OnEnable()
    {
        this.m_MonoScript = MonoScript.FromMonoBehaviour(this.target as MonoBehaviour);
    }

    protected virtual void DrawMonoScript()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", this.m_MonoScript, typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();
    }

    public override void OnInspectorGUI()
    {
        this.DrawMonoScript();
    }
}