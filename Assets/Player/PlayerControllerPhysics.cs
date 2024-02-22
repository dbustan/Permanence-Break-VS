using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    A one-handed Physics based physics based character controller.
    Only uses the mouse, with all instances of keyboard input existing
    solely for playtesting sessions and debuggig.

    Created for the course "CSE 171: Game Design Studio I" at UCSC.
*/

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerControllerPhysics : MonoBehaviour {

    private Rigidbody rb;

    // CAMERA CONTROL
    [Header("Camera Control")]
    public float cameraSensitivity;
    public float verticalRange;
    private Camera playerCamera;
    
    // MOVEMENT
    [Header("Movment")]
    public float maxWalkingSpeed;
    public float walkingAcceleration, walkingDeceleration, walkingSpeedChangeRate, inAirDecelCoef;
    public RectTransform speedReadout;
    private float currentWalkingSpeed, currentWalkingSpeedPreCurve;
    
    // JUMPING
    [Header("Jumping")]
    public float jumpHeight;
    public float gravity, groundCheckDistance;
    public LayerMask groundCheckLayerMask;
    [SerializeField]
    private bool grounded, jumping;

    // USER INPUT
    private bool moveFlag, jumpFlag;

    private Vector3 startingPosition;

    void Start() {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();

        currentWalkingSpeedPreCurve = 0.75f;

        moveFlag = jumpFlag = false;
        startingPosition = transform.position;
    }

    void Update() {
        moveFlag = Input.GetMouseButton(1);
        if(Input.GetMouseButtonDown(2)) {
            jumpFlag = true;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            transform.position = startingPosition;
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        if(Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }
        updateCamera();
        updateSpeed();
    }

    void FixedUpdate() {
        grounded = isGrounded();
        Vector3 currentVelocity = rb.velocity;

        addWalkingMovement(ref currentVelocity);
        addVerticalMovement(ref currentVelocity);

        rb.velocity = currentVelocity;
    }

    private void addWalkingMovement(ref Vector3 currentVelocity) {
        Vector3 currentVelocityXZ = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
        bool decelerateOverride = false;
        if(moveFlag) {
            float lastMagnitude = currentVelocityXZ.magnitude;
            float absWalkSpeed = Mathf.Abs(currentWalkingSpeed);
            currentVelocityXZ += transform.rotation * Vector3.forward * walkingAcceleration * Mathf.Sign(currentWalkingSpeed) * Time.fixedDeltaTime;
            if(lastMagnitude < absWalkSpeed) {
                if(currentVelocityXZ.magnitude > absWalkSpeed) {
                    currentVelocityXZ = currentVelocityXZ.normalized * absWalkSpeed;
                }
            } else {
                if(grounded) {
                    decelerateOverride = true;
                }
                currentVelocityXZ = currentVelocityXZ.normalized * lastMagnitude;
            }
        }
        if((!moveFlag || decelerateOverride) && currentVelocityXZ.magnitude > 0f) {
            Vector3 decelDir = -currentVelocityXZ.normalized;
            currentVelocityXZ += decelDir * walkingDeceleration * (grounded ? 1f : inAirDecelCoef) * Time.fixedDeltaTime;
            if(currentVelocityXZ.sqrMagnitude <= 0.01f || Vector3.Dot(currentVelocityXZ, decelDir) >= 0) {
                currentVelocityXZ = Vector3.zero;
            }
        }

        currentVelocity = new Vector3(currentVelocityXZ.x, currentVelocity.y, currentVelocityXZ.z);
    }
    private void updateSpeed() {
        currentWalkingSpeedPreCurve = Mathf.Clamp(currentWalkingSpeedPreCurve + Input.mouseScrollDelta.y * walkingSpeedChangeRate, -1, 1);
        currentWalkingSpeed = maxWalkingSpeed * Mathf.Pow(currentWalkingSpeedPreCurve, 2) * Mathf.Sign(currentWalkingSpeedPreCurve);
        speedReadout.localScale = new Vector3(1f, getSpeedReadoutScale(), 1f);
    }
    private float getSpeedReadoutScale() {
        return 6*(currentWalkingSpeed/maxWalkingSpeed);
    }
    
    private bool isGrounded() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, groundCheckDistance + 0.1f, groundCheckLayerMask)) {
            Interactible interactible = hit.collider.GetComponent<Interactible>();
            if(interactible && interactible.grabbed) return false;
            return true;
        }
        return false;
    }
    private void addVerticalMovement(ref Vector3 currentVelocity) {
        if(jumpFlag) {
            tryJump(ref currentVelocity);
        }
        if(!grounded) {
            if(jumping && currentVelocity.y < 0) {
                jumping = false;
            }
            currentVelocity += Vector3.down * gravity * Time.fixedDeltaTime;
        }
    }
    private void tryJump(ref Vector3 currentVelocity) {
        if(grounded && !jumping) {
            currentVelocity += Vector3.up * Mathf.Sqrt(2 * gravity * jumpHeight);
            jumping = true;
        }
        jumpFlag = false;
    }
    
    private void updateCamera() {
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 rotationDelta = new Vector3(-mouseMovement.y, mouseMovement.x, 0f) * cameraSensitivity * Time.deltaTime;

        float currentPlayerRotationY = transform.localRotation.eulerAngles.y;
        currentPlayerRotationY += rotationDelta.y;
        transform.localRotation = Quaternion.Euler(Vector3.up * currentPlayerRotationY);

        float currentCameraRotationX = playerCamera.transform.localRotation.eulerAngles.x;
        if(currentCameraRotationX >= 180) currentCameraRotationX -= 360;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX + rotationDelta.x, -verticalRange/2, verticalRange/2);
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * currentCameraRotationX);
    }

}
