using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using UnityEngine;

public class RestaurantSetUpManager : SetUpManager
{
    public ModelWalk WuZiModelWalk;
    public ModelWalk JameModelWalk;
    public ModelWalk WaitressModelWalk;
    public BornPointTag BornPoint2;
    public LookAtPointTag LookAtPoint2;
    public Transform WuZiTalk3dViewSecondTransform;
    public Transform WaitressTalk3dViewSecondTransform;
    public Transform WuZiTalk3dViewEatDinnerTransform;

    public GameObject EatDinnerWuZi;
    public Transform WaitressStartWalkTransform;
    public GameObject CokeCola;
    public GameObject OrangeJuice;
    public GameObject Food;
    public BornPointTag EatDinnerBornPoint2;
    public LookAtPointTag EatDinnerLookAtPoint2;

    private bool m_IsWalkFinish;
    private bool m_IsUpFood;

    protected override void Awake()
    {
        base.Awake();

        WuZiModelWalk.SetActive(false);
        JameModelWalk.SetActive(false);
        WaitressModelWalk.SetActive(false);
        WaitressModelWalk.DoorCollider.SetActive(false);

        EatDinnerWuZi.SetActive(false);
        CokeCola.SetActive(false);
        OrangeJuice.SetActive(false);
        Food.SetActive(false);
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
            if (m_IsUpFood)
            {
                if (DialogueAssets.Count > 2)
                {
                    if (DialogueAssets[2].Count() > 0)
                    {
                        var dialogue = DialogueAssets[2].Dequeue();
                        if (dialogue != null)
                        {
                            Talk(dialogue);
                        }
                    }
                    else
                    {
                        Talk3dView.SetActive(false);
                        TalkUiView.SetActive(false);
                        SecondTalk3dView.SetActive(false);

                        FinishEvent?.Invoke();
                    }
                }
            }
            else
            {
                if (DialogueAssets.Count > 1)
                {
                    SecondQueueCount = DialogueAssets[1].Count();
                    if (SecondQueueCount > 0)
                    {
                        var dialogue = DialogueAssets[1].Dequeue();
                        if (dialogue != null)
                        {
                            Talk(dialogue);
                        }
                    }
                    else
                    {
                        StartUpFood();
                    }
                }
            }
        }
        else
        {
            StartGoIntoRestaurantWalk();
        }
    }

    private void StartGoIntoRestaurantWalk()
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
                    JameModelWalk.WalkStopEvent.AddListener(StopGoIntoRestaurantWalk);
                    JameModelWalk.Walk();
                    WuZiModelWalk.Walk();
                    TransitionController.FadeOut(0.5f);
                });
            });
        }
    }

    private void StopGoIntoRestaurantWalk()
    {
        WuZiModelWalk.SetActive(false);
        JameModelWalk.SetActive(false);
        Talk3dView.Clear();
        Talk3dView.SetActive(false);
        SecondTalk3dView.Clear();
        SecondTalk3dView.SetActive(false);

        m_IsWalkFinish = true;

        TransitionController.FadeIn(1.0f, () =>
        {
            Executors.RunOnMainThread(() =>
            {
                var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
                globalConfigurator.CameraRig.Set3DCameraTransform(BornPoint2.transform, LookAtPoint2.transform);

                var talk3dViewTransform = Talk3dView.transform;
                talk3dViewTransform.position = WuZiTalk3dViewSecondTransform.position;
                talk3dViewTransform.rotation = WuZiTalk3dViewSecondTransform.rotation;

                var secondTalk3dViewTransform = SecondTalk3dView.transform;
                secondTalk3dViewTransform.position = WaitressTalk3dViewSecondTransform.position;
                secondTalk3dViewTransform.rotation = WaitressTalk3dViewSecondTransform.rotation;

                EatDinnerWuZi.SetActive(true);
                WaitressModelWalk.SetActive(true);

                TransitionController.FadeOut(0.5f, Next);
            });
        });
    }

    private void StartUpFood()
    {
        if (SecondQueueCount <= 0 && DialogueAssets.Count > 2 && DialogueAssets[2].Dialogues.Count == DialogueAssets[2].DialogueQueue.Count)
        {
            FirstTalkMate.SetActive(false);
            Talk3dView.Clear();
            Talk3dView.SetActive(false);
            TalkUiView.SetActive(false);
            SecondTalk3dView.Clear();
            SecondTalk3dView.SetActive(false);

            TransitionController.FadeIn(1.0f, () =>
            {
                Executors.RunOnMainThread(() =>
                {
                    WaitressModelWalk.SetActive(true);
                    WaitressModelWalk.transform.position = WaitressStartWalkTransform.position;
                    WaitressModelWalk.transform.rotation = WaitressStartWalkTransform.rotation;
                    WaitressModelWalk.DoorCollider.SetActive(true);
                    WaitressModelWalk.WalkStopEvent.AddListener(StopUpFood);
                    WaitressModelWalk.Walk();

                    TransitionController.FadeOut(0.5f);
                });
            });
        }
    }

    private void StopUpFood()
    {
        WaitressModelWalk.DoorCollider.SetActive(false);
        WaitressModelWalk.SetActive(false);

        TransitionController.FadeIn(0.7f, () =>
        {
            Executors.RunOnMainThread(() =>
            {
                CokeCola.SetActive(true);
                OrangeJuice.SetActive(true);
                Food.SetActive(true);

                var globalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
                globalConfigurator.CameraRig.Set3DCameraTransform(EatDinnerBornPoint2.transform, EatDinnerLookAtPoint2.transform);

                var talk3dViewTransform = Talk3dView.transform;
                talk3dViewTransform.position = WuZiTalk3dViewEatDinnerTransform.position;
                talk3dViewTransform.rotation = WuZiTalk3dViewEatDinnerTransform.rotation;

                SecondTalk3dView.Clear();
                SecondTalk3dView.SetActive(false);
                Talk3dView.Clear();
                Talk3dView.SetActive(true);
                m_IsUpFood = true;

                TransitionController.FadeOut(0.5f, Next);
            });
        });
    }
}