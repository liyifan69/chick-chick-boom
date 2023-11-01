using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip collisionSound;  // 用于存储声音剪辑
    private AudioSource audioSource;  // 用于播放声音的音频源组件

    void Start()
    {
        // 获取物体上的 AudioSource 组件，如果没有就添加一个
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 当发生碰撞时触发
    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞中是否有声音资源
        if (collisionSound != null)
        {
            // 设置音频源的剪辑为碰撞声音
            audioSource.clip = collisionSound;
            // 播放声音
            audioSource.Play();
        }
    }
}
 