using System;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Services;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class BaseLauncher : MonoBehaviour, ILauncher
{
    protected ApplicationContext ApplicationContext;
    protected IServiceContainer ServiceContainer;

    public virtual void Launch(Action<bool> action)
    {
    }

    protected virtual void Awake()
    {
        ApplicationContext = Context.GetApplicationContext();
        ServiceContainer = ApplicationContext.GetContainer();
    }
}