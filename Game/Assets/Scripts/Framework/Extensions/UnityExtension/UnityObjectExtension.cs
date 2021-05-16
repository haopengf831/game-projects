using UnityEngine;

/// <summary>
/// Unity Object的扩展方法
/// </summary>
public static class UnityObjectExtension
{
    /// <summary>
    /// 加载场景时不销毁传入的Object(当编辑器不运行时，启用Unity对象跳过“不运行载入”，以便测试运行程序通过)
    /// </summary>
    public static void DontDestroyOnLoad(this Object target)
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
#endif
            Object.DontDestroyOnLoad(target);
    }
}