using System.ComponentModel;

public static class GlobalFlagDepository
{
    /// <summary>
    /// 用户最近选择的场景
    /// </summary>
    public static SceneType CurSceneType;
}

public enum SceneType
{
    [Description("Processed Library")]
    Library,
    [Description("Processed LivingRoom")]
    Kitchen,
    [Description("Processed Gym")]
    Gym,
}
