using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Views;
using Loxodon.Framework.Views.Variables;
using System.Collections;
using TMPro;
using UnityEngine;

public class Talk3dView : View
{
    [SerializeField] protected VariableArray Variables;
    private TextMeshPro m_TextMesh;

    /// <summary>
    /// 间隔时间
    /// </summary>
    public float LetterPause = 0.1f;

    public void Init()
    {
        if (m_TextMesh == null)
        {
            m_TextMesh = Variables.Get<TextMeshPro>("content");
        }
    }

    /// <summary>
    /// 打字机输入
    /// </summary>
    /// <param name="content"></param>
    public void TypeContent(Dialogue content)
    {
        CoroutineTask.Run(TypeText(content));
    }

    /// <summary>
    /// 打字机效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText(Dialogue content)
    {
        if (m_TextMesh != null)
        {
            m_TextMesh.text = string.Empty;
            foreach (char letter in content.Content)
            {
                if (m_TextMesh != null)
                {
                    m_TextMesh.text += letter;
                }

                yield return new WaitForSeconds(LetterPause);
            }
        }

        yield return new WaitForSeconds(2.0f);
        Messenger.Default.Publish(content);
    }

    public void Clear()
    {
        if (m_TextMesh != null)
        {
            m_TextMesh.text = string.Empty;
        }
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}