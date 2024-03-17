using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataSerializable
{
    public string saveDataName;
    public string currentLevel;

    public string currentSlotInfo;

    public float currentMouseSens;

    public string currentLanguage;

    public bool GameBeat;
    public bool currentGrayScale;

    public void SetSerializableData(SaveData saveData){
        saveDataName = saveData.saveDataName;
        currentLevel = saveData.currentLevel;
        currentSlotInfo = saveData.currentSlotInfo;
        currentMouseSens = saveData.currentMouseSens;
        currentLanguage = saveData.currentLanguage;
        GameBeat = saveData.GameBeat;
        currentGrayScale = saveData.currentGrayScale;
    }
    public void SetSaveData(SaveData saveData){
        saveData.saveDataName = saveDataName;
        saveData.currentLevel = currentLevel;
        saveData.currentSlotInfo = currentSlotInfo;
        saveData.currentMouseSens = currentMouseSens;
        saveData.currentLanguage = currentLanguage;
        saveData.GameBeat = GameBeat;
        saveData.currentGrayScale = currentGrayScale;
    }
    
}
