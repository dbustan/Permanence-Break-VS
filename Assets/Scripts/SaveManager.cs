using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class handles all data that needs to be stored.
public class SaveManager : MonoBehaviour
{
    [SerializeField] 

    private GameObject soundManagerObj;

    [SerializeField]

    private GameObject saveMenuUI;
    private SoundManager sm;

    [SerializeField] 
    private GameObject saveSlot1, saveSlot2, saveSlot3;
    private SaveData saveData1, saveData2, saveData3;

    SaveData currentSaveData;



    Save currentSaveSlot;

    private string path;

   


    void Start()
    {
        saveMenuUI.SetActive(true);
        GenerateBlankSaves();
        sm = soundManagerObj.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath;
        CheckData(path);
        SceneManager.sceneLoaded += OnSceneLoaded;
           
    }


    private void GenerateBlankSaves(){
        saveData1 = SaveData.CreateInstance<SaveData>();
        saveData1.saveDataName = "Save1";
        saveData1.currentLevel = "Level1";
        saveData2 = SaveData.CreateInstance<SaveData>();
        saveData2.saveDataName = "Save2";
        saveData2.currentLevel = "Level1";
        saveData3 = SaveData.CreateInstance<SaveData>();
        saveData3.saveDataName = "Save3";
        saveData3.currentLevel = "Level1";
    }
    //Each Slot will have its saveslot updated here
    private void CheckData(string path){
        
        GameObject[] saveSlots = {saveSlot1, saveSlot2, saveSlot3};
        SaveData[] saveDatas = {saveData1,saveData2, saveData3};
        for (int i = 0; i < 3; i++){
            string saveSlotPath = Path.Combine(path, saveDatas[i].saveDataName + ".json");
            Debug.Log(path);
            if (!File.Exists(saveSlotPath)){
                Debug.Log("No File exists for " + saveSlotPath);
            } else {
                string json = File.ReadAllText(saveSlotPath);
                SaveDataSerializable saveDataSerializable = JsonUtility.FromJson<SaveDataSerializable>(json);
                saveDataSerializable.SetSaveData(saveDatas[i]);
                  
            }
            //string json = File.ReadAllText(saveSlotPath);
            //SaveData parsedSaveData = JsonUtility.FromJson<SaveData>(json);
            //saveSlotSave.SetSaveData(parsedSaveData);
            Debug.Log(saveDatas[i].saveDataName);
        }
        
        
        saveMenuUI.SetActive(false);
            
            
        
    }


     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name != "MainMenu" && !currentSaveData.GameBeat){
             Debug.Log(scene.name);
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
        Debug.Log("Saving! " + currentSave.saveDataName);
    }
  

    public SaveData SetCurrentGameSlot(string SaveSlot) {
        SaveData[] saveDatas = {saveData1,saveData2, saveData3};
        for (int i = 0; i < 3; i++){
            if (SaveSlot == saveDatas[i].saveDataName){
                currentSaveData = saveDatas[i];
                break;
            }
        }
        Debug.Log(currentSaveData.saveDataName + " Locked in!");
        return currentSaveData;
    }

    private void OnApplicationQuit() {
        Debug.Log(currentSaveData.saveDataName);
        CreateJSON();
    }
    private void CreateJSON(){
       SaveDataSerializable saveDataSerializable = new SaveDataSerializable();
       saveDataSerializable.SetSerializableData(currentSaveData);
       string json = JsonUtility.ToJson(saveDataSerializable);
       string specificFilePath = Path.Combine(path, saveDataSerializable.saveDataName + ".json");
       File.WriteAllText(specificFilePath, json);
       Debug.Log("Creating Json...");
    }

    //TO-DO
    public void SetCurrentLanguage(){
        
    }



    
 
}



    

