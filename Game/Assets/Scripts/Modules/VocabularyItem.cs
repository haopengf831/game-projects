using System;
using UnityEngine;
using UnityEngine.UI;

public class VocabularyItem : MonoBehaviour
{
    public Text Content;
    public Button AudioBtn;
    public AudioPlayer AudioPlayer;
    public Vocabulary Vocabulary { get; set; }

    public void SetData(Vocabulary vocabulary)
    {
        Vocabulary = vocabulary;
        Content.text = vocabulary.ChineseWord + " " + vocabulary.EnglishWord;
        AudioPlayer.AudioClip = Vocabulary.AudioClip;
    }

    private void OnDisable()
    {
        AudioPlayer.Stop();
    }
}