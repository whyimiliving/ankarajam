﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// Creates new audiosource with specified settings.
/// </summary>
[Icon("Assets/Realistic Car Controller Pro/Resources/Editor Icons/RCCP_EditorIcon_Component.png")]
public class RCCP_AudioSource {

    /// <summary>
    /// Creates new audiosource with specified settings.
    /// </summary>
    public static AudioSource NewAudioSource(AudioMixerGroup audioMixer, GameObject go, string audioName, float minDistance, float maxDistance, float volume, AudioClip audioClip, bool loop, bool playNow, bool destroyAfterFinished) {

        GameObject audioSourceObject = new GameObject(audioName);

        if (go.transform.Find("All Audio Sources")) {

            audioSourceObject.transform.SetParent(go.transform.Find("All Audio Sources"));

        } else {

            GameObject allAudioSources = new GameObject("All Audio Sources");
            allAudioSources.transform.SetParent(go.transform, false);
            audioSourceObject.transform.SetParent(allAudioSources.transform, false);

        }

        audioSourceObject.transform.SetPositionAndRotation(go.transform.position, go.transform.rotation);

        audioSourceObject.AddComponent<AudioSource>();
        AudioSource source = audioSourceObject.GetComponent<AudioSource>();

        if (audioMixer)
            source.outputAudioMixerGroup = audioMixer;

        //audioSource.GetComponent<AudioSource>().priority =1;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.volume = volume;
        source.clip = audioClip;
        source.loop = loop;
        source.dopplerLevel = .5f;
        source.ignoreListenerPause = false;
        source.ignoreListenerVolume = false;
        source.rolloffMode = AudioRolloffMode.Logarithmic;

        if (minDistance == 0 && maxDistance == 0)
            source.spatialBlend = 0f;
        else
            source.spatialBlend = 1f;

        if (playNow) {

            source.playOnAwake = true;
            source.Play();

        } else {

            source.playOnAwake = false;

        }

        if (destroyAfterFinished) {

            if (audioClip)
                Object.Destroy(audioSourceObject, audioClip.length);
            else
                Object.Destroy(audioSourceObject);

        }

        return source;

    }

    /// <summary>
    /// Creates new audiosource with specified settings.
    /// </summary>
    public static AudioSource NewAudioSource(GameObject go, string audioName, float minDistance, float maxDistance, float volume, AudioClip audioClip, bool loop, bool playNow, bool destroyAfterFinished) {

        GameObject audioSourceObject = new GameObject(audioName);

        if (go.transform.Find("All Audio Sources")) {

            audioSourceObject.transform.SetParent(go.transform.Find("All Audio Sources"));

        } else {

            GameObject allAudioSources = new GameObject("All Audio Sources");
            allAudioSources.transform.SetParent(go.transform, false);
            audioSourceObject.transform.SetParent(allAudioSources.transform, false);

        }

        audioSourceObject.transform.SetPositionAndRotation(go.transform.position, go.transform.rotation);

        audioSourceObject.AddComponent<AudioSource>();
        AudioSource source = audioSourceObject.GetComponent<AudioSource>();

        //audioSource.GetComponent<AudioSource>().priority =1;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.volume = volume;
        source.clip = audioClip;
        source.loop = loop;
        source.dopplerLevel = .5f;
        source.ignoreListenerPause = false;
        source.ignoreListenerVolume = false;
        source.rolloffMode = AudioRolloffMode.Logarithmic;

        if (minDistance == 0 && maxDistance == 0)
            source.spatialBlend = 0f;
        else
            source.spatialBlend = 1f;

        if (playNow) {

            source.playOnAwake = true;
            source.Play();

        } else {

            source.playOnAwake = false;

        }

        if (destroyAfterFinished) {

            if (audioClip)
                Object.Destroy(audioSourceObject, audioClip.length);
            else
                Object.Destroy(audioSourceObject);

        }

        return source;

    }

    /// <summary>
    /// Adds High Pass Filter to audiosource. Used for turbo.
    /// </summary>
    public static void NewHighPassFilter(AudioSource source, float freq, int level) {

        if (source == null)
            return;

        AudioHighPassFilter highFilter = source.gameObject.AddComponent<AudioHighPassFilter>();
        highFilter.cutoffFrequency = freq;
        highFilter.highpassResonanceQ = level;

    }

    /// <summary>
    /// Adds Low Pass Filter to audiosource. Used for engine off sounds.
    /// </summary>
    public static void NewLowPassFilter(AudioSource source, float freq) {

        if (source == null)
            return;

        AudioLowPassFilter lowFilter = source.gameObject.AddComponent<AudioLowPassFilter>();
        lowFilter.cutoffFrequency = freq;

    }

}
