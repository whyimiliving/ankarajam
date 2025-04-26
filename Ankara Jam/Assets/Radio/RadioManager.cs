using System;
using System.Linq;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioClip[] myAudioClips;
    public AudioSource myAudioSource;
    public float soundLevel = 1;

    void Start()
    {
        myAudioSource.clip = myAudioClips[0];
        myAudioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DjPutTheMusic(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DjPutTheMusic(false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SetVolume(-1);
        }
        if (Input.GetKey(KeyCode.T))
        {
            SetVolume(1);
        }
    }

    public void SetVolume(float volume)
    {
        soundLevel += (volume/1000);
        soundLevel = Mathf.Clamp(soundLevel, 0f, 1);
        myAudioSource.volume = soundLevel;
    }

    public void DjPutTheMusic(bool isRight)
    {
        var names = myAudioClips.Select(x => x.name).ToArray();
        var currentMusic = Array.IndexOf(names, myAudioSource.clip.name);
        if (isRight)
        {
            currentMusic++;
        }
        else
        {
            currentMusic--;
        }
        if (currentMusic == -1)
        {
            currentMusic = myAudioClips.Length - 1;
        }
        else if (currentMusic == myAudioClips.Length)
        {
            currentMusic = 0;
        }
        myAudioSource.clip = myAudioClips[currentMusic];
        myAudioSource.Play();
    }
}
