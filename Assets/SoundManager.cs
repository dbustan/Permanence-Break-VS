using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class SoundManager : MonoBehaviour
{
    private float musicVol,soundVol, masterVol, MASTER_AFFECTING;
    private GameObject backgroundMusicGameObj;
    private AudioSource backgroundMusic;
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
        MASTER_AFFECTING = 1 - masterVol; 
        if (type == "Sound"){ 
            soundSource.volume = soundVol - MASTER_AFFECTING;
            soundSource.Play();
        } else if (type == "Music") {
            soundSource.volume = musicVol;
            soundSource.Play();
        } else {
            Debug.Log("Inputted wrong prompt");
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
        MASTER_AFFECTING = 1 - masterVol; 
        backgroundMusic.volume = musicVol - MASTER_AFFECTING;
        Debug.Log(backgroundMusic.volume);
        
    }
}
