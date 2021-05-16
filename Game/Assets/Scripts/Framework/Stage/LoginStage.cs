using System;
using Loxodon.Framework.Contexts;

/// <summary>
/// 登陆场景
/// </summary>
public class LoginStage : StageBase
{
    public override StageEnum StageEnum => StageEnum.Login;

    public override void BeforeTransition()
    {
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="action"></param>
    public override void OnLoadSceneComplete(Action action = null)
    {
        try
        {
            var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
            globalConfigurator.LoginView.SetActive(true);
            base.OnLoadSceneComplete(action);
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


    }

    /// <summary>
    /// 销毁场景
    /// </summary>
    public override void OnDispose()
    {
        var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
        globalConfigurator.LoginView.SetActive(false);
        globalConfigurator.GlobalWindowManager.Clear();
        GameObjectLocator.ReleaseAll(); //销毁所有加载的GameObject

        base.OnDispose();
    }
}