using System;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Services;

public class TableServiceBundle : AbstractServiceBundle
{
    private IDisposable m_ISubscription;

    public TableServiceBundle(IServiceContainer container) : base(container)
    {
    }

    protected override void OnStart(IServiceContainer container)
    {
        MainroomTableData MainroomTableData = new MainroomTableData();

        container.Register(MainroomTableData);

//        m_ISubscription = Messenger.Default.Subscribe<LoginFinishType>(async result =>
//        {
//            if (result == LoginFinishType.FINISH)
//            {
//                await TableHelper.ReadTable();
//                m_ISubscription.Dispose();
//                m_ISubscription = null;
//            }
//        });
    }

    protected override void OnStop(IServiceContainer container)
    {
        container.Unregister<MainroomTableData>();
    }
}