using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.SimpleLocalization.Scripts;
using Unity.Collections;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Class handles all data that needs to be stored.
public class SaveManager : MonoBehaviour
{
    [SerializeField]

    private GameObject soundManagerObj;

    [SerializeField]

    private GameObject saveMenuUI;





    [SerializeField]
    private GameObject optionsMenuUI;
    private SoundManager sm;

    [SerializeField]
    private GameObject saveSlot1, saveSlot2, saveSlot3;
    private SaveData saveData1, saveData2, saveData3;


    private bool inChinese, grayScale;







    [SerializeField]
    private Slider mainMenuMusicSlider, mainMenuMasterSlider, mainMenuSoundSlider;

    SaveData currentSaveData;

    private string path;

    public static SaveManager instance;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        saveMenuUI.SetActive(true);
        inChinese = false;
        GenerateBlankSaves();
        sm = soundManagerObj.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath;
        CheckData(path);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }


    private void GenerateBlankSaves()
    {
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
    private void CheckData(string path)
    {

        GameObject[] saveSlots = { saveSlot1, saveSlot2, saveSlot3 };
        SaveData[] saveDatas = { saveData1, saveData2, saveData3 };
        for (int i = 0; i < 3; i++)
        {
            string saveSlotPath = Path.Combine(path, saveDatas[i].saveDataName + ".json");
            if (!File.Exists(saveSlotPath))
            {
                Debug.Log("No File exists for " + saveSlotPath);
            }
            else
            {
                string json = File.ReadAllText(saveSlotPath);
                SaveDataSerializable saveDataSerializable = JsonUtility.FromJson<SaveDataSerializable>(json);
                saveDataSerializable.SetSaveData(saveDatas[i]);

            }
        }
<<<<<<< HEAD
        string audioPath = Path.Combine(path, "audio" + ".json");
        if (File.Exists(audioPath))
        {
            string audioJson = File.ReadAllText(audioPath);
            SoundValues soundVals = JsonUtility.FromJson<SoundValues>(audioJson);
            sm.SetOriginalMusic(soundVals.originalMusicVol);
            sm.SetOriginalSound(soundVals.originalSoundVol);
            sm.ChangeMasterVol(soundVals.masterSliderVal);
            sm.ChangeMusicVol(soundVals.musicSliderVal);
            sm.ChangeSoundVol(soundVals.soundSliderVal);



        }




=======
        // string settingsPath = Path.Combine(path, "settings" + ".json");
        // if (File.Exists(settingsPath)){
        //     string json = File.ReadAllText(settingsPath);
        //     SettingsValues SettingsValues = JsonUtility.FromJson<SettingsValues>(json);
        // }
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
        saveMenuUI.SetActive(false);



    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
<<<<<<< HEAD

        if (scene.name != "MainMenu" && !currentSaveData.GameBeat)
        {
            Debug.Log(scene.name);
            UpdateSaveFile(currentSaveData, scene.name, sm);
        }
        else if (scene.name == "Credits")
        {
=======
       
        if (scene.name != "MainMenu" && !currentSaveData.GameBeat){
            UpdateSaveFile(currentSaveData, scene.name, sm);   
        } else if (scene.name == "Credits"){
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
            currentSaveData.currentLevel = null;
            currentSaveData.currentSlotInfo = "Level Select Mode!";
            currentSaveData.GameBeat = true;
        }
    }

    private void UpdateSaveFile(SaveData currentSave, string sceneName, SoundManager sm)
    {
        currentSave.currentLevel = sceneName;
        currentSave.currentSlotInfo = sceneName;
<<<<<<< HEAD

        currentSave.currentMasterVol = sm.GetMasterVol();
        currentSave.currentMusicVol = sm.GetMusicVol();
        currentSave.currentSoundVol = sm.GetSoundVol();
        currentSave.currentMusicSlider = sm.GetMusicSliderVal();
        currentSave.currentMasterSlider = sm.GetMasterSliderVal();
        currentSave.currentSoundSlider = sm.GetSoundSliderVal();
=======
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
        Debug.Log("Saving! " + currentSave.saveDataName);
    }


    public SaveData SetCurrentGameSlot(string SaveSlot)
    {
        SaveData[] saveDatas = { saveData1, saveData2, saveData3 };
        for (int i = 0; i < 3; i++)
        {
            if (SaveSlot == saveDatas[i].saveDataName)
            {
                currentSaveData = saveDatas[i];
                break;
            }
        }
        Debug.Log(currentSaveData.saveDataName + " Locked in!");
        return currentSaveData;
    }

<<<<<<< HEAD
    private void OnApplicationQuit()
    {
        Debug.Log(currentSaveData.saveDataName);
        CreateSaveJSON();
        CreateAudioJSON();

=======
    private void OnApplicationQuit() {
        if (currentSaveData != null){
            CreateSaveJSON();
        }
        
        //CreateSettingsJSON();
        
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
    }
    private void CreateSaveJSON()
    {
        SaveDataSerializable saveDataSerializable = new SaveDataSerializable();
        saveDataSerializable.SetSerializableData(currentSaveData);
        string json = JsonUtility.ToJson(saveDataSerializable);
        string specificFilePath = Path.Combine(path, saveDataSerializable.saveDataName + ".json");
        File.WriteAllText(specificFilePath, json);
        Debug.Log("Creating Json...");
    }

<<<<<<< HEAD
    private void CreateAudioJSON()
    {
        SoundValues soundValues = new SoundValues
=======
    private void CreateSettingsJSON(){
        SettingsValues settingsValues = new SettingsValues
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
        {
            chinese = inChinese,

        };

<<<<<<< HEAD
        string json = JsonUtility.ToJson(soundValues);
        string specificFilePath = Path.Combine(path, "audio" + ".json");
        File.WriteAllText(specificFilePath, json);
    }

    //TO-DO
    public void SetCurrentLanguage()
    {

=======
        
        string json = JsonUtility.ToJson(settingsValues);
        string specificFilePath = Path.Combine(path, "settings" + ".json");
        File.WriteAllText(specificFilePath, json);
    }

    public void InChinese(){
        inChinese = true;
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
    }

    public void InEnglish()
    {
        inChinese = false;
    }

<<<<<<< HEAD





=======
    public void ToggleGrayscale(){
        grayScale = !grayScale;
    }
    public SaveData GetCurrentSaveData(){
        return currentSaveData;
    }
    
 
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78
}


[System.Serializable]
<<<<<<< HEAD
public class SoundValues
{
    public float masterVol;
    public float musicVol;
    public float soundVol;
    public float masterSliderVal;
    public float musicSliderVal;
    public float soundSliderVal;
    public float originalMusicVol;
    public float originalSoundVol;
}

=======
    public class SettingsValues
    {
        // public double masterVol;
        // public double musicVol;
        // public double soundVol;
        // public float masterSliderVal;
        // public float musicSliderVal;
        // public float soundSliderVal;
        // public float originalMusicVol;
        // public float originalSoundVol;

        public bool chinese;

       
    }
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78




<<<<<<< HEAD
=======

    
>>>>>>> bd86907042382c797562fc438c61e86bd73c6e78

