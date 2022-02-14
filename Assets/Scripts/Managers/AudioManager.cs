using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioManager : Singleton<AudioManager>
{
    // File path of the Audio Map Asset File
    private const string AUDIO_MAP_PATH = "AssetFiles/AudioMap";

    [SerializeField] AudioMap audioMap;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;

    private Dictionary<string, AudioClip> sfx;
    private Dictionary<string, AudioClip> music;

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

    #region Initialization
    public void Initialize()
    {
        this.audioMap = Resources.Load<AudioMap>(AUDIO_MAP_PATH);

        if (sfxSources == null)
            sfxSources = new Dictionary<string, AudioSource>();

        if (musicSources == null)
            musicSources = new Dictionary<string, AudioSource>();

        if (sfx == null)
            sfx = new Dictionary<string, AudioClip>();

        if (music == null)
            music = new Dictionary<string, AudioClip>();

        StartCoroutine(WaitForAudioMap());
    }

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

        this.isDone = true;
    }
    #endregion

    #region Functions
    /// <summary>
    /// Registers a specific Audio Source to an Audio Group given their respective keys
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="sourceKey">Name of the Audio Source. Must be Unique</param>
    /// <param name="source">The Audio Source File to be registered to an Audio Group</param>
    public void RegisterAudioSource(string audioGroupKey, string sourceKey, AudioSource source)
    {
        Dictionary<string, AudioSource> audioGroup = GetAudioGroup(audioGroupKey);

        if (audioGroup.ContainsKey(sourceKey))
            audioGroup[sourceKey] = source;
        else
            audioGroup.Add(sourceKey, source);
    }

    /// <summary>
    /// Plays an Audio File on a specific Audio Source from an Audio Group given their respective keys
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="sourceKey">Name of the Audio Source</param>
    /// <param name="clipName">Name of the Audio Clip to be played</param>
    public void PlayAudio(string audioGroupKey, string sourceKey, string clipName)
    {
        AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
        Dictionary<string, AudioClip> dict = GetAudioDict(audioGroupKey);

        source.clip = dict[clipName];
        source.Play();
    }

    /// <summary>
    /// Sets the Volume of a Certain Audio Group based on the given key
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="value">New volume value to be set. From 0.0 to 1.0</param>
    public void SetAudioGroupVolume(string audioGroupKey, float value)
    {
        Dictionary<string, AudioSource> audioGroup = GetAudioGroup(audioGroupKey);

        foreach (KeyValuePair<string, AudioSource> kvp in audioGroup)
        {
            kvp.Value.volume = value;
        }
    }

    /// <summary>
    /// Sets the Volume of a specific Audio Source from an Audio Group given their respective keys
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="sourceKey">Name of the Audio Source</param>
    /// <param name="value">New Volume value to be set. From 0.0 to 1.0</param>
    public void SetAudioSourceVolume(string audioGroupKey, string sourceKey, float value)
    {
        AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
        source.volume = value;
    }

    /// <summary>
    /// Sets the loop state of an Audio Group based on the given key
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="value">New Loop state value to be set. true or false</param>
    public void ToggleAudioGroupLoop(string audioGroupKey, bool value)
    {
        Dictionary<string, AudioSource> audioGroup = GetAudioGroup(audioGroupKey);

        foreach (KeyValuePair<string, AudioSource> kvp in audioGroup)
        {
            kvp.Value.loop = value;
        }
    }

    /// <summary>
    /// Sets the loop state of a specific Audio Source from an Audio Group given their respective keys
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="sourceKey">Name of the Audio Source</param>
    /// <param name="value">New Loop state value to be set. true or false</param>
    public void ToggleAudioSourceLoop(string audioGroupKey, string sourceKey, bool value)
    {
        AudioSource source = GetAudioSource(audioGroupKey, sourceKey);
        source.loop = value;
    }
    #endregion

    #region Helpers
    /// <summary>
    /// Gets an Audio Group based on the given key
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <returns></returns>
    private Dictionary<string, AudioSource> GetAudioGroup(string audioGroupKey)
    {
        Dictionary<string, AudioSource> audioGroup = null;

        switch (audioGroupKey)
        {
            case AudioKeys.MUSIC:
                audioGroup = this.musicSources;
                break;
            case AudioKeys.SFX:
                audioGroup = this.sfxSources;
                break;
        }

        return audioGroup;
    }

    /// <summary>
    /// Gets a specific Audio Source from an Audio Group given their respective keys
    /// </summary>
    /// <param name="audioGroupKey">Name of the Audio Group</param>
    /// <param name="sourceKey">Name of the Audio Source</param>
    /// <returns></returns>
    private AudioSource GetAudioSource(string audioGroupKey, string sourceKey)
    {
        AudioSource source = null;

        Dictionary<string, AudioSource> audioGroup = GetAudioGroup(audioGroupKey);

        if (audioGroup.ContainsKey(sourceKey))
            source = audioGroup[sourceKey];

        return source;
    }

    /// <summary>
    /// Gets the specific Audio Clip Group based on the given key
    /// </summary>
    /// <param name="audioClipKey">Name of the Audio Clip Group</param>
    /// <returns></returns>
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
    
}

public class MusicKeys
{
    public const string MAIN_THEME = "chill";
}
