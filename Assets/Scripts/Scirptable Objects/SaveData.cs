using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  [CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData", order = 1)]
public class SaveData : ScriptableObject
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

    
    
}
