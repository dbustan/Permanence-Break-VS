using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;
    public LayerMask interactionLayerMask;
    public Text interactionInfoText;
    public Transform heldTargetTransform;
    public float heldObjectTrackSpeedCoef, heldObjectTrackSpeedMax, heldObjectRotSpeedCoef, heldObjectRotSpeedMax, dropDist;

    private Camera playerCamera;

    private GameObject heldObject;
    const KeyCode PAUSE_KEY = KeyCode.Escape;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PAUSE_KEY))
        {
            if (Time.timeScale == 0) PauseManager.pauseManagerInstance.UnpauseGame();
            else PauseManager.pauseManagerInstance.PauseGame();
        }

        if (!PauseManager.pauseManagerInstance.IsPaused())
        {
            bool grabButtonPressed = Input.GetMouseButtonDown(0);
            RaycastHit hit;
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.red);
            interactionInfoText.text = "";
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactionLayerMask))
            {
                GameObject obj = hit.collider.gameObject;
                Interactible objInterData = obj.GetComponent<Interactible>();
                if (objInterData)
                {
                    if (objInterData.gameObject != heldObject)
                    {
                        interactionInfoText.rectTransform.anchoredPosition = getInteractibleCenterPos(objInterData) - new Vector2(0, 100f);
                        interactionInfoText.text = objInterData.interactionText.text;
                    }

                    if (grabButtonPressed)
                    {
                        grabButtonPressed = false;
                        if (heldObject)
                        {
                            heldObject.GetComponent<Interactible>().drop();
                            heldObject = null;
                        }
                        else
                        {
                            heldObject = obj;
                            heldObject.GetComponent<Interactible>().grab();
                        }
                    }

                }
            }
            if (grabButtonPressed)
            {
                if (heldObject)
                {
                    heldObject.GetComponent<Interactible>().drop();
                    heldObject = null;
                }
            }
            if (heldObject)
            {
                Vector3 distApart = heldTargetTransform.position - heldObject.transform.position + new Vector3(0, 0.7f, 0);
                if (distApart.sqrMagnitude > Mathf.Pow(0.01f, 2))
                {
                    heldObject.GetComponent<Rigidbody>().velocity = distApart * heldObjectTrackSpeedCoef;
                }
                else
                {
                    heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }

    private Vector2 getInteractibleCenterPos(Interactible interactible)
    {
        Vector3Int screenSize = new Vector3Int(playerCamera.pixelWidth, playerCamera.pixelHeight);
        return playerCamera.WorldToScreenPoint(interactible.getInteractionTextPosition()) - screenSize / 2;
    }
}
