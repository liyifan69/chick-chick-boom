using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class HandJointAnimalSpawner : MonoBehaviour
{
    [SerializeField] private PlaneDetectionController m_Spawner;

    private float cooldown = 3f;

    private float lastSpawnTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ARPlane>(out var arPlane))
        {
            if (Time.time - lastSpawnTime > cooldown)
            {
                m_Spawner.SpawnObject(transform.position, Quaternion.identity);
                lastSpawnTime = Time.time;
            }
        }
    }
}
