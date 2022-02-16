using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    const string AUDIO_MAP_PATH = "AssetFiles/AudioMap";

    [SerializeField] AudioMap audioMap;

    Dictionary<string, AudioSource> sfxSources;
    Dictionary<string, AudioSource> musicSources;

    Dictionary<string, AudioClip> sfxClips;
    Dictionary<string, AudioClip> musicClips;

    void Awake()
    {
        Initialize();
    }

    #region Initialisation
    public void Initialize()
    {
        this.audioMap = Resources.Load<AudioMap>(AUDIO_MAP_PATH);

        if (sfxSources == null)
            this.sfxSources = new Dictionary<string, AudioSource>();
        if (musicSources == null)
            this.musicSources = new Dictionary<string, AudioSource>();

        if (sfxClips == null)
            this.sfxClips = new Dictionary<string, AudioClip>();
        if (musicClips == null)
            this.musicClips = new Dictionary<string, AudioClip>();

        StartCoroutine(WaitForAudioMap());
    }

    IEnumerator WaitForAudioMap()
    {
        yield return new WaitUntil(() => { return this.audioMap != null; });

        sfxClips.Clear();

        foreach (AudioMap.AudioEntry entry in this.audioMap.sfx)
        {
            this.sfxClips.Add(entry.key, entry.source);
        }

        musicClips.Clear();

        foreach (AudioMap.AudioEntry entry in this.audioMap.music)
        {
            this.musicClips.Add(entry.key, entry.source);
        }
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

    #region Helper Functions

    Dictionary<string, AudioSource> GetAudioGroup(string audioGroupKey)
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

    Dictionary<string, AudioClip> GetAudioDict(string audioClipKey)
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

    AudioSource GetAudioSource(string audioGroupKey, string audioSourceKey)
    {
        AudioSource audioSource = null;
        Dictionary<string, AudioSource> dict = GetAudioGroup(audioGroupKey);

        if (dict.ContainsKey(audioSourceKey))
            audioSource = dict[audioSourceKey];

        return audioSource;
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
    public const string YEE = "Yee";
    public const string SMALLAHH = "SmallAHH";
    public const string LOUDAHH = "LoudAHH";
}

public class MusicKeys
{
    public const string BGM = "Big Enough";
}