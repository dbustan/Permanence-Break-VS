using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTutorialScript : MonoBehaviour
{
    public Text text;
    public Text look;
    public Text move;
    public Text scroll;
    public Text backwards;
    public PlayerControllerPhysics player;
    float totalMouseMovement = 0;
    private bool needLook, needMoveForward, needChangeSpeed, needMoveBackward, needJump;

    void Start()
    {
        Debug.Log(look.text);
        text.text = look.text;
        player.setMovementEnabled(false);
        needLook = needMoveForward = needChangeSpeed = needMoveBackward = needJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        totalMouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        //Debug.Log(totalMouseMovement);
        if (needLook && totalMouseMovement > 100)
        {
            player.setMovementEnabled(true);
            text.text = move.text;
            needLook = false;
        }
        else if (!needLook && needMoveForward && Input.GetMouseButtonDown(1))
        {
            needMoveForward = false;
            text.text = scroll.text;
        }
        else if (!needMoveForward && needChangeSpeed && Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            needChangeSpeed = false;
            text.text = backwards.text;
        }
        else if (!needChangeSpeed && needMoveBackward && Input.GetMouseButtonDown(1) && player.getCurrentWalkingSpeed() < 0)
        {
            needMoveBackward = false;
            text.text = "";
        }

    }
}
