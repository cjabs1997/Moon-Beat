// Pulled from the Richard Fine Unite 2016 talk on ScriptableObjects
//
// Based on Richard Fine's Unite Talk about Scriptable Objects
//
// https://www.youtube.com/watch?v=6vmRwLYWNRo&t=2557s
// https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/

using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource source);
}