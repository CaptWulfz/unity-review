using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private const string AUDIO_MAP_PATH = "AssetFiles/AudioMap";
    [SerializeField] AudioMap audioMap;
    [SerializeField] AudioSource playerSource;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;

    private Dictionary<string, AudioClip> sfxClips;
    private Dictionary<string, AudioClip> musicClips;

    public void Awake() {
        Initialize();
    }

#region Initialization
    public void Initialize(){

        this.audioMap = Resources.Load<AudioMap>(AUDIO_MAP_PATH);

        if(sfxSources == null)
            this.sfxSources = new Dictionary<string, AudioSource>();

        if(musicSources == null)
            this.musicSources = new Dictionary<string, AudioSource>();

        if(sfxClips == null)
            this.sfxClips = new Dictionary<string, AudioClip>();

        if(musicClips == null)
            this.musicClips = new Dictionary<string, AudioClip>();

        StartCoroutine(WaitForAudioMap());

    }

private IEnumerator WaitForAudioMap(){

    yield return new WaitUntil(() => {return this.audioMap != null;});

        sfxClips.Clear();
            foreach(AudioMap.AudioEntry entry in this.audioMap.SFX)
                this.sfxClips.Add(entry.key, entry.source);

        musicClips.Clear();
            foreach(AudioMap.AudioEntry entry in this.audioMap.Music)
                this.musicClips.Add(entry.key, entry.source);
}

public void PlayAudioOnPlayer(string audioClipKey, string clipKey){

    Dictionary<string, AudioClip> dict = GetAudioDict(audioClipKey);
    AudioClip clip = dict[clipKey];
    this.playerSource.clip = clip;
    this.playerSource.Play();

}

#endregion

#region Functions
public void RegisterAudioToSource(string audioGroupKey, string sourceKey, AudioSource source){

    Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

    if (dict.ContainsKey(sourceKey))
        dict[sourceKey] = source;
    else
        dict.Add(sourceKey, source);

}

public void UnregisterAudioSource(string audioGroupKey, string sourceKey){

    Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

    if(dict.ContainsKey(sourceKey))
        dict.Remove(sourceKey);
}

public void PlayAudio(string audioGroupKey, string sourceKey, string clipName){
    
    AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
    Dictionary<string, AudioClip> dict = GetAudioDict(audioGroupKey);

    source.clip = dict[clipName];
    source.Play();
}
#endregion

#region Helpers

private Dictionary<string, AudioSource> GetAudioGroup(string audioGroupKey){

    Dictionary<string, AudioSource> dict = null;

    switch(audioGroupKey){
        case AudioKeys.SFX:
            dict = this.sfxSources;
            break;
        
        case AudioKeys.MUSIC:
            dict = this.musicSources;
            break;
    }

    return dict;
}

private AudioSource GetAudioSource(string audioGroupKey, string audioSourceKey){

    AudioSource source = null;

    Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

    if(dict.ContainsKey(audioSourceKey))
        source = dict[audioSourceKey];

    return source;
}

private Dictionary<string, AudioClip> GetAudioDict(string audioClipKey){

    Dictionary<string, AudioClip> dict = null;

    switch (audioClipKey)
    {
        case AudioKeys.SFX:
            dict = this.sfxClips;
            break;
        case AudioKeys.MUSIC:
            dict = this.musicClips;
            break;
    }

        return dict;
    }

#endregion

}

public class AudioKeys{
    public const string SFX = "SFX";
    public const string MUSIC = "MUSIC";
}

public class SFXKeys{
    public const string GIB = "gib";
    public const string STOP = "stop";
    public const string SPRAY = "spray";
    public const string STEP1 = "step1";
    public const string STEP2 = "step2";
    public const string STEP3 = "step3";
    public const string STEP4 = "step4";
}
public class MusicKeys{
    public const string CHILL = "Sakura Chill";
}