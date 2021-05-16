using System.Collections.Generic;
using System;

public class HyperlinksEvent
{
    private Dictionary<string, Action> clickEventSet = new Dictionary<string, Action>();

    public void AddEvent(string character, Action action)
    {
        if (!clickEventSet.TryGetValue(character, out Action targetAction))
            clickEventSet[character] = action;
    }

    public void RemoveEvent(string character)
    {
        if (clickEventSet.TryGetValue(character, out Action targetAction))
            clickEventSet.Remove(character);
    }

    public void RemoveAllEvent() => clickEventSet.Clear();

    public void ExecuteEvent(string character)
    {
        if (clickEventSet.TryGetValue(character, out Action targetAction))
            targetAction();
    }
}