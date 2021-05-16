using System;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Messaging;
using Loxodon.Log;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class StageManager
{
    public StageManager()
    {
        try
        {
            m_ISubscription = Messenger.Default.Subscribe<StageEventEnum>(MessengerSubscribe);
        }
        catch (Exception e)
        {
            m_Log.Fatal(e.Message);
        }
    }

    ~StageManager()
    {
        m_ISubscription?.Dispose();
        m_ISubscription = null;
    }

    private static readonly ILog m_Log = LogManager.GetLogger(typeof(StageManager));
    private IDisposable m_ISubscription;

    public StageConfiguration SceneConfiguration { get; private set; }
    private StageBase m_LastScene;
    public StageBase CurScene { get; private set; }
    private SceneInstance m_TransitionSceneInstance;
    public bool IsLoading { get; private set; } = false; //是否正在加载中

    private bool m_IsResourcesLoadCompleted = false;
    private bool m_IsTransitionCompleted = false;

    private TransitionController m_TransitionController;
    private StageEventEnum m_CurStageEventEnum = StageEventEnum.None;
    private bool m_IsTwice;
    private GlobalConfigurator m_GlobalConfigurator;

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="sceneConfiguration"></param>
    /// <typeparam name="TScene"></typeparam>
    public void ChangeScene<TScene>(StageConfiguration sceneConfiguration = null) where TScene : StageBase, new()
    {
        try
        {
            if (IsLoading)
            {
                if (CurScene != null && m_Log.IsErrorEnabled)
                {
                    m_Log.Error(CurScene.StageEnum + " Scene is Loading!");
                }

                return;
            }

            if (m_GlobalConfigurator == null)
            {
                m_GlobalConfigurator = Context.GetApplicationContext().GetService<GlobalConfigurator>();
            }

            if (m_TransitionController == null)
            {
                m_TransitionController = m_GlobalConfigurator.TransitionController;
            }

            IsLoading = true;
            SceneConfiguration = sceneConfiguration;
            CurScene = new TScene();
            Messenger.Default.Publish(StageEventEnum.StartChangeScene);
        }
        catch (Exception e)
        {
            m_Log.Fatal(e);
        }
    }

    /// <summary>
    /// 状态机
    /// </summary>
    /// <param name="sceneEventEnum"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void MessengerSubscribe(StageEventEnum sceneEventEnum)
    {
        try
        {
            m_CurStageEventEnum = sceneEventEnum;

            switch (sceneEventEnum)
            {
                case StageEventEnum.None:
                    break;
                case StageEventEnum.StartChangeScene:
                {
                    CurScene.BeforeTransition();
                    Messenger.Default.Publish(CurScene.StageEnum != StageEnum.Init ? StageEventEnum.StartTransitionEffectFadeIn : StageEventEnum.CompleteTransitionEffectFadeIn);
                }
                    break;
                case StageEventEnum.StartLoadResources:
                {
                    CurScene.OnGetPreLoadResources();
                    CurScene.OnPreLoadResources();
                }
                    break;
                case StageEventEnum.CompleteLoadResources:
                {
                    m_IsResourcesLoadCompleted = true;
                    Messenger.Default.Publish(StageEventEnum.CompleteLoadResourcesOrTransitionEffectIn);
                }
                    break;
                case StageEventEnum.StartTransitionEffectFadeIn:
                {
                    m_TransitionController.FadeInTransition(1.5f);
                }
                    break;
                case StageEventEnum.CompleteTransitionEffectFadeIn:
                {
                    if (m_IsTwice)
                    {
                        SceneLocator.LoadSceneAsync(CurScene.StageEnum.ToString(), LoadSceneMode.Additive, true, 100,
                            result =>
                            {
                                CurScene.StageInstance = result;
                                CurScene.OnLoadSceneComplete(() =>
                                {
                                    CurScene.Resources?.ForEach(resource => { SceneManager.MoveGameObjectToScene(resource, result.Scene); });
                                    Messenger.Default.Publish(StageEventEnum.CompleteLoadScene);
                                });
                            });

                        m_IsTwice = false;
                        return;
                    }

                    m_LastScene?.OnDispose();
                    m_LastScene = null;
                    UnloadUnusedAssets();

                    SceneLocator.LoadSceneAsync(StageEnum.Transition.ToString(), LoadSceneMode.Single, true, 100,
                        result =>
                        {
                            if (CurScene.StageEnum != StageEnum.Init)
                            {
                                Messenger.Default.Publish(StageEventEnum.StartTransitionEffectFadeOut);
                            }

                            m_TransitionSceneInstance = result;
                            CurScene.OnInitialize();
                            Messenger.Default.Publish(StageEventEnum.StartLoadResources);
                            m_IsTransitionCompleted = true;
                            Messenger.Default.Publish(StageEventEnum.CompleteLoadResourcesOrTransitionEffectIn);
                        });
                }
                    break;
                case StageEventEnum.CompleteLoadResourcesOrTransitionEffectIn:
                {
                    if (m_IsResourcesLoadCompleted && m_IsTransitionCompleted)
                    {
                        Messenger.Default.Publish(StageEventEnum.StartLoadScene);
                    }
                }
                    break;
                case StageEventEnum.StartLoadScene:
                {
                    if (CurScene.StageEnum != StageEnum.Init)
                    {
                        m_IsTwice = true;
                        Messenger.Default.Publish(StageEventEnum.StartTransitionEffectFadeIn);
                    }

                    else
                    {
                        SceneLocator.LoadSceneAsync(CurScene.StageEnum.ToString(), LoadSceneMode.Additive, true, 100,
                            result =>
                            {
                                CurScene.StageInstance = result;
                                CurScene.OnLoadSceneComplete(() =>
                                {
                                    CurScene.Resources?.ForEach(resource => { SceneManager.MoveGameObjectToScene(resource, result.Scene); });
                                    Messenger.Default.Publish(StageEventEnum.CompleteLoadScene);
                                });
                            });
                    }
                }
                    break;
                case StageEventEnum.CompleteLoadScene:
                {
                    SceneLocator.UnLoadSceneAsync(m_TransitionSceneInstance, false, result =>
                    {
                        m_TransitionSceneInstance = new SceneInstance();
                        OnSceneLoadComplete();

                        if (CurScene.StageEnum != StageEnum.Init)
                        {
                            Messenger.Default.Publish(StageEventEnum.StartTransitionEffectFadeOut);
                        }
                    });
                }
                    break;
                case StageEventEnum.StartTransitionEffectFadeOut:
                {
                    m_TransitionController.FadeOutTransition(1.5f);
                }
                    break;
                case StageEventEnum.CompleteTransitionEffectFadeOut:
                {
                }
                    break;
                case StageEventEnum.CompleteReadTable:
                    break;
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(sceneEventEnum), sceneEventEnum, null);
                }
            }
        }
        catch (Exception e)
        {
            m_Log.Fatal(e);
        }
    }

    /// <summary>
    /// 场景加载完毕
    /// </summary>
    private void OnSceneLoadComplete()
    {
        try
        {
            IsLoading = false;
            m_IsResourcesLoadCompleted = false;
            m_IsTransitionCompleted = false;
            m_LastScene = CurScene;
            CurScene.OnCompleteAll();
        }
        catch (Exception e)
        {
            m_Log.Fatal(e);
        }
    }

    /// <summary>
    /// 卸载未使用资源
    /// </summary>
    private void UnloadUnusedAssets()
    {
        try
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
        catch (Exception e)
        {
            m_Log.Fatal(e);
        }
    }
}