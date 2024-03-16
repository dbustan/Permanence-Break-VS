using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{

   
   [SerializeField] public SaveManager saveManager;

   [SerializeField] private MenuScript menuManager;

   [SerializeField] private GameObject LevelSelectUI;

   [SerializeField] private TextMeshProUGUI saveDescription;

   private SaveData save; 

   private void Start(){
      GenerateBlankSaveSlot();
   }
   public void OnClick(){
        saveManager.SetCurrentGameSlot(gameObject);
        
        if (!save.GameBeat){
         SceneManager.LoadScene(save.currentLevel);
        } else {
            menuManager.OpenLevelSelect();
        }
        
   }

   private void GenerateBlankSaveSlot(){
      save = SaveData.CreateInstance<SaveData>();
      save.saveDataName = gameObject.name;
   }

   public SaveData GetSaveData(){
      return save;
   }

   public void SetSaveData(SaveData newSave){
      save = newSave;
      saveDescription.text = save.currentSlotInfo;
   }




   

    
}
