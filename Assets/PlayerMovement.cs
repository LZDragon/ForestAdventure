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
            Vector3 offsetMoveDirection =
                Vector3.RotateTowards(moveInput, playerCamera.transform.forward, 2 * Mathf.PI, 0);
            Vector3 movementAmount = (offsetMoveDirection * (speed * Time.deltaTime)) + new Vector3(0, gravity);
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

    }


    /*public bool IsGrounded()
    {
        LayerMask ground = 1 << 6;
        return Physics.Raycast(transform.position, Vector3.down,0.05f, ground );
    }*/
}
