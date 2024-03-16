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

    SaveData currentSaveData;

    Save currentSaveSlot;

    private string path;

   


    void Start()
    {
        sm = soundManagerObj.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath;
        CheckData(path);
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }




    //Each Slot will have its saveslot updated here
    private void CheckData(string path){
        GameObject[] saveSlots = {saveSlot1, saveSlot2, saveSlot3};
        
        foreach (GameObject saveSlot in saveSlots){
            Save saveSlotSave = saveSlot.GetComponent<Save>();
            SaveData saveSlotData = saveSlotSave.GetSaveData();
            string saveSlotPath = Path.Combine(path, saveSlotData.saveDataName + ".json");
            string json = File.ReadAllText(saveSlotPath);
            SaveData parsedSaveData = JsonUtility.FromJson<SaveData>(json);
            saveSlotSave.SetSaveData(parsedSaveData);
        }
            
            
        
    }


     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu" && !currentSaveData.GameBeat){
            UpdateSaveFile(currentSaveData, scene.name, sm);   
        } else if (scene.name == "Credits"){
            currentSaveData.currentLevel = null;
            currentSaveData.currentSlotInfo = "Level Select Mode!";
            currentSaveData.GameBeat = true;
            currentSaveData.currentMasterVol = sm.GetMasterVol();
            currentSaveData.currentMusicVol = sm.GetMusicVol();
            currentSaveData.currentSoundVol = sm.GetSoundVol();
            currentSaveData.currentMusicSlider = sm.GetMusicSliderVal();
            currentSaveData.currentMasterSlider = sm.GetMasterSliderVal();
            currentSaveData.currentSoundSlider = sm.GetSoundSliderVal();
        }
    }

    private void UpdateSaveFile(SaveData currentSave, string sceneName, SoundManager sm){
        currentSave.currentLevel = sceneName;
        currentSave.currentSlotInfo = sceneName;
        currentSave.currentMasterVol = sm.GetMasterVol();
        currentSave.currentMusicVol = sm.GetMusicVol();
        currentSave.currentSoundVol = sm.GetSoundVol();
        currentSave.currentMusicSlider = sm.GetMusicSliderVal();
        currentSave.currentMasterSlider = sm.GetMasterSliderVal();
        currentSave.currentSoundSlider = sm.GetSoundSliderVal();
        Debug.Log("Saving!");
    }
  

    public void SetCurrentGameSlot(GameObject SaveSlotObj) {
        currentSaveSlot = SaveSlotObj.GetComponent<Save>();
        currentSaveData = currentSaveSlot.GetSaveData();
        Debug.Log("Save " + currentSaveData.saveDataName + " Locked in!");
    }

    private void OnApplicationQuit() {
        CreateJSON();
    }
    private void CreateJSON(){
       string json = JsonUtility.ToJson(currentSaveData);
       string specificFilePath = Path.Combine(path, currentSaveData.saveDataName + ".json");
       File.WriteAllText(specificFilePath, json);
    }

    //TO-DO
    public void SetCurrentLanguage(){
        
    }



    
 
}



    

