public enum StageEventEnum
{
    None = 0,
    /// <summary>
    /// 开始切换场景
    /// </summary>
    StartChangeScene,
    /// <summary>
    /// 开始加载资源
    /// </summary>
    StartLoadResources,
    /// <summary>
    /// 加载资源结束
    /// </summary>
    CompleteLoadResources,
    /// <summary>
    /// 开始过渡效果
    /// </summary>
    StartTransitionEffectFadeIn,
    /// <summary>
    /// 过渡效果结束
    /// </summary>
    CompleteTransitionEffectFadeIn,
    /// <summary>
    /// 加载资源或过渡效果结束
    /// </summary>
    CompleteLoadResourcesOrTransitionEffectIn,
    /// <summary>
    /// 开始加载场景
    /// </summary>
    StartLoadScene,
    /// <summary>
    /// 结束加载场景
    /// </summary>
    CompleteLoadScene,
    /// <summary>
    /// 开始过渡效果
    /// </summary>
    StartTransitionEffectFadeOut,
    /// <summary>
    /// 过渡效果结束
    /// </summary>
    CompleteTransitionEffectFadeOut,
    /// <summary>
    /// 读表结束
    /// </summary>
    CompleteReadTable,
}