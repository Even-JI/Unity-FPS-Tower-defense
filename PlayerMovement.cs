using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES
    public float moveSpeed; /*SerializeField gj�r at du kan endre verdier i unity editor(inspector)*/
    public float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;


    //public GameObject Weapon;
    //private GameObject handLocat;


    //REFRENCES
    private CharacterController controller; /*Referanse til character controlleren*/
    public bool isWalking;
    public bool isRunning;
    public bool crouching;
    private bool aiming;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }


        float moveZ = Input.GetAxis("Vertical"); /*Registrerer om du trykker fremover eller bakover*/
        float moveX = Input.GetAxis("Horizontal");


        //moveDirection = new Vector3(0, 0, moveZ); /*Sier hvilke vei du skal g�*/
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;


        if (isGrounded)
        {

            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.LeftControl))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                CrouchDown();
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                CrouchUp();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            moveDirection *= moveSpeed;
        }


        controller.Move(moveDirection * Time.deltaTime); /*Uansett hvor mange frames spillet ditt kj�rer i s� er bevegelsen like kjapp*/

        velocity.y += gravity * Time.deltaTime; /*calculates gravity*/
        controller.Move(velocity * Time.deltaTime); /*applyes gravity to character*/
    }

    private void Idle()
    {
        //armsAnimator.SetBool("IsRunning", false);
        isWalking = false;
        isRunning = false;
    }

    private void Walk()
    {
        if (crouching == true) 
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        isWalking = true;
        isRunning = false;
        //armsAnimator.SetBool("IsRunning", false);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        isWalking = false;
        isRunning = true;
        //armsAnimator.SetBool("IsRunning", true);
    }

    private void CrouchDown()
    {
        crouching = true;
        Camera cam = GetComponentInChildren<Camera>();
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 1, cam.transform.position.z);
    }
    private void CrouchUp()
    {
        crouching = false;
        Camera cam = GetComponentInChildren<Camera>();
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 1, cam.transform.position.z);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        moveSpeed = walkSpeed;
    }
}
