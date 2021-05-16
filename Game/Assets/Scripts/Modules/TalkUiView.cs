using System;
using System.Collections;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Views.Variables;
using UnityEngine;
using UnityEngine.UI;
using Loxodon.Framework.Messaging;

public class TalkUiView : MonoBehaviour
{
    [SerializeField] protected VariableArray Variables;
    private Text m_Text;
    private OptionGroup m_OptionGroup;
    private Text m_BelowText;
    private bool m_IsTyping;

    /// <summary>
    /// 间隔时间
    /// </summary>
    public float LetterPause = 0.1f;

    public void Init()
    {
        if (m_Text == null)
        {
            m_Text = Variables.Get<Text>("text");
        }

        if (m_OptionGroup == null)
        {
            m_OptionGroup = Variables.Get<OptionGroup>("options");
        }

        if (m_BelowText == null)
        {
            m_BelowText = Variables.Get<Text>("belowText");
        }
    }

    /// <summary>
    /// 输入
    /// </summary>
    /// <param name="dialogue"></param>
    public void TypeContent(Dialogue dialogue)
    {
        if (m_IsTyping)
        {
            return;
        }

        if (dialogue.IsOption)
        {
            m_Text.text = string.Empty;
            m_BelowText.SetActive(false);
            m_Text.SetActive(true);
            m_OptionGroup.SetActive(true);
            CoroutineTask.Run(TypeText(m_Text, dialogue, false));
            m_OptionGroup.RefreshOptionGroup(dialogue.Options);
            m_OptionGroup.SelectEvent.ReAddListener(option => { Messenger.Default.Publish(option); });
        }
        else
        {
            m_BelowText.text = string.Empty;
            m_Text.SetActive(false);
            m_OptionGroup.SetActive(false);
            m_BelowText.SetActive(true);
            CoroutineTask.Run(TypeText(m_BelowText, dialogue));
        }
    }

    /// <summary>
    /// 打字机效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText(Text text, Dialogue content, bool isPublish = true)
    {
        m_IsTyping = true;
        if (text != null)
        {
            text.text = string.Empty;
            foreach (char letter in content.Content)
            {
                if (text != null)
                {
                    text.text += letter;
                }

                yield return new WaitForSeconds(LetterPause);
            }
        }

        if (isPublish)
        {
            yield return new WaitForSeconds(2.0f);
            m_IsTyping = false;
            Messenger.Default.Publish(content);
        }
        else
        {
            m_IsTyping = false;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}