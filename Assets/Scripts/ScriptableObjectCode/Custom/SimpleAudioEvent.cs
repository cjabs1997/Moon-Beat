// Pulled from the Richard Fine Unite 2016 talk on ScriptableObjects
//
// Based on Richard Fine's Unite Talk about Scriptable Objects
//
// https://www.youtube.com/watch?v=6vmRwLYWNRo&t=2557s
// https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SimpleAudioEvent", menuName = "Event/SimpleAudio")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] clips;

    public float minVolume;
    public float maxVolume;

    public float minPitch;
    public float maxPitch;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0) return;

        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(minVolume, maxVolume);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }
}