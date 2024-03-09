using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTutorialScript : MonoBehaviour
{
    public Text text;
    public PlayerControllerPhysics player;
    float totalMouseMovement = 0;
    private bool needLook, needMoveForward, needChangeSpeed, needMoveBackward, needJump;
    
    void Start()
    {
        text.text = "Look around by moving the mouse";
        player.setMovementEnabled(false);
        needLook = needMoveForward = needChangeSpeed = needMoveBackward = needJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        totalMouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        Debug.Log(totalMouseMovement);
        if(needLook && totalMouseMovement > 100) {
            player.setMovementEnabled(true);
            text.text = "Move using Right Mouse Button";
            needLook = false;
        } else if(!needLook && needMoveForward && Input.GetMouseButtonDown(1)) {
            needMoveForward = false;
            text.text = "Change your current speed with the Scroll Wheel ";
        } else if(!needMoveForward && needChangeSpeed && Mathf.Abs(Input.mouseScrollDelta.y) > 0) {
            needChangeSpeed = false;
            text.text = "Walk Backwards using by changing your speed";
        } else if(!needChangeSpeed && needMoveBackward && Input.GetMouseButtonDown(1) && player.getCurrentWalkingSpeed() < 0) {
            needMoveBackward = false;
            text.text = "";
        }
    }
}
