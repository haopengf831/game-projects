using Loxodon.Framework.Services;

namespace Runtime.Launcher
{
    public class LauncherServiceBundle : AbstractServiceBundle
    {
        public LauncherServiceBundle(IServiceContainer container) : base(container)
        {
        }

        protected override void OnStart(IServiceContainer container)
        {
//            OculusLauncher oculusLauncher = new OculusLauncher();
//            oculusLauncher.Launch();
//            VRLauncher vrLauncher = new VRLauncher();
//            vrLauncher.Launch();
//
//            container.Register(oculusLauncher);
//            container.Register(vrLauncher);
        }

        protected override void OnStop(IServiceContainer container)
        {
            container.Unregister<ILauncher>();
        }
    }
}