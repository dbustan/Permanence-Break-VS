using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{

   
   [SerializeField] public SaveManager saveManager; 

   public void OnClick(){
        saveManager.SetCurrentGameSlot(gameObject);
   }
    
}
