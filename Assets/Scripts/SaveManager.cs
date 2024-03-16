using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class handles all data that needs to be stored.
public class SaveManager : MonoBehaviour
{
    [SerializeField] 

    private GameObject soundManagerObj;
    private SoundManager sm;

    [SerializeField] 
    private GameObject saveSlot1, saveSlot2, saveSlot3;


    private string currentLanguage;

    SaveData currentSave;

    private string path;

    private string currentSaveSlot;
    //We want to store our current Scene as a string, and when we load in we want the game to have reference to what
    void Start()
    {
        sm = soundManagerObj.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath;
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }


    //Will check and update per game launch, going to each saveslot 
    //Setting current level in the script of each one.
    private void CheckData(string path){
        
    }

    //We want it to save per level complete

    //Perhaps we can also just like update it whenever the player clicks save in pause
     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu"){
            UpdateSaveFile(currentSave, scene.name, sm);   
        } else if (scene.name == "Credits"){
            currentSave.currentLevel = null;
            currentSaveSlot = "Level Select Mode!";
            currentSave.currentMasterVol = sm.GetMasterVol();
            currentSave.currentMusicVol = sm.GetMusicVol();
            currentSave.currentSoundVol = sm.GetSoundVol();
            currentSave.currentMusicSlider = sm.GetMusicSliderVal();
            currentSave.currentMasterSlider = sm.GetMasterSliderVal();
            currentSave.currentSoundSlider = sm.GetSoundSliderVal();
        }
    }

    private void UpdateSaveFile(SaveData currentSave, string sceneName, SoundManager sm){
        currentSave.currentLevel = sceneName;
        currentSave.slotInfo = sceneName;
        currentSave.currentMasterVol = sm.GetMasterVol();
        currentSave.currentMusicVol = sm.GetMusicVol();
        currentSave.currentSoundVol = sm.GetSoundVol();
        currentSave.currentMusicSlider = sm.GetMusicSliderVal();
        currentSave.currentMasterSlider = sm.GetMasterSliderVal();
        currentSave.currentSoundSlider = sm.GetSoundSliderVal();
    }
  

    public void SetCurrentGameSlot(GameObject SaveSlotObj) {
        currentSaveSlot = SaveSlotObj.name;
    }

    private void CreateJSON(){
       
       string json = JsonUtility.ToJson(currentSave);
       string specificFilePath = path + currentSave.currentSlot +".json";
       File.WriteAllText(path, json);
    }

    public void SetcurrentLanguage(){

    }



    
 
}



    

