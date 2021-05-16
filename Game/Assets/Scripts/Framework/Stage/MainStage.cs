using System;
using Loxodon.Framework.Contexts;

public class MainStage : StageBase
{
    public override StageEnum StageEnum => StageEnum.Main;

    public override void BeforeTransition()
    {
    }

    public override void OnLoadSceneComplete(Action action = null)
    {
        try
        {
            var stageType = Context.GetApplicationContext().Get<StageType>(nameof(StageType));
            GameObjectLocator.InstantiateAsync(stageType.ToDescription(), resource =>
            {
                resource.SetActive(true);
                Resources.Add(resource);
                base.OnLoadSceneComplete(action);
            });
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 场景加载完毕
    /// </summary>
    public override void OnCompleteAll()
    {
        base.OnCompleteAll();

        var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
        var loginLauncher = UnityEngine.Object.FindObjectOfType<SetUpManager>();
        globalConfigurator.CameraRig.Set3DCameraTransform(loginLauncher.BornPoint.transform, loginLauncher.LookAtPoint.transform);
    }

    /// <summary>
    /// 销毁场景
    /// </summary>
    public override void OnDispose()
    {
        var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();

        globalConfigurator.GlobalWindowManager.Clear();
        GameObjectLocator.ReleaseAll();
        base.OnDispose();
    }
}