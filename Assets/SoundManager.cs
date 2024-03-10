using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class SoundManager : MonoBehaviour
{
    private float musicVol,soundVol, masterVol, prevMaxMusicVol, prevMaxSoundVol;
    private GameObject backgroundMusicGameObj;
    private AudioSource backgroundMusic;

    public Slider musicSlider, soundSlider;
    void Start()
    {
        //Add long term persistance
        masterVol = 1;
        musicVol = 1;
        soundVol = 1;
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBackgroundMusic();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    public void playAudio (AudioSource soundSource, string type){
        if (type == "Sound"){ 
            soundSource.volume = soundVol;
            soundSource.Play();
        } else if (type == "Music") {
            soundSource.volume = musicVol;
            soundSource.Play();
        } else {
            Debug.Log("Inputted wrong prompt");
        }
    }

    //Useful for audios that may play the same audio more than once.
    public void playAudio (ArrayList soundSources){
        for (int i = 0; i < soundSources.Count; i++){
            AudioSource audio = (AudioSource) soundSources[i];
            if (audio != audio.isPlaying){
                Debug.Log("hi");
                audio.volume = soundVol;
                audio.Play();
                break;
            }
            
        }
       
    }

    public void playAudio (AudioSource[] soundSources, float valueToScale){
        int index = UnityEngine.Random.Range(0, soundSources.Length-1);
        AudioSource soundToPlay = soundSources[index];
        if (!soundToPlay.isPlaying) {
            soundToPlay.volume = soundVol * valueToScale;
            
            soundToPlay.Play();
        } else {
            playAudio(soundSources, valueToScale);
        }

       
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic(){
        backgroundMusicGameObj = GameObject.Find("BackgroundMusic");
        backgroundMusic = backgroundMusicGameObj.GetComponent<AudioSource>();
        playAudio(backgroundMusic, "Music");
    }
    
    public void ChangeMasterVol(float sliderVal){
        masterVol = sliderVal;
        prevMaxMusicVol = musicSlider.maxValue;
        prevMaxSoundVol = soundSlider.maxValue;
        musicSlider.maxValue = masterVol;
        soundSlider.maxValue = masterVol;
        if (musicSlider.value == prevMaxMusicVol){
            musicSlider.value = masterVol;
            
        }
        if (soundSlider.value == prevMaxSoundVol){
            soundSlider.value = masterVol;
        }
        
    }

    public void ChangeMusicVol(float sliderVal) {
        musicVol = sliderVal;
        backgroundMusic.volume = musicVol;
    }

    public void ChangeSoundVol(float sliderVal){
        soundVol = sliderVal;
        
    }
}
