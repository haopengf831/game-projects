using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Camera MainCamera3d; //3d主摄像头
    public Camera MainCamera2d; //2d主摄像头

    public void Set3DCameraTransform(Transform location,Transform rotation)
    {
        MainCamera3d.transform.position = location.position;
        MainCamera3d.transform.LookAt(rotation);
    }
}