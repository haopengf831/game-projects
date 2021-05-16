using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using System;

/// <summary>
/// Unity 的扩展方法
/// </summary>
public static partial class UnityExtension
{
    /// <summary>
    /// 获取应用上下文中的服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetApplicationService<T>()
    {
        return Context.GetApplicationContext().GetService<T>();
    }
    public static T GetService<C, T>() where C : Context
    {
        var contextName = typeof(C).FullName;
        var context = Context.GetContext<C>(contextName);
        return context.GetService<T>();
    }

    /// <summary>
    /// 获取Window
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetWindow<T>() where T : IWindow
    {
        var globalWindow = GetApplicationService<GlobalConfigurator>().GlobalWindowManager;
        return globalWindow.Find<T>();
    }

    /// <summary>
    /// 创建窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public static void CreateWindow<T>() where T : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        locator.LoadWindowAsync<T>().Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T window = result.Result;
                    window.Create();
                }
            });
    }
    public static void CreateWindow<T>(Action action = null) where T : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        locator.LoadWindowAsync<T>().Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T window = result.Result;
                    window.Create();
                    action?.Invoke();
                    action = null;
                }
            });
    }
    public static void CreateWindow<T>(Action<T> action = null) where T : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        locator.LoadWindowAsync<T>().Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T window = result.Result;
                    window.Create();
                    action?.Invoke(window);
                    action = null;
                }
            });
    }

    /// <summary>
    /// 创建视图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public static void CreateView<T, W>(W window, Action action = null) where T : UIView where W : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        locator.LoadViewAsync<T>().Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T view = result.Result;
                    window.AddView(view);
                    action?.Invoke();
                    action = null;
                }
                else
                {
                    CommonLog.LogError(result.Exception);
                }
            });
    }
    public static void CreateView<T, W>(W window, Action<T> action = null) where T : UIView where W : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        CommonLog.LogError(nameof(T));
        locator.LoadViewAsync<T>().Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T view = result.Result;
                    window.AddView(view);
                    action?.Invoke(view);
                    action = null;
                }
                else
                {
                    CommonLog.LogError(result.Exception);
                }
            });
    }
    public static void CreateView<T, W>(W window, string name, Action<T> action = null) where T : UIView where W : Window
    {
        IUiViewLocator locator = GetApplicationService<IUiViewLocator>();
        locator.LoadViewAsync(name).Callbackable().OnCallback(
            result =>
            {
                if (result.Exception == null)
                {
                    T view = result.Result as T;
                    window.AddView(view);
                    action?.Invoke(view);
                    action = null;
                }
                else
                {
                    CommonLog.LogError(result.Exception);
                }
            });
    }

}