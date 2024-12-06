using System.Collections;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private AudioSource footstepSound;

    [Header("Footstep Settings")]
    [SerializeField] private LayerMask terrainLayerMask;
    [SerializeField] private float stepInterval = 1f;

    private float nextStepTimer = 0;

    [Header("Camera Bob")]
    [SerializeField] private float bobFrequency = 1f;
    [SerializeField] private float bobAmplitude = 1f;

    private CinemachineBasicMultiChannelPerlin noiseComponent;

    [Header("SFX")]
    [SerializeField] private AudioClip[] groundFootstep;
    [SerializeField] private AudioClip[] grassFootstep;
    [SerializeField] private AudioClip[] gravelFootstep;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Collision Detection")]
    [SerializeField] private float capsuleCastRadius = 0.5f;
    [SerializeField] private float capsuleCastHeight = 2f;
    [SerializeField] private float collisionBuffer = 0.1f;
    [SerializeField] private LayerMask collisionLayerMask;


    private float verticalVelocity;
    private float currentSpeed;
    private float currentSpeedMultiplier;

    private float xRotation;

    [Header("Input")]
    [SerializeField] private float mouseSensitivity = 100f; // Default sensitivity
    private float moveInput;
    private float turnInput;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Set xRotation to the camera's initial rotation
        xRotation = virtualCamera.transform.localRotation.eulerAngles.x;

        noiseComponent = virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }


    private void Update()
    {
        InputManagement();
        Movement();

        PlayFootstepSound();
    }

    private void LateUpdate() {
        CameraBob();
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }

    private void GroundMovement()
    {
        // Buat vektor pergerakan
        Vector3 move = transform.right * turnInput + transform.forward * moveInput;

        // Normalisasi agar tidak lebih cepat saat diagonal
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        // Periksa tumbukan di arah pergerakan
        if (DetectCollision(move))
        {
            // Jika ada tumbukan, batalkan pergerakan
            Debug.Log("Collision detected! Stopping movement.");
            return;
        }

        // Sprint
        currentSpeedMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintSpeedMultiplier : 1;

        // Lerp untuk transisi kecepatan
        currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed * currentSpeedMultiplier, sprintTransitSpeed * Time.deltaTime);

        // Tambahkan kecepatan ke vektor gerakan
        move *= currentSpeed;

        // Tambahkan gravitasi
        move.y = VerticalForceCalculation();

        // Pindahkan karakter
        controller.Move(move * Time.deltaTime);
    }


    private void Turn()
    {
        // Read mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Handle vertical rotation (camera pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit pitch angle

        // Apply rotation to the camera
        virtualCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Handle horizontal rotation (player yaw)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void CameraBob()
    {
        if(controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            noiseComponent.FrequencyGain = bobFrequency * currentSpeedMultiplier;
            noiseComponent.AmplitudeGain = bobAmplitude * currentSpeedMultiplier;
        } else {
            noiseComponent.FrequencyGain = 0.0f;
            noiseComponent.AmplitudeGain = 0.0f;
        }
    }

    private void PlayFootstepSound()
    {
        if(controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            if(Time.time >= nextStepTimer)
            {
                AudioClip[] footstepClips =  DetermineAudioClips();

                if(footstepClips.Length > 0)
                {
                    AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

                    footstepSound.PlayOneShot(clip);
                }

                nextStepTimer = Time.time + (stepInterval / currentSpeedMultiplier);
            }
        }
    }

    private AudioClip[] DetermineAudioClips()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit, 1.5f, terrainLayerMask))
        {
            string tag = hit.collider.tag;

            switch(tag)
            {
                case "Ground":
                    return groundFootstep;
                case "Grass":
                    return grassFootstep;
                case "Gravel":
                    return gravelFootstep;
                default:
                    return groundFootstep;
            }
        }

        return groundFootstep;
    }

    private float VerticalForceCalculation()
    {
        if(controller.isGrounded){
            verticalVelocity = -1f;

            if(Input.GetButtonDown("Jump")){
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity *2);
            }
        }
        else {
            
            verticalVelocity -= gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }

    private bool DetectCollision(Vector3 direction)
    {
        // Hitungan titik awal dan akhir kapsul
        Vector3 capsuleBottom = transform.position + Vector3.up * (capsuleCastRadius);
        Vector3 capsuleTop = capsuleBottom + Vector3.up * (capsuleCastHeight - capsuleCastRadius * 2);

        // Lakukan CapsuleCast
        bool isHit = Physics.CapsuleCast(
            capsuleBottom,
            capsuleTop,
            capsuleCastRadius,
            direction,
            out RaycastHit hitInfo,
            collisionBuffer,
            collisionLayerMask
        );

        // Debug untuk melihat hasil
        if (isHit)
        {
            Debug.DrawLine(capsuleBottom, hitInfo.point, Color.red, 1f);
        }
        else
        {
            Debug.DrawRay(capsuleBottom, direction * collisionBuffer, Color.green, 1f);
        }

        return isHit;
    }


    private void InputManagement()
    {
        // Read movement inputs
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    
}
