using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioClip verticalMovingSound;
    public AudioClip horizontalMovingSound;
    private AudioSource audioSource;
    private Vector3 initialPosition;

    private bool isVerticalMoving;
    private bool isHorizontalMoving;

    // colors
    float angryValue = 0.35f;
    float releasePerSecond = 0.1f;




    float deltaMovement = 0.1f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        initialPosition = transform.position;

        isVerticalMoving = false;
        isHorizontalMoving = false;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (!isVerticalMoving && !isHorizontalMoving)
        {
            // 检测竖直移动并播放音频
            if (currentPosition.y != initialPosition.y)
            {
                if (verticalMovingSound != null)
                {
                    audioSource.clip = verticalMovingSound;
                    audioSource.Play();
                    isVerticalMoving = true;
                }
            }
        }

        // 竖直移动停止时停止播放音频
        if (isVerticalMoving && currentPosition.y == initialPosition.y)
        {
            audioSource.Stop();
            isVerticalMoving = false;
            isHorizontalMoving = true; // 启动水平移动检测
        }

        //if (isHorizontalMoving)
        //{
        //    // 检测水平移动并播放音频
        //    if (currentPosition.x - initialPosition.x > deltaMovement)
        //    {
        //        if (horizontalMovingSound != null)
        //        {
        //            audioSource.clip = horizontalMovingSound;
        //            if (!audioSource.isPlaying)
        //            {
        //                audioSource.Play();
        //            }
        //        }
        //    }
        //    else if (audioSource.isPlaying)
        //    {
        //        audioSource.Stop();
        //    }
        //}

        initialPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter");

        if (!audioSource.isPlaying)
        {
            audioSource.clip = horizontalMovingSound;
            audioSource.Play();
        }
        else
        {

        }


        
    }
}
