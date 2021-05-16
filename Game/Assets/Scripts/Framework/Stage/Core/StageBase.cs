using System;
using System.Collections.Generic;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Messaging;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public abstract class StageBase : IStageBase
{
    public abstract StageEnum StageEnum { get; }

    public virtual SceneInstance StageInstance { get; set; }

    public virtual List<GameObject> Resources { get; private set; }

    public virtual void BeforeTransition()
    {
    }

    public virtual void OnGetPreLoadResources()
    {
    }

    public virtual void OnPreLoadResources()
    {
        Messenger.Default.Publish(StageEventEnum.CompleteLoadResources);
    }

    public virtual void OnInitialize()
    {
        Resources = new List<GameObject>();
    }

    public virtual void OnLoadSceneComplete(Action action = null)
    {
        action?.Invoke();
    }

    public virtual void OnCompleteAll()
    {
        
    }

    public virtual void OnDispose()
    {
        Resources?.ForEach(x => x.Release());
        Resources?.Clear();
    }
}