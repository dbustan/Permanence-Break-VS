using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{

   
   [SerializeField] public SaveManager saveManager;

   private string currentLevel;

   private SaveData save; 

   public void OnClick(){
        saveManager.SetCurrentGameSlot(gameObject);
        if (save.currentLevel == "Complete"){
         
        } else {

        }
        
   }

   public void SetCurrentLevel(string current){
      currentLevel = current;
   }

   

    
}
