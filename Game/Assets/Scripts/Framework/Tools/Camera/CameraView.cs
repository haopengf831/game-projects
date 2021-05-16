using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    private Camera m_Camera;

    //距离摄像机8.5米 用黄色表示
    public float UpperDistance = 8.5f;

    //距离摄像机12米 用红色表示
    public float LowerDistance = 12.0f;

    protected virtual void Start()
    {
        if (!m_Camera)
        {
            m_Camera = this.GetComponent<Camera>();
            if (m_Camera == null)
            {
                throw new NoNullAllowedException(nameof(m_Camera));
            }
        }
    }


    protected virtual void Update()
    {
        FindUpperCorners();
        FindLowerCorners();
    }

    protected virtual void FindUpperCorners()
    {
        var corners = m_Camera.FieldOfViewCornersArray(UpperDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], Color.yellow); // UpperLeft -> UpperRight
        Debug.DrawLine(corners[1], corners[3], Color.yellow); // UpperRight -> LowerRight
        Debug.DrawLine(corners[3], corners[2], Color.yellow); // LowerRight -> LowerLeft
        Debug.DrawLine(corners[2], corners[0], Color.yellow); // LowerLeft -> UpperLeft
        Debug.DrawLine(m_Camera.transform.position, m_Camera.CenterViewPosition(UpperDistance), Color.yellow);
    }

    protected virtual void FindLowerCorners()
    {
        var corners = m_Camera.FieldOfViewCornersArray(LowerDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], Color.red);
        Debug.DrawLine(corners[1], corners[3], Color.red);
        Debug.DrawLine(corners[3], corners[2], Color.red);
        Debug.DrawLine(corners[2], corners[0], Color.red);
        Debug.DrawLine(m_Camera.transform.position, m_Camera.CenterViewPosition(LowerDistance), Color.red);
    }
}