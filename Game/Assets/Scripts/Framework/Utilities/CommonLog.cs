using System;
using System.IO;
using Loxodon.Log;
using Loxodon.Log.Log4Net;
using UnityEngine;

public class CommonLog
{
    private static ILog m_Log = LogManager.GetLogger(typeof(CommonLog));
    private static readonly Exception m_Exception = new Exception("日志系统异常");
    private static readonly string m_ConfigFilename = "Log4NetConfigRelease";
    private static readonly string m_EditorLogPath = "/Editor Default Resources/Log/";

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize(Level logLevel)
    {
        try
        {
#if UNITY_EDITOR
            LogManager.Default.Level = Level.ALL;
#else
            LogManager.Default.Level = logLevel;
#endif

            var configText = Resources.Load<TextAsset>(m_ConfigFilename);
            if (configText != null)
            {
                using (var memStream = new MemoryStream(configText.bytes))
                {
                    log4net.Config.XmlConfigurator.Configure(memStream);
                }
            }

            LogManager.Registry(new Log4NetFactory());
            m_Log = LogManager.GetLogger(typeof(CommonLog));
        }
        catch (Exception e)
        {
            throw;
        }
    }

    /// <summary>
    /// 注销
    /// </summary>
    public static void Dispose()
    {
        try
        {
            log4net.LogManager.Shutdown();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    #region Debug

    /// <summary>
    /// 输出测试日志 --- 用于开发测试阶段的Debug日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    public static void Log(object message)
    {
        try
        {
            if (m_Log.IsDebugEnabled)
            {
                m_Log.Debug(message);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出测试日志 --- 用于开发测试阶段的Debug日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Log(object message, Exception exception)
    {
        try
        {
            if (m_Log.IsDebugEnabled)
            {
                m_Log.Debug(message, exception);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出测试格式日志 --- 用于开发测试阶段的Debug格式日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogFormat(string format, params object[] args)
    {
        try
        {
            if (m_Log.IsDebugEnabled)
            {
                m_Log.DebugFormat(format, args);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion

    #region Info

    /// <summary>
    /// 输出信息日志 --- 用于关键且必要的信息的日志输出，Debug和Release阶段都会开放，不建议频繁调用
    /// </summary>
    /// <param name="message"></param>
    public static void LogInfo(object message)
    {
        try
        {
            if (m_Log.IsInfoEnabled)
            {
                m_Log.Info(message);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出信息日志 --- 用于关键且必要的信息的日志输出，Debug和Release阶段都会开放，不建议频繁调用
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogInfo(object message, Exception exception)
    {
        try
        {
            if (m_Log.IsInfoEnabled)
            {
                m_Log.Info(message, exception);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出信息格式日志 --- 用于关键且必要的信息的格式日志输出，Debug和Release阶段都会开放，不建议频繁调用
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogInfoFormat(string format, params object[] args)
    {
        try
        {
            if (m_Log.IsInfoEnabled)
            {
                m_Log.InfoFormat(format, args);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion

    #region Warning

    /// <summary>
    /// 输出警告日志 --- 用于开发测试阶段的Warning日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    public static void LogWarning(object message)
    {
        try
        {
            if (m_Log.IsWarnEnabled)
            {
                m_Log.Warn(message);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出警告日志 --- 用于开发测试阶段的Warning日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogWarning(object message, Exception exception)
    {
        try
        {
            if (m_Log.IsWarnEnabled)
            {
                m_Log.Warn(message, exception);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出警告格式日志 --- 用于开发测试阶段的Warning格式日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogWarningFormat(string format, params object[] args)
    {
        try
        {
            if (m_Log.IsWarnEnabled)
            {
                m_Log.WarnFormat(format, args);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion

    #region Error

    /// <summary>
    /// 输出错误日志 --- 用于开发测试阶段的Error日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(object message)
    {
        try
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Error(message);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出错误日志 --- 用于开发测试阶段的Error日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogError(object message, Exception exception)
    {
        try
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Error(message, exception);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出错误格式日志 --- 用于开发测试阶段的Error格式日志输出，Release阶段会关闭此通道
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogErrorFormat(string format, params object[] args)
    {
        try
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.ErrorFormat(format, args);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion

    #region Fatal

    /// <summary>
    /// 输出致命日志 --- 用于致命错误信息的日志输出，Debug和Release阶段都会开放，只建议在发生极其严重的错误的部分调用
    /// </summary>
    /// <param name="message"></param>
    public static void LogFatal(object message)
    {
        try
        {
            if (m_Log.IsFatalEnabled)
            {
                m_Log.Fatal(message);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出致命日志 --- 用于致命错误信息的日志输出，Debug和Release阶段都会开放，只建议在发生极其严重的错误的部分调用
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogFatal(object message, Exception exception)
    {
        try
        {
            if (m_Log.IsFatalEnabled)
            {
                m_Log.Fatal(message, exception);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    /// <summary>
    /// 输出致命格式日志 --- 用于致命错误信息的格式日志输出，Debug和Release阶段都会开放，只建议在发生极其严重的错误的部分调用
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogFatalFormat(string format, params object[] args)
    {
        try
        {
            if (m_Log.IsFatalEnabled)
            {
                m_Log.FatalFormat(format, args);
            }
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion

    #region Local

    /// <summary>
    /// 记录本地日志
    /// </summary>
    /// <param name="logContent"></param>
    /// <param name="defaultLogTxtName"></param>
    public static void LogLocal(object logContent, string defaultLogTxtName = "DefaultLog")
    {
        try
        {
            if (logContent == null)
            {
                return;
            }

#if UNITY_EDITOR

            lock (m_Log)
            {
                var defaultLogPath = Application.dataPath + m_EditorLogPath + defaultLogTxtName + ".txt";
                var streamWriter = File.AppendText(defaultLogPath);
                streamWriter.WriteLine(logContent);
                streamWriter.Flush();
                streamWriter.Close();
            }

#endif
        }
        catch
        {
            throw m_Exception;
        }
    }

    #endregion
}