using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTutorialScript : MonoBehaviour
{
    public PlayerControllerPhysics playerController;
    public float startDelay;
    private DialogueBox dialogueBox;
    private float startTime;
    private int stage;

    public Text look;
    public Text move;
    public Text scroll;
    public Text backwards;
    public Text jump;


    void Start()
    {
        dialogueBox = playerController.GetComponentInChildren<DialogueBox>();
        playerController.setMovementEnabled(false);
        startTime = Time.time;
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= startDelay)
        {
            dialogueBox.showDialogue(look.text);
            playerController.lookThresholdReached += actionComplete;
            startTime = float.MaxValue;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            actionComplete(null, null);
        }
    }

    private void actionComplete(object sender, EventArgs e)
    {
        switch (stage)
        {
            case 0:
                playerController.lookThresholdReached -= actionComplete;
                playerController.moved += actionComplete;
                playerController.setMovementEnabled(true);
                dialogueBox.showDialogue(move.text);
                break;
            case 1:
                playerController.moved -= actionComplete;
                playerController.changedSpeed += actionComplete;
                dialogueBox.showDialogue(scroll.text);
                break;
            case 2:
                playerController.changedSpeed -= actionComplete;
                playerController.walkedBackwards += actionComplete;
                dialogueBox.showDialogue(backwards.text);
                break;
            case 3:
                playerController.walkedBackwards -= actionComplete;
                playerController.jumped += actionComplete;
                dialogueBox.showDialogue(jump.text);
                break;
            case 4:
                playerController.jumped -= actionComplete;
                dialogueBox.hideDialogue();
                return;
            default:
                dialogueBox.hideDialogue();
                return;
        }
        stage++;
    }
}
