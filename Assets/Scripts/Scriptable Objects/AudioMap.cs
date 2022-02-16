using UnityEngine;

// for right click -> Database -> AudioMap
[CreateAssetMenu(fileName = "AudioMap.asset", menuName = "Database/Audio Map")]
public class AudioMap : ScriptableObject
{
    [System.Serializable]
    // JSON AudioEntry
    public class AudioEntry
    {
        public string key;
        public AudioClip source;
    }

    public AudioEntry[] SFX;
    public AudioEntry[] Music;
}
