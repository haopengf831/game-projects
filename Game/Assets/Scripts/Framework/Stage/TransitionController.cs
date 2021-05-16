using System;
using DG.Tweening;
using Loxodon.Framework.Messaging;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;

    public virtual void FadeInTransition(float duration)
    {
        FadeIn(duration, () => { Messenger.Default.Publish(StageEventEnum.CompleteTransitionEffectFadeIn); });
    }

    public virtual void FadeOutTransition(float duration)
    {
        FadeOut(duration, () => { Messenger.Default.Publish(StageEventEnum.CompleteTransitionEffectFadeOut); });
    }

    public virtual void FadeIn(float duration, Action action = null)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.DOFade(1.0f, duration).OnComplete(() => { action?.Invoke(); });
        }
    }

    public virtual void FadeOut(float duration, Action action = null)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.DOFade(0.0f, duration).OnComplete(() => { action?.Invoke(); });
        }
    }
}