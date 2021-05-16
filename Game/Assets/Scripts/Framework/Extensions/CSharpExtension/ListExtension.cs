using System;
using System.Collections.Generic;
using System.Linq;

public static partial class ListExtension
{
    /// <summary>
    /// 将Item添加进IList集合,如果集合中已经包含此Item,则不添加
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public static IList<TItem> AddUnique<TItem>(this IList<TItem> list, TItem item)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (!list.Contains(item))
        {
            list.Add(item);
        }

        return list;
    }

    /// <summary>
    /// 将指定集合的元素添加到IList集合的末尾,如果IList集合中已经包含指定集合中的某个元素，则不添加此元素
    /// </summary>
    /// <param name="list"></param>
    /// <param name="collection"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public static IList<TItem> AddRangeUnique<TItem>(this IList<TItem> list, IEnumerable<TItem> collection)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        foreach (var pair in collection)
        {
            list.AddUnique(pair);
        }

        return list;
    }

    /// <summary>
    /// 移除集合中重复元素
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public static IList<TItem> RemoveDuplicate<TItem>(this IList<TItem> list)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (list.Count > 1)
        {
            for (int i = 0; i < list.Count; i++) //外循环是循环的次数
            {
                for (int j = list.Count - 1; j > i; j--) //内循环是 外循环一次比较的次数
                {
                    var tempI = list[i];
                    var tempJ = list[j];
                    if (tempI == null)
                    {
                        if (tempJ == null)
                        {
                            list.RemoveAt(j);
                        }
                    }
                    else
                    {
                        if (tempI.Equals(tempJ))
                        {
                            list.RemoveAt(j);
                        }
                    }
                }
            }
        }

        return list;
    }

    /// <summary>
    /// 把一个集合以数量portion个为一组来分组
    /// </summary>
    /// <param name="list"></param>
    /// <param name="portion"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IList<IList<TItem>> DivideToListByPortion<TItem>(this IList<TItem> list, int portion)
    {
        var groupList = new List<IList<TItem>>();

        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (list.Count <= 0)
        {
            throw new ArgumentException(nameof(list));
        }

        if (portion <= 0)
        {
            portion = list.Count;
        }

        var count = portion;
        for (int i = 0; i < list.Count; i += portion)
        {
            var cList = list.Take(count).Skip(i).ToList();
            count += portion;
            groupList.Add(cList);
        }

        return groupList;
    }

    /// <summary>
    /// 把一个集合以数量portion个为一组来分组
    /// </summary>
    /// <param name="list"></param>
    /// <param name="portion"></param>
    /// <typeparam name="TItem"></typeparam>
    /// <returns></returns>
    public static IList<TItem>[] DivideToArrayByPortion<TItem>(this IList<TItem> list, int portion)
    {
        var groupList = list.DivideToListByPortion(portion);
        var groupArray = groupList.ToArray();
        return groupArray;
    }
}