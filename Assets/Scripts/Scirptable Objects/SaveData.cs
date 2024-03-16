using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  [CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData", order = 1)]
public class SaveData : ScriptableObject
{
    public string currentLevel;

    public string currentSlot;

    public float currentMasterVol, currentMusicVol, currentSoundVol;

    public float currentMasterSlider, currentMusicSlider, currentSoundSlider;
    public float currentMouseSens;

    public string currentLanguage;

    public string slotInfo;

    public bool currentGrayScale;

    
    
}
