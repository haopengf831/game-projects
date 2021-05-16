using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int Index;
    public bool IsSelf;
    public bool IsThirdPerson;
    public string Content = string.Empty;
    public AudioClip AudioClip;
    public bool IsOption;
    public List<Option> Options = new List<Option>();
}