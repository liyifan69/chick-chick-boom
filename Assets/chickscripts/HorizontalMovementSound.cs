using UnityEngine;

public class HorizontalMovementSound : MonoBehaviour
{
    public AudioClip movingSound;  // 用于存储移动中的声音
    private AudioSource audioSource;  // 用于播放声音的音频源组件
    private Vector3 lastPosition;  // 用于跟踪上一帧的位置

    void Start()
    {
        // 获取物体上的 AudioSource 组件，如果没有就添加一个
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 初始化上一帧的位置
        lastPosition = transform.position;
    }

    void Update()
    {
        // 获取物体的当前位置
        Vector3 currentPosition = transform.position;

        // 在水平方向上检测位移
        if (currentPosition.x != lastPosition.x)
        {
            // 播放声音
            if (movingSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = movingSound;
                audioSource.Play();
            }
        }
        else
        {
            // 位移停止时停止声音
            audioSource.Stop();
        }

        // 更新上一帧的位置
        lastPosition = currentPosition;
    }
}
