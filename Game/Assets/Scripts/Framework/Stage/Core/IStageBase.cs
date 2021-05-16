using System;

public interface IStageBase
{
    StageEnum StageEnum { get; }

    void OnInitialize();

    void OnGetPreLoadResources();

    void OnPreLoadResources();

    void OnLoadSceneComplete(Action action);

    void OnCompleteAll();

    void OnDispose();
}