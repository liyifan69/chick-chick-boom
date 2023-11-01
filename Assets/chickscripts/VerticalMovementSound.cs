using UnityEngine;

public class VerticalMovementSound : MonoBehaviour
{
    public AudioClip movementSound;  // 用于存储位移时的声音剪辑
    private AudioSource audioSource;  // 用于播放声音的音频源组件
    private Vector3 lastPosition;  // 上一帧的位置

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
        // 计算当前帧与上一帧的位移
        float verticalMovement = transform.position.y - lastPosition.y;

        // 如果竖直方向有位移
        if (verticalMovement != 0f)
        {
            // 检查位移中是否有声音资源
            if (movementSound != null && !audioSource.isPlaying)
            {
                // 设置音频源的剪辑为位移声音
                audioSource.clip = movementSound;
                // 播放声音
                audioSource.Play();
            }
        }
        else
        {
            // 位移停止时停止音频
            audioSource.Stop();
        }

        // 更新上一帧的位置
        lastPosition = transform.position;
    }
}