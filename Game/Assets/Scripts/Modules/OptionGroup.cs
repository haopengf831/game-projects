using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EnglishCharacter
{
    A = 0,
    B = 1,
    C = 2,
    D = 3,
    E = 4,
    F = 5,
    G = 6,
    H = 7,
    I = 8,
    J = 9,
    K = 10,
    L = 11,
    M = 12,
    N = 13,
    O = 14,
    P = 15,
    Q = 16,
    R = 17,
    S = 18,
    T = 19,
    U = 20,
    V = 21,
    W = 22,
    X = 23,
    Y = 24,
    Z = 25,
}

public class OptionGroup : MonoBehaviour
{
    [System.Serializable]
    public class UnityEvent : UnityEvent<Option>
    {
    }

    public UnityEvent SelectEvent = new UnityEvent();

    public OptionComponent OptionComponentItem;
    private List<OptionComponent> m_OptionComponents = new List<OptionComponent>();

    protected void Awake()
    {
        OptionComponentItem.SetActive(false);
    }

    private List<T> RandomSort<T>(List<T> list)
    {
        var random = new System.Random();
        var newList = new List<T>();
        foreach (var item in list)
        {
            newList.Insert(random.Next(newList.Count), item);
        }

        return newList;
    }

    /// <summary>
    /// 刷新对话组
    /// </summary>
    /// <param name="options"></param>
    public void RefreshOptionGroup(List<Option> options)
    {
        m_OptionComponents?.ForEach(optionComponent => { optionComponent.Release(); });
        m_OptionComponents?.Clear();

        options = RandomSort(options);

        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];

            var tempOptionComponent = Instantiate(OptionComponentItem, this.transform);
            tempOptionComponent.SetActive(true);
            tempOptionComponent.Set(option, (EnglishCharacter) i, () => { Select(tempOptionComponent); });
            m_OptionComponents?.Add(tempOptionComponent);
        }
    }

    protected virtual void Select(OptionComponent optionComponent)
    {
        if (optionComponent != null)
        {
            SelectEvent?.Invoke(optionComponent.Option);
        }
    }
}