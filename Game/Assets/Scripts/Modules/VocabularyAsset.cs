using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vocabulary
{
    public string ChineseWord = String.Empty;
    public string EnglishWord = String.Empty;
    public AudioClip AudioClip;
}

[CreateAssetMenu(fileName = "VocabularyAsset", menuName = "Creat Vocabulary Asset")]
public class VocabularyAsset : ScriptableObject
{
    public List<Vocabulary> Words = new List<Vocabulary>();

    private void Awake()
    {
        Words?.ForEach(word =>
        {
            word.ChineseWord.TrimAll();
            word.EnglishWord.TrimAll();
        });
    }
}