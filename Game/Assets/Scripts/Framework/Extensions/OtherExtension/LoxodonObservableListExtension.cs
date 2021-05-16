using System;

public static partial class LoxodonObservableListExtension
{
    /// <summary>
    /// 若ObservableList包含符合指定条件的元素则返回true,反之为false
    /// </summary>
    /// <param name="observableList"></param>
    /// <param name="match"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool Contains<T>(this Loxodon.Framework.Observables.ObservableList<T> observableList, Predicate<T> match)
    {
        if (observableList == null)
        {
            throw new ArgumentNullException(nameof(observableList));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        foreach (var item in observableList)
        {
            if (match(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 寻找符合指定条件的元素
    /// </summary>
    /// <param name="observableList"></param>
    /// <param name="match"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T Find<T>(this Loxodon.Framework.Observables.ObservableList<T> observableList, Predicate<T> match)
    {
        if (observableList == null)
        {
            throw new ArgumentNullException(nameof(observableList));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        foreach (var item in observableList)
        {
            if (match(item))
            {
                return item;
            }
        }

        return default;
    }

    /// <summary>
    /// 寻找符合指定条件的元素
    /// </summary>
    /// <param name="observableList"></param>
    /// <param name="match"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Loxodon.Framework.Observables.ObservableList<T> FindAll<T>(this Loxodon.Framework.Observables.ObservableList<T> observableList, Predicate<T> match)
    {
        if (observableList == null)
        {
            throw new ArgumentNullException(nameof(observableList));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        var objList = new Loxodon.Framework.Observables.ObservableList<T>();
        foreach (var item in observableList)
        {
            if (match(item))
            {
                objList.Add(item);
            }
        }

        return objList;
    }

    /// <summary>
    /// 移除符合指定条件的元素
    /// </summary>
    /// <param name="observableList"></param>
    /// <param name="match"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool Remove<T>(this Loxodon.Framework.Observables.ObservableList<T> observableList, Predicate<T> match)
    {
        if (observableList == null)
        {
            throw new ArgumentNullException(nameof(observableList));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        var objList = new Loxodon.Framework.Observables.ObservableList<T>();
        foreach (var item in observableList)
        {
            if (match(item))
            {
                objList.Add(item);
            }
        }

        foreach (var obj in objList)
        {
            observableList.Remove(obj);
        }

        return true;
    }
}