using System;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Views;

public abstract class UiViewLocatorBase : AssetLocator, IUiViewLocator
{
    public virtual void LoadViewAsync(Action<IView> action, string uiKey = "")
    {
        LoadViewAsync<IView>(iView =>
        {
            try
            {
                action?.Invoke(iView);
            }
            catch (Exception e)
            {
                throw;
            }
        }, uiKey);
    }

    public abstract void LoadViewAsync<T>(Action<T> action, string uiKey = "") where T : IView;

    public virtual void LoadWindowAsync(Action<Window> action, string uiKey = "")
    {
        LoadWindowAsync<Window>(window =>
        {
            try
            {
                action?.Invoke(window);
            }
            catch (Exception e)
            {
                throw;
            }
        }, uiKey);
    }

    public abstract void LoadWindowAsync<T>(Action<T> action, string uiKey = "") where T : Window;

    public virtual void LoadWindowAsync(IWindowManager windowManager, Action<Window> action, string uiKey = "")
    {
        LoadWindowAsync<Window>(windowManager, window =>
        {
            try
            {
                action?.Invoke(window);
            }
            catch (Exception e)
            {
                throw;
            }
        }, uiKey);
    }

    public abstract void LoadWindowAsync<T>(IWindowManager windowManager, Action<T> action, string uiKey = "")
        where T : Window;

    public virtual IProgressTask<float, IView> LoadViewAsync(string uiKey = "")
    {
        return LoadViewAsync<IView>(uiKey);
    }

    public abstract IProgressTask<float, T> LoadViewAsync<T>(string uiKey = "") where T : IView;

    public virtual IProgressTask<float, Window> LoadWindowAsync(string uiKey = "")
    {
        return LoadWindowAsync<Window>(uiKey);
    }

    public abstract IProgressTask<float, T> LoadWindowAsync<T>(string uiKey = "") where T : Window;

    public virtual IProgressTask<float, Window> LoadWindowAsync(IWindowManager windowManager, string uiKey = "")
    {
        return LoadWindowAsync<Window>(windowManager, uiKey);
    }

    public abstract IProgressTask<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string uiKey = "")
        where T : Window;
}