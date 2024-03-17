using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataSerializable
{
    public string saveDataName;
    public string currentLevel;

    public string currentSlotInfo;

    public float currentMasterVol, currentMusicVol, currentSoundVol;

    public float currentMasterSlider, currentMusicSlider, currentSoundSlider;
    public float currentMouseSens;

    public string currentLanguage;

    public bool GameBeat;
    public bool currentGrayScale;

    public void SetSerializableData(SaveData saveData){
        saveDataName = saveData.saveDataName;
        currentLevel = saveData.currentLevel;
        currentSlotInfo = saveData.currentSlotInfo;
        currentMasterVol = saveData.currentMasterVol;
        currentMusicVol = saveData.currentMusicVol;
        currentMasterSlider = saveData.currentMasterSlider;
        currentMusicSlider = saveData.currentMusicSlider;
        currentSoundSlider = saveData.currentSoundSlider;
        currentMouseSens = saveData.currentMouseSens;
        currentLanguage = saveData.currentLanguage;
        GameBeat = saveData.GameBeat;
        currentGrayScale = saveData.currentGrayScale;
    }
    public void SetSaveData(SaveData saveData){
        saveData.saveDataName = saveDataName;
        saveData.currentLevel = currentLevel;
        saveData.currentSlotInfo = currentSlotInfo;
        saveData.currentMasterVol = currentMasterVol;
        saveData.currentMusicVol = currentMusicVol;
        saveData.currentMasterSlider = currentMasterSlider;
        saveData.currentMusicSlider = currentMusicSlider;
        saveData.currentSoundSlider = currentSoundSlider;
        saveData.currentMouseSens = currentMouseSens;
        saveData.currentLanguage = currentLanguage;
        saveData.GameBeat = GameBeat;
        saveData.currentGrayScale = currentGrayScale;
    }
    
}
