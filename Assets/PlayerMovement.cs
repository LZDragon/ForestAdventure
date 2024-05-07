using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController playerController;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Player playerComponent;
    private Vector3 moveInput;
    private Vector3 lookInput;
    private float xmouseSensitivity = 2f;
    private float ymouseSensitivity = 1f;
    float speed;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator.applyRootMotion = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (isAttacking)
        {
            PreformAttack();
        }
        else
        {
            Movement();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }
    void Movement()
    {

        float gravity = -9.8f;
        if (moveInput.magnitude > 0)
        {
            speed = 5f;
            Vector3 offsetMoveDirection = new Vector3();
            if (moveInput.x > 0)
            {
                //Debug.Log("D key is pressed");
                offsetMoveDirection = playerCamera.transform.right;
            }

            else if (moveInput.x < 0)
            {
                offsetMoveDirection = -playerCamera.transform.right;
                //Debug.Log("A key is pressed");
            }

            if (moveInput.z > 0)
            {
                offsetMoveDirection = playerCamera.transform.forward;
                //Debug.Log("W key is pressed");
                
            }
            else if(moveInput.z < 0)
            {
                offsetMoveDirection = -playerCamera.transform.forward;
                //Debug.Log("S key is pressed");
            }
            
            Vector3 movementAmount = (offsetMoveDirection.normalized * (speed * Time.deltaTime)) + new Vector3(0, gravity);
            
            RotatePlayerModel();
            playerController.Move(movementAmount);
        }
        else
        {
            speed = 0;
        }
        playerAnimator.SetFloat("Speed", speed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }

    void RotateCamera()
    {
        playerCamera.transform.Rotate((-lookInput.y* ymouseSensitivity),lookInput.x*xmouseSensitivity,-playerCamera.transform.eulerAngles.z);
        //Vector3 newRotation = new Vector3((moveInput.x * xmouseSensitivity), (moveInput.y * ymouseSensitivity), 0)+ playerCamera.transform.rotation.eulerAngles;

        /*if (newRotation != new Vector3(0, 0, 0))
        {
            playerCamera.transform.rotation.SetLookRotation(new Vector3((moveInput.x * xmouseSensitivity), (moveInput.y * ymouseSensitivity), 0f));
            
        }*/
        //new Vector3((moveInput.x * xmouseSensitivity), (moveInput.y * ymouseSensitivity), 0);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttacking = context.performed;
    }

    void PreformAttack()
    {
        playerAnimator.SetTrigger("Attack");
        isAttacking = false;
        playerComponent.Attack();

    }

    void RotatePlayerModel()
    {
        playerModel.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
    }


    /*public bool IsGrounded()
    {
        LayerMask ground = 1 << 6;
        return Physics.Raycast(transform.position, Vector3.down,0.05f, ground );
    }*/
}
