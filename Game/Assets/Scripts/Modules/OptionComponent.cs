using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionComponent : MonoBehaviour
{
    public Option Option;
    public TextMeshProUGUI Title;
    public Button Btn;
    public TextMeshProUGUI Text;
    public AudioPlayer AudioPlayer;
    public int FlashTimes = 3;
    public float FlashDuration = 0.08f;
    private Color m_BtnBgPrimaryColor;
    private int m_FlashCount;

    private void Awake()
    {
        m_BtnBgPrimaryColor = Btn.image.color;
    }

    public void Set(Option option, EnglishCharacter englishCharacter, Action action)
    {
        if (option == null)
        {
            return;
        }

        Option = option;

        Title.text = englishCharacter.ToString();
        Text.text = option.Content;
        AudioPlayer.AudioClip = Option.AudioClip;
        Btn.onClick.ReAddListener(() =>
        {
            if (option.IsCorrect)
            {
                action?.Invoke();
            }
            else
            {
                ColorBreathToRed();
            }
        });
    }

    private void ColorBreathToRed()
    {
        if (m_FlashCount >= FlashTimes)
        {
            m_FlashCount = 0;
            return;
        }

        Btn.image.color = m_BtnBgPrimaryColor;
        Btn.image.DOColor(Color.red, FlashDuration).OnComplete(ColorBreathToPrimary);
        m_FlashCount++;
    }

    private void ColorBreathToPrimary()
    {
        Btn.image.color = Color.red;
        Btn.image.DOColor(m_BtnBgPrimaryColor, FlashDuration).OnComplete(ColorBreathToRed);
    }

    public void Reset()
    {
        Title.text = string.Empty;
        Text.text = string.Empty;
    }
}