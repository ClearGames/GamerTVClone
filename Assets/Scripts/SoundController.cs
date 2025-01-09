using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        // 재생
        BGM.Play();

        // 뮤트 true >> 음소거
        BGM.mute = true;

        // 루프 true >> 반복
        BGM.loop = true;

        // 자동 재생 true >> 자동재생
        BGM.playOnAwake = true;

        // 정지
        BGM.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
