using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.Events;

public class ModelWalk : MonoBehaviour
{
    private bool m_IsWalk;
    public Animation Animation;
    public float Speed = 2;
    public Transform CameraPoint;
    public ColliderTrigger ColliderTrigger;
    public Collider DoorCollider;

    public UnityEvent WalkStopEvent = new UnityEvent();

    private void Awake()
    {
        if (ColliderTrigger != null)
        {
            ColliderTrigger.TriggerEnter.AddListener(StopWalk);
        }
    }

    public void Walk()
    {
        if (CameraPoint != null)
        {
            var camera3D = Context.GetApplicationContext().GetService<GlobalConfigurator>().CameraRig.MainCamera3d.transform;
            camera3D.position = CameraPoint.position;
            camera3D.rotation = CameraPoint.rotation;
        }

        Animation.SetActive(true);
        Animation.Play("walk");
        m_IsWalk = true;
    }

    public void StopWalk(Collider other)
    {
        if (other != null && DoorCollider != null)
        {
            if (other.gameObject.GetInstanceID() != DoorCollider.gameObject.GetInstanceID())
            {
                return;
            }
        }

        Animation.Play("idle");
        m_IsWalk = false;
        WalkStopEvent?.Invoke();
    }

    private void Update()
    {
        if (m_IsWalk)
        {
            Animation.transform.Translate(Speed * Time.deltaTime * transform.forward, Space.World);
        }
    }
}