using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//This is a simple audio manager that lets you assign a set of tracks to an array in the inspector and play them from code.
//You can play a clip once or on loop. 
//Call a track using its index in the array, eg: PlayAudioClip(0) or PlayAudioLoop(4)
//You can also play a random clip from a range in the array, eg: PlayAudioClip(0,2) will play either the 0, 1, or 2 tracks.
//Stop all audio using StopAllAudio()

//Original script by @srgovindan
public class AudioManager : MonoBehaviour
{
    //Public variables
    public AudioSource _sfxAudioSource;
    [Range(0, 1)] public float sfxVolume = 0.5f;
    public List<AudioClip> audioClips;
    public AudioSource _BGMAudioSource;
    [Range(0, 1)] public float BGMVolume = 0.2f;
    public List<AudioClip> BGMClips;
    
    void Start()
    {
        //Play a random BGM track if the list is not empty
        if (BGMClips != null)
        {
            PlayBGM(0, BGMClips.Count - 1, BGMVolume);
        }
    }
    
    public void PlayBGM(int i, float volume=1f)
    {
        _BGMAudioSource.loop = true;
        _BGMAudioSource.clip = BGMClips[i];
        _BGMAudioSource.volume = volume;
        _BGMAudioSource.Play();
    }

    //Random clip from range
    public void PlayBGM(int i, int j, float volume = 1f)
    {
        int k = Random.Range(i, j + 1);
        _BGMAudioSource.loop = true;
        _BGMAudioSource.clip = BGMClips[k];
        _BGMAudioSource.volume = volume;
        _BGMAudioSource.Play();
    }

    
    public void PlayAudioClip(int i, float volume=1f)
    {
        //Debug.Log("Playing Audio Clip " + i);
        _sfxAudioSource.PlayOneShot(audioClips[i],volume);
    }
       
    //Random clip from range
    public void PlayAudioClip(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        //Debug.Log("Playing Audio Clip " + i);
        _sfxAudioSource.PlayOneShot(audioClips[k],volume);
    }
    
    
    public void PlayAudioLoop(int i, float volume=1f)
    {
        //Debug.Log("Playing Audio Clip " + i);
        _sfxAudioSource.loop = true;
        _sfxAudioSource.clip = audioClips[i];
        _sfxAudioSource.volume = volume;
        _sfxAudioSource.Play();
    }
       
    //Random clip from range
    public void PlayAudioLoop(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        //Debug.Log("Playing Audio Clip " + i);
        _sfxAudioSource.loop = true;
        _sfxAudioSource.clip = audioClips[k];
        _sfxAudioSource.volume = volume;
        _sfxAudioSource.Play();
    }

    
    public void StopAllAudio()
    {
        _BGMAudioSource.Stop();
        _sfxAudioSource.Stop();
    }
}
