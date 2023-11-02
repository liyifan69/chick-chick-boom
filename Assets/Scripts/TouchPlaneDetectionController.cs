using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchPlaneDetectionController : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject customObjectToGenerate;
    [SerializeField]
    private GameObject obiPlaneColliderToGenerate;

    public Transform CenterEyePose;

    private List<GameObject> generatedPlanes = new List<GameObject>();
    private List<GameObject> generatedChicken = new();
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private const float MAX_OBJECT_COUNT = 2;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 从屏幕发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // 尝试检测平面
            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose; // 获取第一个检测到的平面

                // 生成新的整体
                GeneratePlane(obiPlaneColliderToGenerate, hitPose.position, hitPose.rotation);
                GenerateChick(customObjectToGenerate, hitPose.position + Vector3.up * 0.5f, hitPose.rotation);
            }
        }
    }

    private void GeneratePlane(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (generatedPlanes.Count >= MAX_OBJECT_COUNT)
        {
            // 删除最早生成的整体
            Destroy(generatedPlanes[0]);
            generatedPlanes.RemoveAt(0);
        }

        // 生成新的整体
        GameObject newObject = Instantiate(prefab, position, rotation);
        generatedPlanes.Add(newObject);
    }

    private void GenerateChick(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (generatedChicken.Count >= MAX_OBJECT_COUNT)
        {
            // 删除最早生成的整体
            Destroy(generatedChicken[0]);
            generatedChicken.RemoveAt(0);
        }

        // 生成新的整体
        GameObject newObject = Instantiate(prefab, position, rotation);

        var playerPosXZ = new Vector3(CenterEyePose.position.x, position.y , CenterEyePose.position.z);

        newObject.transform.LookAt(playerPosXZ);
        generatedChicken.Add(newObject);
    }

    public void SpawnObject(Vector3 position, Quaternion rotation)
    {
        GeneratePlane(obiPlaneColliderToGenerate, position, rotation);
        GenerateChick(customObjectToGenerate, position + Vector3.up * 0.5f, rotation);
    }
}
