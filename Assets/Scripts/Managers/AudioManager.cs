using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private const string AUDIO_MAP_PATH = "Asset Files/AudioMap";

    [SerializeField] AudioMap audioMap;
    //[SerializeField] AudioSource playerSource;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;

    private Dictionary<string, AudioClip> sfx;
    private Dictionary<string, AudioClip> music;

    public void Awake()
    {
        Initialize();
    }

    #region Initialization
    public void Initialize()
    {

        this.audioMap = Resources.Load<AudioMap>(AUDIO_MAP_PATH);

        if (this.sfxSources == null)
            this.sfxSources = new Dictionary<string, AudioSource>();

        if (this.musicSources == null)
            this.musicSources = new Dictionary<string, AudioSource>();

        if (this.sfx == null)
            this.sfx = new Dictionary<string, AudioClip>();

        if (this.music == null)
            this.music = new Dictionary<string, AudioClip>();

        // start Coroutine
        StartCoroutine(WaitForAudioMap());
    }

    // ASYNC -> para by order yung codes 
    private IEnumerator WaitForAudioMap() 
    {
        yield return new WaitUntil(() => { return this.audioMap != null; });

        sfx.Clear();
        foreach (AudioMap.AudioEntry entry in this.audioMap.SFX)
        {
            this.sfx.Add(entry.key, entry.source);
        }

        music.Clear();
        foreach (AudioMap.AudioEntry entry in this.audioMap.Music)
        {
            this.music.Add(entry.key, entry.source);
        }
    }

    //public void PlayAudioOnPlayer(string audioClipKey, string clipKey)
    //{
    //    Dictionary<string, AudioClip> dict = GetAudioDict(audioClipKey);
    //    AudioClip clip = dict[clipKey];
    //    this.playerSource.clip = clip;
    //    this.playerSource.Play();
    //}

    #endregion

    #region Functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="audioGroupKey"> SFX OR MUSIC</param>
    /// <param name="sourceKey"> </param>
    /// <param name="source"></param>
    /// <returns></returns>
    /// 

    public void RegisterAudioSource(string audioGroupKey, string sourceKey, AudioSource source)
    {
        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(sourceKey))
            dict[sourceKey] = source;
        else
            dict.Add(sourceKey, source);
    }
    public void PlayAudio(string audioGroupKey, string sourceKey, string clipName)
    {
        AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
        Dictionary<string, AudioClip> dict = GetAudioDict(audioGroupKey);

        source.clip = dict[clipName];
        source.Play();

    }   

    public void UnregisterAudioSource(string audioGroupKey, string sourceKey)
    {
        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(sourceKey))
            dict.Remove(sourceKey);
    }

    #endregion


    #region Helpers
    private Dictionary<string, AudioSource> GetAudioGroup(string audioGroupKey)
    {
        Dictionary<string, AudioSource> dict = null;

        switch (audioGroupKey)
        {
            case AudioKeys.SFX:
                dict = this.sfxSources;
                break; 
            case AudioKeys.MUSIC:
                dict = this.musicSources;
                break;
        }

        return dict;
    }


    private AudioSource GetAudioSource(string audioGroupKey, string audioSourceKey)
    {
        AudioSource source = null;

        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(audioSourceKey))
            source = dict[audioSourceKey];

        return source;
    }

    private Dictionary<string, AudioClip> GetAudioDict(string audioClipKey)
    {
        Dictionary<string, AudioClip> dict = null;

        switch (audioClipKey)
        {
            case AudioKeys.SFX:
                dict = this.sfx;
                break;
            case AudioKeys.MUSIC:
                dict = this.music;
                break;
        }
        return dict;

    }

    #endregion

}

public class AudioKeys
{
    public const string SFX = "SFX";
    public const string MUSIC = "MUSIC";
}

public class SFXKeys
{
    public const string COIN = "coin";
    public const string JUMP = "jump";
    public const string DOWN = "down";
    public const string RISE = "rise";
}

public class MusicKeys
{
    public const string CHILL = "chill";
}