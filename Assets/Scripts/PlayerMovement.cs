using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : Health
{
    public float jumpSpd = 7f;

    public Gun[] guns;
    public int levelsIncoming;
    [SerializeField] private Gun currentGun;
    private Animator playerAnimator;
    InputAction shootAction;

    private Keyboard keyboard;
    private PlayerInput playerInput;
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        keyboard = Keyboard.current;
        playerInput = GetComponent<PlayerInput>();
            playerAnimator = GetComponentInChildren<Animator>();
            health = maxHealth;
            shootAction = InputSystem.actions.FindAction("Attack");
    }
    private bool canMove = true;
    private CharacterController characterController;

    /*private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }*/

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    private void PlayerJump()
    {
        if (keyboard.spaceKey.wasPressedThisFrame && grounded && canMove)
        {
            rb.AddForce(Vector3.up * jumpSpd, ForceMode.Impulse);
        }
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        // Debug.Log(health);
        
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

            // asen vaihtoa varten ei ole valmis eikä taida tulla käyttöön
            // /* for (int i = 0; i <guns.Length; i++)
            //     {
            //         if (Input.GetKeyDown((i+1) + "") || Input.GetKeyDown("[" + (i+1) +"]"))
            //         {
            //             EquipGun(i);
            //             break;
            //         }
            //     */
            //what is the condition for isPc?

}
}
