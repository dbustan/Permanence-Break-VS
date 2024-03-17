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

    private string state;
    private bool needLook, needMoveForward, needChangeSpeed, needMoveBackward, needJump;

    void Start()
    {
        state = "look";
        text.text = look.text;
        player.setMovementEnabled(false);
        needLook = needMoveForward = needChangeSpeed = needMoveBackward = needJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        totalMouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));

        if (needLook && totalMouseMovement > 100)
        {
            player.setMovementEnabled(true);
            text.text = move.text;
            needLook = false;
            state = "move";
        }
        else if (!needLook && needMoveForward && Input.GetMouseButtonDown(1))
        {
            needMoveForward = false;
            text.text = scroll.text;
            state = "scroll";
        }
        else if (!needMoveForward && needChangeSpeed && Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            needChangeSpeed = false;
            text.text = backwards.text;
            state = "backwards";
        }
        else if (!needChangeSpeed && needMoveBackward && Input.GetMouseButtonDown(1) && player.getCurrentWalkingSpeed() < 0)
        {
            needMoveBackward = false;
            text.text = "";
            state = "done";
        }

    }

    public void refreshText()
    {
        switch (state)
        {
            case "look":
                text.text = look.text;
                break;
            case "move":
                text.text = move.text;
                break;
            case "scroll":
                text.text = scroll.text;
                break;
            case "backwards":
                text.text = backwards.text;
                break;
            default:
                break;
        }

    }
}
