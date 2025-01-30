/****************************************************
    文件：CameraFollow.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // 船的 Transform
    public Vector2 minBounds;         // 地图的左下角坐标
    public Vector2 maxBounds;         // 地图的右上角坐标
    public Vector3 offset = new Vector3(0, 0, -10); // 相机与船的偏移量

    private Camera cam;
    private float cameraHalfHeight;
    private float cameraHalfWidth;

    void Start()
    {
        cam = Camera.main;

        // 计算相机的可视范围（适用于正交相机）
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }
    private void Update()
    {
        
    }
    void LateUpdate()
    {
        if (target == null)
            return;

        // 获取目标位置加上偏移
        Vector3 desiredPosition = target.position + offset;

        // 限制相机的位置范围
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        // 更新相机位置
        transform.position = new Vector3(clampedX, clampedY, desiredPosition.z);
    }
}
