using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterControllerInput : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 movementInput;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float gravity = -10f;
    private float currentGravity = -10f;
    private Vector3 _velocity;
    private bool isGrounded = false;
    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpForceFadeSpeed;
    private bool isJumping = false;
    private bool jumpRequest;
    private float jumpRequestTimer = 0.5f;

    private Animator _animator;

    [Header("SFX Stuff")]
    [SerializeField] EventReference walkingSFX;
    EventInstance walkingI;
    bool walkingStarted;
    [SerializeField] EventReference breathingSFX;
    EventInstance breathingI;
    bool breathingStarted;
    private float breathingTimer;
    [SerializeField] float breathingNeed = 10;
    [SerializeField] EventReference jumpSFX;
    [SerializeField] EventReference barkSFX;
    public KeyCode keyToBark;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        currentGravity = gravity;

        //Start Sounds
        walkingI = RuntimeManager.CreateInstance(walkingSFX);
        breathingI = RuntimeManager.CreateInstance(breathingSFX);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckDistance, groundLayer);
        //isGrounded = _characterController.isGrounded;

        Debug.Log(isGrounded);

        if (Input.GetButtonDown("Jump"))
            StartCoroutine(RequestJump());

        if(Input.GetKeyDown(keyToBark))
        {
            RuntimeManager.PlayOneShot(barkSFX);
        }

        Gravity();

        Jump();

        WalkingMovement();
        
        movementSFX();

        _animator.SetBool("isJumping", !isGrounded);
    }

    private void WalkingMovement()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        movementInput = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movementInput;
        _characterController.Move(movementInput * movementSpeed * Time.deltaTime);

        if (movementInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotateSpeed * Time.deltaTime);
            transform.rotation = targetRotation;

            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void Gravity()
    {
        interpolatecurrentGravityFromJumpingToFalling();

        _velocity.y = _characterController.velocity.y;

        if (isGrounded)
        {
            isJumping = false;
        }

        if (_velocity.y > 0 && !isJumping)
            _velocity.y = 0; //cancels velocity of running upstairs to avoid hops

        _velocity.y += currentGravity * Time.deltaTime;

        if (isGrounded && _velocity.y < 0f)
        {
            _velocity.y = 0f;
        }

        _characterController.Move(new Vector3(0, _velocity.y, 0));      
    }

    private void Jump()
    {
        if (isJumping || !jumpRequest || !isGrounded)
            return;
        Debug.Log("Jump");
        isJumping = true;
        jumpRequest = false;
        currentGravity = jumpForce;
        RuntimeManager.PlayOneShot(jumpSFX);
    }

    private IEnumerator RequestJump()
    {
        StopCoroutine(RequestJump());

        jumpRequest = true;

        yield return new WaitForSeconds(jumpRequestTimer);

        jumpRequest = false;

        yield return null;
    }

    private void interpolatecurrentGravityFromJumpingToFalling()
    {
        if (currentGravity > gravity)
            currentGravity -= jumpForceFadeSpeed * Time.deltaTime;

    }

    void movementSFX()
    {
        //Debug.Log(breathingTimer);
        if (movementInput.magnitude > 0  && isGrounded)
        {
            if (!walkingStarted)
            {
                walkingI.start();
                walkingStarted = true;
            }
            if (!breathingStarted)
            {



                breathingI.start();
                breathingStarted = true;
            }
            if(breathingTimer < 100)
            {
                breathingTimer += Time.deltaTime * breathingNeed;
                breathingI.setParameterByName("TimeWalking", breathingTimer);
            }
        } else
        {
            if (walkingStarted)
            {
                walkingI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                walkingStarted = false;
            }
            if (breathingStarted) { 
            breathingI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            breathingStarted=false;
            }
            if (breathingTimer >= 0)
            {
                breathingTimer -= Time.deltaTime * breathingNeed * 5;
                breathingI.setParameterByName("TimeWalking", breathingTimer);
            }
        }
    }
    private void OnDisable()
    {
        walkingI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        walkingI.release();
        breathingI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        breathingI.release();
    }
}
