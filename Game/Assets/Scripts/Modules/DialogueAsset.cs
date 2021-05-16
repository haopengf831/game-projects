using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAsset", menuName = "Creat Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    public List<Dialogue> Dialogues = new List<Dialogue>();
    public Queue<Dialogue> DialogueQueue = new Queue<Dialogue>();

    public void RefreshQueue()
    {
        DialogueQueue.Clear();
        var count = Dialogues.Count;
        for (int i = 0; i < count; i++)
        {
            var dialogue = Dialogues[i];
            dialogue.Index = i;
            DialogueQueue.Enqueue(dialogue);
        }
    }

    public Dialogue Dequeue()
    {
        return DialogueQueue.Dequeue();
    }

    public int Count()
    {
        return DialogueQueue.Count;
    }
}