using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource [] footsteps;
    private Vector3 lastPos;

    private SoundManager sm;

    private GameObject soundManagerGameobj;

    private GameObject footstepsGameObj;

     private float footstepsVol;

    [SerializeField]
    private float moveThreshold = 1.45f;
    void Start()
    {
        footstepsVol = 0.2f;
        footstepsGameObj = gameObject.transform.GetChild(2).gameObject;
        footsteps = footstepsGameObj.GetComponents<AudioSource>();
        lastPos = transform.position;
        soundManagerGameobj = GameObject.Find("SoundManager");
        sm = soundManagerGameobj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = Vector3.Distance(transform.position, lastPos);

        if (moveDistance >= moveThreshold){
            lastPos = transform.position;
            sm.playAudio(footsteps, footstepsVol);
        }
    }
}
