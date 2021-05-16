using Loxodon.Framework.Messaging;
using System;
using System.Collections.Generic;
using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.Events;

public class SetUpManager : MonoBehaviour
{
    public BornPointTag BornPoint;
    public LookAtPointTag LookAtPoint;
    public Talk3dView Talk3dView;
    public Talk3dView SecondTalk3dView;
    public TalkUiView TalkUiView;
    public VocabularyUiView VocabularyUiView;
    public GameObject FirstTalkMate;
    public AudioSource AudioSource;
    public List<DialogueAsset> DialogueAssets = new List<DialogueAsset>();
    public FinishUiView FinishUiView;
    private IDisposable m_DialogueSubs;
    private IDisposable m_OptionSubs;
    protected int FirstQueueCount;
    protected int SecondQueueCount;
    protected TransitionController TransitionController;
    public UnityEvent FinishEvent = new UnityEvent();

    protected virtual void Awake()
    {
        DialogueAssets.ForEach(asset => { asset.RefreshQueue(); });

        TransitionController = Context.GetApplicationContext().GetService<GlobalConfigurator>().TransitionController;

        Talk3dView.Init();
        Talk3dView.SetActive(false);

        if (SecondTalk3dView != null)
        {
            SecondTalk3dView.Init();
            SecondTalk3dView.SetActive(false);
        }

        TalkUiView.Init();
        TalkUiView.SetActive(false);

        VocabularyUiView.SetActive(true);

        m_DialogueSubs = Messenger.Default.Subscribe<Dialogue>(dialogue => { Next(); });
        m_OptionSubs = Messenger.Default.Subscribe<Option>(option => { Next(); });

        VocabularyUiView.CloseBtn.onClick.ReAddListener(Next);

        FirstTalkMate.SetActive(true);

        if (AudioSource == null)
        {
            AudioSource = this.RequireComponent<AudioSource>();
            AudioSource.playOnAwake = false;
            AudioSource.loop = false;
        }

        if (FinishUiView != null)
        {
            FinishUiView.SetActive(false);
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_DialogueSubs != null)
        {
            m_DialogueSubs.Dispose();
            m_DialogueSubs = null;
        }

        if (m_OptionSubs != null)
        {
            m_OptionSubs.Dispose();
            m_OptionSubs = null;
        }
    }

    protected virtual void Next()
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
            else
            {
                FinishEvent?.Invoke();
            }
        }
    }

    protected virtual void Talk(Dialogue dialogue)
    {
        AudioSource.Stop();

        if (dialogue.IsSelf || dialogue.IsOption)
        {
            Talk3dView.SetActive(false);
            if (SecondTalk3dView != null)
            {
                SecondTalk3dView.SetActive(false);
            }

            TalkUiView.SetActive(true);
            TalkUiView.TypeContent(dialogue);

            if (dialogue.AudioClip != null)
            {
                AudioSource.clip = dialogue.AudioClip;
                AudioSource.Play();
            }
        }
        else
        {
            if (dialogue.IsThirdPerson)
            {
                TalkUiView.SetActive(false);
                Talk3dView.SetActive(false);
                if (SecondTalk3dView != null)
                {
                    SecondTalk3dView.SetActive(true);
                    SecondTalk3dView.TypeContent(dialogue);

                    if (dialogue.AudioClip != null)
                    {
                        AudioSource.clip = dialogue.AudioClip;
                        AudioSource.Play();
                    }
                }
            }
            else
            {
                TalkUiView.SetActive(false);
                if (SecondTalk3dView != null)
                {
                    SecondTalk3dView.SetActive(false);
                }

                Talk3dView.SetActive(true);
                Talk3dView.TypeContent(dialogue);

                if (dialogue.AudioClip != null)
                {
                    AudioSource.clip = dialogue.AudioClip;
                    AudioSource.Play();
                }
            }
        }
    }
}