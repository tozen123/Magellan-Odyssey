using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _playerInput;
    private Vector3 _playerLookAttackInput;

    
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] private float turningSpeed = 5f;

    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private FixedJoystick lookAttackJoystick;

    [Header("Effects")]
    public GameObject walkEffectSmoke;
    public Transform playerLowerPart;

    [Header("Animations")]
    public Animator animator;

    public bool canMove;
    private bool isPlayingWalkingSound;
    private void Start()
    {
        canMove = true;
        isPlayingWalkingSound = false;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetPlayerInput();
        Look();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void GetPlayerInput()
    {
        //_playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _playerInput = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical);
        _playerLookAttackInput = new Vector3(lookAttackJoystick.Horizontal, 0, lookAttackJoystick.Vertical);
        
    }
    private void Look()
    {
        if (!canMove)
        {
            return;
        }
        if(_playerLookAttackInput != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var newInput = matrix.MultiplyPoint3x4(_playerLookAttackInput);


            var relative = (transform.position + newInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turningSpeed * Time.deltaTime);
        }

        if (_playerInput != Vector3.zero)
        {
            if (!isPlayingWalkingSound) // Only play sound if not already playing
            {
                PlayerSoundEffectManager.PlayPlayerWalking();
                isPlayingWalkingSound = true; // Set flag to prevent overlapping
            }

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var newInput = matrix.MultiplyPoint3x4(_playerInput);


            var relative = (transform.position + newInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turningSpeed * Time.deltaTime);
        }
        else
        {
            // Stop the walking sound if player stops moving
            if (isPlayingWalkingSound)
            {
                PlayerSoundEffectManager.PlayPlayerWalkingStop();
                isPlayingWalkingSound = false; // Reset the flag
            }
        }

        if (_playerInput != Vector3.zero)
        {




            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
    private void Move()
    {
        /*
         * _rigidbody.MovePosition(transform.position + (transform.forward * _playerInput.magnitude) * movementSpeed * Time.deltaTime);
         */

        /*
         * Vector3 movement = transform.forward * _playerInput.magnitude * movementSpeed * Time.deltaTime;
        */

        if (canMove)
        {
            Vector3 movement = transform.forward * _playerInput.magnitude * movementSpeed * Time.deltaTime;
            _characterController.Move(movement);

        }
       

        if(walkEffectSmoke != null)
        {
            if (_playerInput.magnitude > 0 && Random.value < 0.1f)
            {
                Instantiate(walkEffectSmoke, playerLowerPart.transform.position, Quaternion.identity);
            }
        }

    }

    
}
