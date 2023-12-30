using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

    public AudioSource Steps;
    public AudioSource ShootBullet;

    public void StepsAudio(float speed)
    {
        if (speed != 0)
        {
            if (!Steps.isPlaying)
            {
                Steps.Play();
            }
        }
    }
}
