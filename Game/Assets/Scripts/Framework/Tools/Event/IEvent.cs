public interface IEvent
{
    void Initialize();

    void ReAddAllListeners();

    void RemoveAllListeners();

    void Dispose();
}