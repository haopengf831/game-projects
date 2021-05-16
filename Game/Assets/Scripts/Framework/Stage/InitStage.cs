using Loxodon.Framework.Contexts;
using Loxodon.Framework.Messaging;

public class InitStage : StageBase
{
    public override StageEnum StageEnum => StageEnum.Init;

    public override void OnGetPreLoadResources()
    {
        
    }

    public override void OnCompleteAll()
    {
        Context.GetApplicationContext().GetService<StageManager>().ChangeScene<LoginStage>();
    }
}