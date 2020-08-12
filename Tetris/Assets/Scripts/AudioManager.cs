using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// INTENT: This script is a basic audio manager that allows you to play a BGM track on start as well as SFX clips
/// directly from a script.
/// USAGE: Place this script on the AudioManager GameObject. Attach two audio sources to the AudioManager & connect
/// the references in the inspector. Add SFX clips & BGM tracks through the inspector & call the appropriate function
/// to play them from a script.
/// </summary>
public class AudioManager : MonoBehaviour
{
    //Public variables
    public AudioSource _sfxAudioSource;
    [Range(0, 1)] public float sfxVolume = 0.5f;
    public List<AudioClip> audioClips;
    public AudioSource _BGMAudioSource;
    [Range(0, 1)] public float BGMVolume = 0.2f;
    public List<AudioClip> BGMTracks;
    
    void Start()
    {
        //Play a random BGM track if the list is not empty
        if (BGMTracks != null)
        {
            PlayBGM(0, BGMTracks.Count - 1, BGMVolume);
        }
    }
    
    /// <summary>
    /// Plays a BGM track specified by the index in the list of BGM tracks.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="volume"></param>
    public void PlayBGM(int i, float volume=1f)
    {
        _BGMAudioSource.loop = true;
        _BGMAudioSource.clip = BGMTracks[i];
        _BGMAudioSource.volume = volume;
        _BGMAudioSource.Play();
    }
    
    /// <summary>
    /// Plays a random BGM track specified by the index range from the list of BGM tracks.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="volume"></param>
    public void PlayBGM(int i, int j, float volume = 1f)
    {
        int k = Random.Range(i, j + 1);
        _BGMAudioSource.loop = true;
        _BGMAudioSource.clip = BGMTracks[k];
        _BGMAudioSource.volume = volume;
        _BGMAudioSource.Play();
    }

    /// <summary>
    /// Plays an audio clip specified by the index in the list of audio clips.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="volume"></param>
    public void PlayAudioClip(int i, float volume=1f)
    {
        _sfxAudioSource.PlayOneShot(audioClips[i],volume);
    }
       
    /// <summary>
    /// Plays a random audio clip specified by the index range from the list of audio clips.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="volume"></param>
    public void PlayAudioClip(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        _sfxAudioSource.PlayOneShot(audioClips[k],volume);
    }
    
    /// <summary>
    /// Loops an audio clip specified by the index in the list of audio clips.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="volume"></param>
    public void PlayAudioLoop(int i, float volume=1f)
    {
        _sfxAudioSource.loop = true;
        _sfxAudioSource.clip = audioClips[i];
        _sfxAudioSource.volume = volume;
        _sfxAudioSource.Play();
    }
       
    /// <summary>
    /// Loops a random audio clip specified by the index range from the list of audio clips.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="volume"></param>
    public void PlayAudioLoop(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        _sfxAudioSource.loop = true;
        _sfxAudioSource.clip = audioClips[k];
        _sfxAudioSource.volume = volume;
        _sfxAudioSource.Play();
    }

    /// <summary>
    /// Stops all currently playing BGM & audio clips.
    /// </summary>
    public void StopAllAudio()
    {
        _BGMAudioSource.Stop();
        _sfxAudioSource.Stop();
    }
}