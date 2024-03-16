using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class SoundManager : MonoBehaviour
{
    //NOTE: Value of scale refers to if the sound source has a predetermined volume and you want it to continue to abide to that
    private float musicVol, soundVol, masterVol;

    private float musicSliderVal, soundSliderVal, masterSliderVal;
    private GameObject backgroundMusicGameObj;
    private AudioSource backgroundMusic;

    private float originalMusicVol, originalSoundVol;

    public static SoundManager instance;
    void Start()
    {
        if (instance == null){
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        masterVol = 1;
        musicVol = 1;
        soundVol = 1;

        masterSliderVal = 1;
        musicSliderVal = 1;
        soundSliderVal = 1;
        originalMusicVol = 1;
        originalSoundVol = 1;

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBackgroundMusic();

        
        DontDestroyOnLoad(this.gameObject);
    }

    public void playAudio(AudioSource soundSource, string type, float valueToScale = 1)
    {
        if (type == "Sound")
        {
            soundSource.volume = soundVol * valueToScale;
            soundSource.Play();
        }
        else if (type == "Music")
        {
            soundSource.volume = musicVol * valueToScale;
            soundSource.Play();
        }
        else
        {
            Debug.Log("Inputted wrong prompt");
        }
    }

    //Useful for audios that may play the same audio more than once.
    public void playAudio(ArrayList soundSources, float valueToScale = 1)
    {
        for (int i = 0; i < soundSources.Count; i++)
        {
            AudioSource audio = (AudioSource)soundSources[i];
            if (audio != audio.isPlaying)
            {
                audio.volume = soundVol;
                audio.Play();
                break;
            }

        }

    }

    public void playAudio(AudioSource[] soundSources, float valueToScale = 1)
    {
        int index = UnityEngine.Random.Range(0, soundSources.Length - 1);
        AudioSource soundToPlay = soundSources[index];
        if (!soundToPlay.isPlaying)
        {
            soundToPlay.volume = soundVol * valueToScale;
            soundToPlay.Play();
        }
        else
        {
            playAudio(soundSources, valueToScale);
        }


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();
    }



    private void PlayBackgroundMusic()
    {
        backgroundMusicGameObj = GameObject.Find("BackgroundMusic");
        backgroundMusic = backgroundMusicGameObj.GetComponent<AudioSource>();
        playAudio(backgroundMusic, "Music");
    }

    public void ChangeMasterVol(float sliderVal)
    {
        masterSliderVal = sliderVal;
        masterVol = sliderVal;
        musicVol = originalMusicVol * masterVol;

        backgroundMusic.volume = musicVol;
        soundVol = originalSoundVol * masterVol;

    }

    public void ChangeMusicVol(float sliderVal)
    {
        musicSliderVal = sliderVal;
        musicVol = sliderVal * masterVol;
        originalMusicVol = musicVol;
        backgroundMusic.volume = musicVol;
    }

    public void ChangeSoundVol(float sliderVal)
    {
        soundSliderVal = sliderVal;
        soundVol = sliderVal * soundVol;
        originalSoundVol = soundVol;
        soundVol = sliderVal;

    }

    public float GetMasterVol(){
        return masterVol;
    }

    public float GetSoundVol(){
        return soundVol;
    }

    public float GetMusicVol(){
        return musicVol;
    }

    public float GetMusicSliderVal()
    {
        return musicSliderVal;
    }

    public float GetMasterSliderVal()
    {
        return masterSliderVal;
    }

    public float GetSoundSliderVal()
    {
        return soundSliderVal;
    }
}
