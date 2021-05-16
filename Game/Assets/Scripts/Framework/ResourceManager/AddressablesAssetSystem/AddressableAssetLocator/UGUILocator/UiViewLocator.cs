using System;
using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Views;
using UnityEngine;
using Object = UnityEngine.Object;

public class UiViewLocator : UiViewLocatorBase
{
    private GlobalWindowManager m_GlobalWindowManager;
    private readonly Dictionary<string, WeakReference> m_Templates = new Dictionary<string, WeakReference>();

    public UiViewLocator()
    {
    }

    public UiViewLocator(GlobalWindowManager globalWindowManager)
    {
        if (globalWindowManager != null)
        {
            m_GlobalWindowManager = globalWindowManager;
        }
    }

    public virtual IWindowManager GetDefaultWindowManager()
    {
        if (m_GlobalWindowManager != null)
        {
            return m_GlobalWindowManager;
        }

        m_GlobalWindowManager = Object.FindObjectOfType<GlobalWindowManager>();
        if (m_GlobalWindowManager == null)
        {
            throw new NotFoundException("GlobalWindowManager");
        }

        return m_GlobalWindowManager;
    }

    public virtual GlobalWindowManager GetGlobalWindowManager()
    {
        if (m_GlobalWindowManager != null)
        {
            return m_GlobalWindowManager;
        }

        m_GlobalWindowManager = Object.FindObjectOfType<GlobalWindowManager>();
        if (m_GlobalWindowManager == null)
        {
            throw new NotFoundException("GlobalWindowManager");
        }

        return m_GlobalWindowManager;
    }

    protected virtual string Normalize<T>(string uiKey)
    {
        if (string.IsNullOrWhiteSpace(uiKey))
        {
            uiKey = typeof(T).FullName;
        }

        return uiKey;
    }

    protected virtual void DoLoadViewAsync<T>(Action<T> action, string uiKey)
    {
        uiKey = Normalize<T>(uiKey);
        GameObject viewTemplateGo = null;
        try
        {
            try
            {
                if (m_Templates.TryGetValue(uiKey, out var weakReference) && weakReference.IsAlive)
                {
                    viewTemplateGo = (GameObject) weakReference.Target;

                    //Check if the object is valid because it may have been destroyed.
                    //Unmanaged objects,the weak caches do not accurately track the validity of objects.
                    if (viewTemplateGo != null)
                    {
                        string goName = viewTemplateGo.name;
                    }
                }
            }
            catch
            {
                viewTemplateGo = null;
            }

            if (viewTemplateGo == null)
            {
                LoadAssetAsyncByCoroutine(uiKey, (GameObject result) =>
                {
                    if (result != null)
                    {
                        viewTemplateGo = result;
                        viewTemplateGo.SetActive(false);
                        m_Templates[uiKey] = new WeakReference(viewTemplateGo);

                        var goAsync = Object.Instantiate(result);
                        goAsync.name = result.name;
                        var viewAsync = goAsync.GetComponent<T>();
                        if (viewAsync == null && goAsync != null)
                        {
                            ReleaseInstance(goAsync);
                            ReleaseAsset(viewTemplateGo);
                            throw Exception;
                        }

                        action?.Invoke(viewAsync);
                    }
                });

                return;
            }

            if (viewTemplateGo == null || viewTemplateGo.GetComponent<T>() == null)
            {
                action?.Invoke(default);
                return;
            }

            var go = Object.Instantiate(viewTemplateGo);
            go.name = viewTemplateGo.name;
            var view = go.GetComponent<T>();
            if (view == null && go != null)
            {
                ReleaseInstance(go);
                ReleaseAsset(viewTemplateGo);
                throw Exception;
            }

            action?.Invoke(view);
        }
        catch (Exception)
        {
            viewTemplateGo = null;
        }
    }

    public override void LoadViewAsync<T>(Action<T> action, string uiKey = "")
    {
        DoLoadViewAsync(action, uiKey);
    }

    public override void LoadWindowAsync<T>(Action<T> action, string uiKey = "")
    {
        LoadWindowAsync(null, action, uiKey);
    }

    public override void LoadWindowAsync<T>(IWindowManager windowManager, Action<T> action, string uiKey = "")
    {
        if (windowManager == null)
        {
            windowManager = GetDefaultWindowManager();
        }

        LoadViewAsync((T target) =>
        {
            if (target != null)
            {
                target.WindowManager = windowManager;
            }

            action?.Invoke(target);
        }, uiKey);
    }

    protected virtual IEnumerator ProgressLoad<T>(IProgressPromise<float, T> promise, string uiKey)
    {
        uiKey = Normalize<T>(uiKey);
        GameObject viewTemplateGo = null;
        try
        {
            if (m_Templates.TryGetValue(uiKey, out var weakReference) && weakReference.IsAlive)
            {
                viewTemplateGo = weakReference.Target as GameObject;

                //Check if the object is valid because it may have been destroyed.
                //Unmanaged objects,the weak caches do not accurately track the validity of objects.
                if (viewTemplateGo != null)
                {
                    string goName = viewTemplateGo.name;
                }
            }
        }
        catch (Exception)
        {
            viewTemplateGo = null;
        }

        if (viewTemplateGo == null)
        {
            var handle = LoadAssetAsyncOperationHandle<GameObject>(uiKey);
            while (!handle.IsDone)
            {
                promise.UpdateProgress(handle.PercentComplete);
                yield return null;
            }

            viewTemplateGo = handle.Result;
            if (viewTemplateGo != null)
            {
                viewTemplateGo.SetActive(false);
                m_Templates[uiKey] = new WeakReference(viewTemplateGo);
            }
        }

        if (viewTemplateGo == null || viewTemplateGo.GetComponent<T>() == null)
        {
            promise.UpdateProgress(1f);
            promise.SetException(new NotFoundException(uiKey));
            yield break;
        }

        GameObject go = Object.Instantiate(viewTemplateGo);
        go.name = viewTemplateGo.name;
        T view = go.GetComponent<T>();
        if (view == null)
        {
            ReleaseInstance(go);
            ReleaseAsset(viewTemplateGo);
            promise.SetException(new NotFoundException(uiKey));
        }
        else
        {
            promise.UpdateProgress(1f);
            promise.SetResult(view);
        }
    }

    public override IProgressTask<float, T> LoadViewAsync<T>(string uiKey = "")
    {
        var task = new ProgressTask<float, T>(p => ProgressLoad(p, uiKey));
        return task.Start(30);
    }

    public override IProgressTask<float, T> LoadWindowAsync<T>(string uiKey = "")
    {
        return LoadWindowAsync<T>(GetDefaultWindowManager(), uiKey);
    }

    public override IProgressTask<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string uiKey = "")
    {
        if (windowManager == null)
        {
            windowManager = GetDefaultWindowManager();
        }

        var task = new ProgressTask<float, T>(p => ProgressLoad(p, uiKey));
        return task.Start(30).OnPostExecute(win => { win.WindowManager = windowManager; });
    }
}