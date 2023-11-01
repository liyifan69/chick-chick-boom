using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectToGenerate; // 要生成的物体

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            // 发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // 如果射线与物体相交
            {
                // 在交点位置生成物体
                Instantiate(objectToGenerate, hit.point, Quaternion.identity);
            }
        }
    }
}
