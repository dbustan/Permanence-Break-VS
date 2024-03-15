using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] 

    private GameObject SoundManagerObj;
    private SoundManager sm;

    [SerializeField] 
    private GameObject CurrentSaveSlot;

    //Create SoundData
   






    private string currentSaveSlot;
    //We want to store our current Scene as a string, and when we load in we want the game to have reference to what
    void Start()
    {
        sm = SoundManagerObj.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);    
    }

    private void OnApplicationQuit() {
        CreateJSON();
    }

    public void SetCurrentGameSlot(GameObject SaveSlotObj) {
        CurrentSaveSlot = SaveSlotObj;
    }

    private void CreateJSON(){
        Scene currentLevelScene = SceneManager.GetActiveScene();
        SaveData currentSave = SaveData.CreateInstance<SaveData>();
        currentSave.currentLevel = currentLevelScene.name;
        currentSave.currentSlot = CurrentSaveSlot.name;
        currentSave.currentMasterVol = sm.GetMasterVol();
        currentSave.currentMusicVol = sm.GetMusicVol();
        currentSave.currentSoundVol = sm.GetSoundVol();
       
       string json = JsonUtility.ToJson(currentSave);
       string path = Application.persistentDataPath + "/CurrentSave.json";
       File.WriteAllText(path, json);
    }



    
 
}



    

