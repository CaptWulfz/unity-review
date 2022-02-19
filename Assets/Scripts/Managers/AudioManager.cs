using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    // File path of the Audio Map Asset File
    private const string AUDIO_MAP_PATH = "AssetFiles/AudioMap";

    [SerializeField] AudioMap audioMap;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;

    private Dictionary<string, AudioClip> sfxClips;
    private Dictionary<string, AudioClip> musicClips;

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

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

        if (this.sfxClips == null)
            this.sfxClips = new Dictionary<string, AudioClip>();

        if (this.musicClips == null)
            this.musicClips = new Dictionary<string, AudioClip>();

        StartCoroutine(WaitForAudioMap());
    }

    private IEnumerator WaitForAudioMap()
    {
        yield return new WaitUntil(() => { return this.audioMap != null; });

        sfxClips.Clear();
        foreach (AudioMap.AudioEntry entry in this.audioMap.SFX)
        {
            this.sfxClips.Add(entry.key, entry.source);
        }

        musicClips.Clear();
        foreach (AudioMap.AudioEntry entry in this.audioMap.Music)
        {
            this.musicClips.Add(entry.key, entry.source);
        }

        this.isDone = true;
    }
    #endregion

    #region Functions
    public void RegisterAudioSource(string audioGroupKey, string sourceKey, AudioSource source)
    {
        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(sourceKey))
            dict[sourceKey] = source;
        else
            dict.Add(sourceKey, source);
    }

    public void UnregisterAudioSource(string audioGroupKey, string sourceKey)
    {
        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(sourceKey))
            dict.Remove(sourceKey);
    }

    public void PlayAudio(string audioGroupKey, string sourceKey, string clipName)
    {
        AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
        Dictionary<string, AudioClip> dict = GetAudioDict(audioGroupKey);

        source.clip = dict[clipName];
        source.Play();
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

public class AudioKeys
{
    public const string SFX = "SFX";
    public const string MUSIC = "MUSIC";
}

public class SFXKeys
{
    public const string TOM = "tom";
    public const string NANI = "nani";
    public const string ULTRA = "ultra";
    public const string DBZ = "dbz";
}

public class MusicKeys
{
    public const string CHILL = "chill";
}
