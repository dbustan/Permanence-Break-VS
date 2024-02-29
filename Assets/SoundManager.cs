using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private float musicVol,soundVol;
     
    void Start()
    {
        //Add long term persistance
        musicVol = 100;
        soundVol = 100;
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
}
