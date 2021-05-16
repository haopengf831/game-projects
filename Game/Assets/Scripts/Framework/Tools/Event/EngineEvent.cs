using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class EngineEvent : MonoBehaviour
{
    [SerializeField] protected UnityEvent FirstPause = new UnityEvent(); //程序第一次暂停
    [SerializeField] protected UnityEvent Pause = new UnityEvent(); //程序暂停
    [SerializeField] protected UnityEvent FirstResume = new UnityEvent(); //程序第一次恢复运行
    [SerializeField] protected UnityEvent Resume = new UnityEvent(); //程序恢复运行
    [SerializeField] protected UnityEvent FirstFocus = new UnityEvent(); //程序第一次聚焦
    [SerializeField] protected UnityEvent Focus = new UnityEvent(); //程序聚焦
    [SerializeField] protected UnityEvent FirstLoseFocus = new UnityEvent(); //程序第一次丢失焦点
    [SerializeField] protected UnityEvent LoseFocus = new UnityEvent(); //程序丢失焦点

    protected bool IsHasPause = false;
    protected bool IsHasResume = false;
    protected bool IsHasFocus = false;
    protected bool IsHasLoseFocus = false;

    #region Mono

    protected virtual void OnApplicationPause(bool pauseStatus)
    {
        try
        {
            if (pauseStatus)
            {
                if (!IsHasPause)
                {
                    FirstPause?.Invoke();
                    IsHasPause = true;
                }

                Pause?.Invoke();
            }
            else
            {
                if (!IsHasResume)
                {
                    FirstResume?.Invoke();
                    IsHasResume = true;
                }

                Resume?.Invoke();
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    protected virtual void OnApplicationFocus(bool hasFocus)
    {
        try
        {
            if (hasFocus)
            {
                if (!IsHasFocus)
                {
                    FirstFocus?.Invoke();
                    IsHasFocus = true;
                }

                Focus?.Invoke();
            }
            else
            {
                if (!IsHasLoseFocus)
                {
                    FirstLoseFocus?.Invoke();
                    IsHasLoseFocus = true;
                }

                LoseFocus?.Invoke();
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    #endregion

    #region Public

    public virtual void Dispose()
    {
        try
        {
            RemoveAllListeners();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    #endregion

    #region Add Listener

    /// <summary>
    /// 监听程序第一次暂停的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddFirstPauseListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstPause?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序暂停的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddPauseListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            Pause?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序第一次恢复运行的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddFirstResumeListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstResume?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序恢复运行的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddResumeListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            Resume?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序第一次聚焦的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddFirstFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstFocus?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序聚焦的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            Focus?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序第一次失去焦点的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddFirstLossFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstLoseFocus?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 监听程序失去焦点的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddLoseFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            LoseFocus?.AddListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    #endregion

    #region Remove Listener

    /// <summary>
    /// 移除所有监听
    /// </summary>
    protected virtual void RemoveAllListeners()
    {
        FirstPause?.RemoveAllListeners();
        Pause?.RemoveAllListeners();
        FirstResume?.RemoveAllListeners();
        Resume?.RemoveAllListeners();
        FirstFocus?.RemoveAllListeners();
        Focus?.RemoveAllListeners();
        FirstLoseFocus?.RemoveAllListeners();
        LoseFocus?.RemoveAllListeners();
    }

    /// <summary>
    /// 取消监听程序第一次暂停的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveFirstPauseListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstPause?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序暂停的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemovePauseListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstPause?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序第一次恢复运行的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveFirstResumeListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstResume?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序恢复运行的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveResumeListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            Resume?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序第一次聚焦的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveFirstFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstFocus?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序聚焦的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            Focus?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序第一次失去焦点的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveFirstLossFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            FirstLoseFocus?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 取消监听程序失去焦点的事件
    /// </summary>
    /// <param name="action"></param>
    public virtual void RemoveLoseFocusListener(UnityAction action)
    {
        try
        {
            if (action == null)
            {
                return;
            }

            LoseFocus?.RemoveListener(action);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    #endregion
}