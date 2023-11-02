using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnCollision : MonoBehaviour
{
    public Material originalMaterial;
    public Material yellowMaterial;
    public Material orangeMaterial;
    public Material redMaterial;

    public GameObject vfx1; // 物体2的预制体，同时带有特效组件
    public GameObject vfx1Instance; // 物体2的预制体，同时带有特效组件
    public GameObject vfx2; // 物体3的预制体，同时带有特效组件
    public GameObject vfx2Instance; // 物体3的预制体，同时带有特效组件

    public MeshRenderer ChickMeshRenderer;
    private int collisionCount = 0;
    private float lastCollisionTime = 0f;
    public float colorChangeInterval = 1.0f; // 颜色更改间隔（秒）

    enum State {
    origin,
    yellow,
    orange,
    red
    }

    State chickState = State.origin;

    private void Start()
    {
        ChickMeshRenderer.material = originalMaterial; // 设置初始材质
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastCollisionTime > colorChangeInterval)
        {
            collisionCount++;
            lastCollisionTime = Time.time;
        }

        if (collisionCount == 1 && chickState == State.origin)
        {
            ChickMeshRenderer.material = yellowMaterial;
            chickState = State.yellow;

        }
        else if (collisionCount == 2 && chickState == State.yellow)
        {
            ChickMeshRenderer.material = orangeMaterial;
            chickState = State.orange;


            // 在变成橙色时生成物体2（带有特效组件）
            if (vfx1Instance != null)
            {
                // nothing
            }
            else
            {
                vfx1Instance = Instantiate(vfx1, transform.position + Vector3.up * 0.08f, transform.rotation);
                vfx1Instance.transform.parent = transform;
                // 启动物体3的特效
                vfx1Instance.GetComponent<ParticleSystem>().Simulate(1);
                vfx1Instance.GetComponent<ParticleSystem>().Play();
                var particleSystems3 = vfx1Instance.GetComponentsInChildren<ParticleSystem>();
                foreach (var ps in particleSystems3)
                {
                    ps.Simulate(1);
                    ps.Play();
                }
            }

        }
        else if (collisionCount == 3 && chickState == State.orange)
        {
            ChickMeshRenderer.material = redMaterial;
            chickState = State.red;


            // 在变成红色时生成物体3（带有特效组件）
            if (vfx2Instance != null)
            {
                // nothing
            }
            else
            { 
                Destroy(vfx1Instance.gameObject);
                vfx2Instance = Instantiate(vfx2, transform.position + Vector3.up * 0.08f, transform.rotation);
                vfx2Instance.transform.parent = transform;

                // 启动物体3的特效
                vfx2Instance.GetComponent<ParticleSystem>().Simulate(1);
                vfx2Instance.GetComponent<ParticleSystem>().Play();
                var particleSystems3 = vfx2Instance.GetComponentsInChildren<ParticleSystem>();
                foreach (var ps in particleSystems3)
                {
                    ps.Simulate(1);
                    ps.Play();
                }
            }

        }
    }

    private void Update()
    {
        // 如果距离上一次碰撞已经超过 colorChangeInterval，重置颜色和计数
        if (collisionCount > 0 && Time.time - lastCollisionTime > colorChangeInterval * 3)
        {
            collisionCount = 0;
            ChickMeshRenderer.material = originalMaterial;
            chickState = State.origin;

            Destroy(vfx1Instance.gameObject);
            Destroy(vfx2Instance.gameObject);


        }
    }
}
