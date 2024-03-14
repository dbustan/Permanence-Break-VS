using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
   private bool currentOpenState, newOpenState;

   [SerializeField] 

   private AudioSource closedDoor, openDoor;

   private Door door;

    void Start()
    {
        door = GetComponent<Door>();
        currentOpenState = door.getOpenState();
    }

    // Update is called once per frame
    void Update()
    {
        newOpenState = door.getOpenState();
        if (currentOpenState != newOpenState){
            if (currentOpenState){
                //opening sound effect
            } else {
                //closing
            }
            currentOpenState = newOpenState;
        }
    }
}
