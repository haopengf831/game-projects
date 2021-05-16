using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using UnityEngine;

public class LibrarySetUpManager : SetUpManager
{
    public ModelWalk WalkManager;
    public BornPointTag BornPoint2;
    public LookAtPointTag LookAtPoint2;
    public Transform Talk3dViewSecondTransform;

    private bool m_IsWalkFinish;

    protected override void Awake()
    {
        base.Awake();

        WalkManager.SetActive(false);
    }

    protected override void Next()
    {
        if (DialogueAssets.Count > 0)
        {
            FirstQueueCount = DialogueAssets[0].Count();
            if (FirstQueueCount > 0)
            {
                var dialogue = DialogueAssets[0].Dequeue();
                if (dialogue != null)
                {
                    Talk(dialogue);
                }
            }
        }

        if (m_IsWalkFinish)
        {
            if (DialogueAssets.Count > 0)
            {
                FirstQueueCount = DialogueAssets[1].Count();
                if (FirstQueueCount > 0)
                {
                    var dialogue = DialogueAssets[1].Dequeue();
                    if (dialogue != null)
                    {
                        Talk(dialogue);
                    }
                }
                else
                {
                    Talk3dView.SetActive(false);
                    TalkUiView.SetActive(false);

                    FinishEvent?.Invoke();
                }
            }
        }
        else
        {
            StartWalk();
        }
    }

    private void StartWalk()
    {
        if (FirstQueueCount <= 0 && DialogueAssets.Count > 1 && DialogueAssets[1].Dialogues.Count == DialogueAssets[1].DialogueQueue.Count)
        {
            FirstTalkMate.SetActive(false);
            Talk3dView.SetActive(false);
            TalkUiView.SetActive(false);

            TransitionController.FadeIn(1.0f, () =>
            {
                Executors.RunOnMainThread(() =>
                {
                    WalkManager.WalkStopEvent.AddListener(StopWalk);
                    WalkManager.Walk();
                    TransitionController.FadeOut(0.5f);
                });
            });
        }
    }

    private void StopWalk()
    {
        m_IsWalkFinish = true;
        TransitionController.FadeIn(1.0f, () =>
        {
            Executors.RunOnMainThread(() =>
            {
                var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
                globalConfigurator.CameraRig.Set3DCameraTransform(BornPoint2.transform, LookAtPoint2.transform);

                var talk3dViewTransform = Talk3dView.transform;
                talk3dViewTransform.position = Talk3dViewSecondTransform.position;
                talk3dViewTransform.rotation = Talk3dViewSecondTransform.rotation;
                WalkManager.SetActive(false);

                TransitionController.FadeOut(0.5f, Next);
            });
        });
    }
}