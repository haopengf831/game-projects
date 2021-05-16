using System;
using System.Threading;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using IAsyncResult = Loxodon.Framework.Asynchronous.IAsyncResult;

public interface IThreadLoopExecutor
{
    IAsyncResult Execute(Action action);
}

/// <summary>
/// 线程循环执行器，可开启一个线程专门跑子线程Update，直到Cancel为止
/// </summary>
public class ThreadLoopExecutor : AbstractExecutor, IThreadLoopExecutor
{
    public virtual IAsyncResult Execute(Action action)
    {
        var result = new AsyncResult(true);
        Executors.RunAsyncNoReturn(() =>
        {
            try
            {
                while (!result.IsCancellationRequested)
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (ThreadAbortException threadAbortException)
                    {
                        break;
                    }
                    catch (Exception exception)
                    {
                        break;
                    }
                }

                result.SetResult();
                result.SetCancelled();
            }
            catch (Exception e)
            {
                result.SetException(e);
            }
        });
        return result;
    }
}