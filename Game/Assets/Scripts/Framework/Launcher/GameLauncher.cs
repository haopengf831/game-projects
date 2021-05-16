using Loxodon.Framework.Binding;

public class GameLauncher : BaseLauncher
{
    public GlobalConfigurator GlobalConfigurator;
    public GlobalSetting GlobalSetting;
    public GlobalEvent GlobalEvent;

    protected override void Awake()
    {
        base.Awake();
        CommonLog.Initialize(GlobalSetting.LogLevel);
        RegisterServices();
    }

    private void Start()
    {
        ApplicationContext.GetService<StageManager>().ChangeScene<InitStage>();
    }

    /// <summary>
    /// 注册全局应用服务
    /// </summary>
    private void RegisterServices()
    {
        ServiceContainer.Register(GlobalConfigurator);
        ServiceContainer.Register(GlobalEvent);
        ServiceContainer.Register(GlobalSetting);
        ServiceContainer.Register(new StageManager());
        ServiceContainer.Register<IUiViewLocator>(new UiViewLocator(GlobalConfigurator.GlobalWindowManager));
        new BindingServiceBundle(ServiceContainer).Start();
    }
}