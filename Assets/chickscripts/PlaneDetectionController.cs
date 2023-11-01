using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetectionController : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject customObjectToGenerate;
    [SerializeField]
    private GameObject obiPlaneColliderToGenerate;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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

                // 在平面上生成物体
                Instantiate(customObjectToGenerate, hitPose.position + new Vector3(0,0.25f,0), hitPose.rotation);
                Instantiate(obiPlaneColliderToGenerate, hitPose.position, hitPose.rotation);
            }
        }
    }
}

