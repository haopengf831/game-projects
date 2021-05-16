using System;

public interface ILauncher
{
    /// <summary>
    /// 启动
    /// </summary>
    void Launch(Action<bool> action);
}