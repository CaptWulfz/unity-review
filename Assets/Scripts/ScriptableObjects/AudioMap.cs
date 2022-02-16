using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioMap.asset", menuName = "Audio/AudioMap")]
public class AudioMap : ScriptableObject
{
    [System.Serializable]
    public class AudioEntry
    {
        public string key;
        public AudioClip source;
    }

    public AudioEntry[] sfx;
    public AudioEntry[] music;
}
