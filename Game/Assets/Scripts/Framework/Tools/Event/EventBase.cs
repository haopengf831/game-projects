using System;

public abstract class EventBase : IEvent, IDisposable
{
    private bool m_Disposed;

    ~EventBase()
    {
        Dispose(false);
    }

    public virtual void Initialize()
    {
        try
        {
            ReAddAllListeners();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public abstract void ReAddAllListeners();

    public abstract void RemoveAllListeners();

    public virtual void Dispose()
    {
        try
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        try
        {
            if (m_Disposed)
            {
                return;
            }

            if (disposing)
            {
                RemoveAllListeners();
            }

            m_Disposed = true;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}