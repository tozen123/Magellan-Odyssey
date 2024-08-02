using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _playerInput;

    
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] private float turningSpeed = 5f;

    [SerializeField] private FixedJoystick movementJoystick;

    [Header("Effects")]
    public GameObject walkEffectSmoke;
    public Transform playerLowerPart;

    [Header("Animations")]
    public Animator animator;
    

    private void Start()
    {
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
        
    }
    private void Look()
    { 
        if(_playerInput != Vector3.zero)
        {

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var newInput = matrix.MultiplyPoint3x4(_playerInput);


            var relative = (transform.position + newInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turningSpeed * Time.deltaTime);


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

        Vector3 movement = transform.forward * _playerInput.magnitude * movementSpeed * Time.deltaTime;
        _characterController.Move(movement);

        if(walkEffectSmoke != null)
        {
            if (_playerInput.magnitude > 0 && Random.value < 0.1f)
            {
                Instantiate(walkEffectSmoke, playerLowerPart.transform.position, Quaternion.identity);
            }
        }

    }

    
}
