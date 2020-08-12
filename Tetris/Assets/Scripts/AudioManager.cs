using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// This is a simple audio manager that lets you assign a set of tracks to an array in the inspector and play them from code.
/// You can play a clip once or on loop. 
/// Call a track using its index in the array, eg: PlayAudioClip(0) or PlayAudioLoop(4)
/// You can also play a random clip from a range in the array, eg: PlayAudioClip(0,2) will play either the 0, 1, or 2 tracks.
/// Stop all audio using StopAllAudio()
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