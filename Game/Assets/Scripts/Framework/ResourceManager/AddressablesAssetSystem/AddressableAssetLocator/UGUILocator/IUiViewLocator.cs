using System;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Views;

public interface IUiViewLocator
{
    void LoadViewAsync(Action<IView> action, string uiKey = "");

    void LoadViewAsync<T>(Action<T> action, string uiKey = "") where T : IView;

    void LoadWindowAsync(Action<Window> action, string uiKey = "");

    void LoadWindowAsync<T>(Action<T> action, string uiKey = "") where T : Window;

    void LoadWindowAsync(IWindowManager windowManager, Action<Window> action, string uiKey = "");

    void LoadWindowAsync<T>(IWindowManager windowManager, Action<T> action, string uiKey = "") where T : Window;

    IProgressTask<float, IView> LoadViewAsync(string uiKey = "");

    IProgressTask<float, T> LoadViewAsync<T>(string uiKey = "") where T : IView;

    IProgressTask<float, Window> LoadWindowAsync(string uiKey = "");

    IProgressTask<float, T> LoadWindowAsync<T>(string uiKey = "") where T : Window;

    IProgressTask<float, Window> LoadWindowAsync(IWindowManager windowManager, string uiKey = "");

    IProgressTask<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string uiKey = "") where T : Window;
}