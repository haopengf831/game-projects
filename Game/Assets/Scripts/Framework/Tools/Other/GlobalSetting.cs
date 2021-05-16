using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Level = Loxodon.Log.Level;

public sealed class GlobalSetting : MonoBehaviour
{
    private const int DefaultDpi = 96; // default windows dpi

    private float m_GameSpeedBeforePause = 1.0f;

    [SerializeField] private bool m_RunInBackground = true;

    [SerializeField] private bool m_NeverSleep = true;

    [SerializeField] [Range(60, 160)] private int m_FrameRate = 72;

    [SerializeField] [Range(0, 100)] private float m_GameSpeed = 1.0f;

    /// <summary>
    /// 获取或设置游戏帧率。
    /// </summary>
    public int FrameRate
    {
        get => m_FrameRate;
        set => Application.targetFrameRate = m_FrameRate = value;
    }

    /// <summary>
    /// 获取或设置游戏速度。
    /// </summary>
    public float GameSpeed
    {
        get => m_GameSpeed;
        set => Time.timeScale = m_GameSpeed = (value >= 0.0f ? value : 0.0f);
    }

    /// <summary>
    /// 获取游戏是否暂停。
    /// </summary>
    public bool IsGamePaused => m_GameSpeed <= 0.0f;

    /// <summary>
    /// 获取是否正常游戏速度。
    /// </summary>
    public bool IsNormalGameSpeed => Math.Abs(m_GameSpeed - 1.0f) < 0.0001f;

    /// <summary>
    /// 获取或设置是否允许后台运行。
    /// </summary>
    public bool RunInBackground
    {
        get => m_RunInBackground;

        set => Application.runInBackground = m_RunInBackground = value;
    }

    /// <summary>
    /// 获取或设置是否禁止休眠。
    /// </summary>
    public bool NeverSleep
    {
        get => m_NeverSleep;
        set
        {
            m_NeverSleep = value;
            Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        }
    }

    [field: SerializeField] public Level LogLevel { get; set; } = Level.ALL;

    private void Awake()
    {
        Application.targetFrameRate = m_FrameRate;
        Time.timeScale = m_GameSpeed;
        Application.runInBackground = m_RunInBackground;
        Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        DOTween.Init();
#if UNITY_5_6_OR_NEWER
        Application.lowMemory += OnLowMemory;
#endif
    }

    private void OnDestroy()
    {
#if UNITY_5_6_OR_NEWER
        Application.lowMemory -= OnLowMemory;
#endif
        CommonLog.Dispose();
    }

    private void Update()
    {
        Time.timeScale = m_GameSpeed;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EditorApplication.isPlaying = false;
        }
#endif
    }

    private void OnApplicationQuit()
    {
        CommonLog.Dispose();
    }

    /// <summary>
    /// 暂停游戏。
    /// </summary>
    public void PauseGame()
    {
        if (IsGamePaused)
        {
            return;
        }

        m_GameSpeedBeforePause = GameSpeed;
        GameSpeed = 0f;
    }

    /// <summary>
    /// 恢复游戏。
    /// </summary>
    public void ResumeGame()
    {
        if (!IsGamePaused)
        {
            return;
        }

        GameSpeed = m_GameSpeedBeforePause;
    }

    /// <summary>
    /// 重置为正常游戏速度。
    /// </summary>
    public void ResetNormalGameSpeed()
    {
        if (IsNormalGameSpeed)
        {
            return;
        }

        GameSpeed = 1.0f;
    }

    private void OnLowMemory()
    {
        Resources.UnloadUnusedAssets();
    }
}