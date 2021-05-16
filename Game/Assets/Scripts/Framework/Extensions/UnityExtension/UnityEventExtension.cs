using UnityEngine.Events;

/// <summary>
/// Unity UnityEvent类的扩展方法
/// </summary>
public static partial class UnityEventExtension
{
    #region ReAddListener

    /// <summary>
    /// 重新添加监听(移除并添加监听)
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void ReAddListener(this UnityEvent unityEvent, UnityAction call)
    {
        unityEvent?.RemoveListener(call);
        unityEvent?.AddListener(call);
    }

    /// <summary>
    /// 重新添加监听(移除并添加监听)
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    /// <typeparam name="T0"></typeparam>
    public static void ReAddListener<T0>(this UnityEvent<T0> unityEvent, UnityAction<T0> call)
    {
        unityEvent?.RemoveListener(call);
        unityEvent?.AddListener(call);
    }

    /// <summary>
    /// 重新添加监听(移除并添加监听)
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public static void ReAddListener<T0, T1>(this UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> call)
    {
        unityEvent?.RemoveListener(call);
        unityEvent?.AddListener(call);
    }

    /// <summary>
    /// 重新添加监听(移除并添加监听)
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public static void ReAddListener<T0, T1, T2>(this UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> call)
    {
        unityEvent?.RemoveListener(call);
        unityEvent?.AddListener(call);
    }

    /// <summary>
    /// 重新添加监听(移除并添加监听)
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public static void ReAddListener<T0, T1, T2, T3>(this UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> call)
    {
        unityEvent?.RemoveListener(call);
        unityEvent?.AddListener(call);
    }

    #endregion

    #region AddDisposableListener

    /// <summary>
    /// 添加一次性监听
    /// </summary>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void AddDisposableListener(this UnityEvent unityEvent, UnityAction call)
    {
        void TempCall()
        {
            unityEvent?.RemoveListener(TempCall);
            call?.Invoke();
        }

        unityEvent?.AddListener(TempCall);
    }

    /// <summary>
    /// 添加一次性监听
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void AddDisposableListener<T0>(this UnityEvent<T0> unityEvent, UnityAction<T0> call)
    {
        void TempCall(T0 t0)
        {
            unityEvent?.RemoveListener(TempCall);
            call?.Invoke(t0);
        }

        unityEvent?.AddListener(TempCall);
    }

    /// <summary>
    /// 添加一次性监听
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void AddDisposableListener<T0, T1>(this UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> call)
    {
        void TempCall(T0 t0, T1 t1)
        {
            unityEvent?.RemoveListener(TempCall);
            call?.Invoke(t0, t1);
        }

        unityEvent?.AddListener(TempCall);
    }

    /// <summary>
    /// 添加一次性监听
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void AddDisposableListener<T0, T1, T2>(this UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> call)
    {
        void TempCall(T0 t0, T1 t1, T2 t2)
        {
            unityEvent?.RemoveListener(TempCall);
            call?.Invoke(t0, t1, t2);
        }

        unityEvent?.AddListener(TempCall);
    }

    /// <summary>
    /// 添加一次性监听
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="unityEvent"></param>
    /// <param name="call"></param>
    public static void AddDisposableListener<T0, T1, T2, T3>(this UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> call)
    {
        void TempCall(T0 t0, T1 t1, T2 t2, T3 t3)
        {
            unityEvent?.RemoveListener(TempCall);
            call?.Invoke(t0, t1, t2, t3);
        }

        unityEvent?.AddListener(TempCall);
    }

    #endregion

//    #region ReAddDisposableListener
//
//    /// <summary>
//    /// 重新添加一次性监听
//    /// </summary>
//    /// <param name="unityEvent"></param>
//    /// <param name="call"></param>
//    public static void ReAddDisposableListener(this UnityEvent unityEvent, UnityAction call)
//    {
//        void TempCall()
//        {
//            unityEvent?.RemoveListener(TempCall);
//            call?.Invoke();
//        }
//
//        unityEvent?.ReAddListener(TempCall);
//    }
//
//    /// <summary>
//    /// 重新添加一次性监听
//    /// </summary>
//    /// <typeparam name="T0"></typeparam>
//    /// <param name="unityEvent"></param>
//    /// <param name="call"></param>
//    public static void ReAddDisposableListener<T0>(this UnityEvent<T0> unityEvent, UnityAction<T0> call)
//    {
//        void TempCall(T0 t0)
//        {
//            unityEvent?.RemoveListener(TempCall);
//            call?.Invoke(t0);
//        }
//
//        unityEvent?.ReAddListener(TempCall);
//    }
//
//    /// <summary>
//    /// 重新添加一次性监听
//    /// </summary>
//    /// <typeparam name="T0"></typeparam>
//    /// <typeparam name="T1"></typeparam>
//    /// <param name="unityEvent"></param>
//    /// <param name="call"></param>
//    public static void ReAddDisposableListener<T0, T1>(this UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> call)
//    {
//        void TempCall(T0 t0, T1 t1)
//        {
//            unityEvent?.RemoveListener(TempCall);
//            call?.Invoke(t0, t1);
//        }
//
//        unityEvent?.ReAddListener(TempCall);
//    }
//
//    /// <summary>
//    /// 重新添加一次性监听
//    /// </summary>
//    /// <typeparam name="T0"></typeparam>
//    /// <typeparam name="T1"></typeparam>
//    /// <typeparam name="T2"></typeparam>
//    /// <param name="unityEvent"></param>
//    /// <param name="call"></param>
//    public static void ReAddDisposableListener<T0, T1, T2>(this UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> call)
//    {
//        void TempCall(T0 t0, T1 t1, T2 t2)
//        {
//            unityEvent?.RemoveListener(TempCall);
//            call?.Invoke(t0, t1, t2);
//        }
//
//        unityEvent?.ReAddListener(TempCall);
//    }
//
//    /// <summary>
//    /// 重新添加一次性监听
//    /// </summary>
//    /// <typeparam name="T0"></typeparam>
//    /// <typeparam name="T1"></typeparam>
//    /// <typeparam name="T2"></typeparam>
//    /// <typeparam name="T3"></typeparam>
//    /// <param name="unityEvent"></param>
//    /// <param name="call"></param>
//    public static void ReAddDisposableListener<T0, T1, T2, T3>(this UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> call)
//    {
//        void TempCall(T0 t0, T1 t1, T2 t2, T3 t3)
//        {
//            unityEvent?.RemoveListener(TempCall);
//            call?.Invoke(t0, t1, t2, t3);
//        }
//
//        unityEvent?.ReAddListener(TempCall);
//    }
//
//    #endregion
}