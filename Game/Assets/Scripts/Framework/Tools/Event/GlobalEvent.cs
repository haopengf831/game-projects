using UnityEngine;

public class GlobalEvent : MonoBehaviour
{
    public EngineEvent EngineEvent;

    private void Awake()
    {
        if (EngineEvent == null)
        {
            EngineEvent = this.RequireComponent<EngineEvent>();
        }
    }

    private void OnDestroy()
    {
        if (EngineEvent != null)
        {
            EngineEvent.Dispose();
        }
    }
}