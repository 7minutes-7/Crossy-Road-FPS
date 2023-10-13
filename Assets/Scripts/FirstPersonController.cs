using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera playerCamera;
    private CharacterController characterController;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform Head;

    [Header("Movement Parameters")]
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpDistance = 2f;

    private bool jumping = false;
    private float jumpStartVelocityY;


    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 30.0f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 50.0f;
    [SerializeField] float sensitivity = 15f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    Vector3 lookDirection = Vector3.forward;
    float z_angle = 0f;
    float x_angle = 0f;

    const int GROUND_LAYER = 6;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        rb = GetComponent<Rigidbody>();

        jumpStartVelocityY = -jumpDuration * Physics.gravity.y / 2;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    bool IsGrounded()
    {
        return Physics.CheckBox(groundCheck.position, new Vector3(1, 0.05f, 1), groundCheck.rotation, ground);
        //return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    private void HandleMovementInput()
    {
        lookDirection = playerCamera.transform.TransformDirection(Vector3.forward).normalized;
        z_angle = Mathf.Acos(Vector3.Dot(lookDirection, Vector3.forward));
        x_angle = Mathf.Acos(Vector3.Dot(lookDirection, Vector3.right));

        float jumpForwardVelocity = jumpDistance / jumpDuration * (float)1.01492935711076;
        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (z_angle <= Mathf.Deg2Rad * 45f)
                {
                    StartCoroutine(Jump(Vector3.forward * jumpDistance));
                }

                else if (z_angle >= Mathf.Deg2Rad * 135f)
                {
                    StartCoroutine(Jump(-Vector3.forward * jumpDistance));
                }
                else if (x_angle < Mathf.Deg2Rad * 45f)
                {
                    StartCoroutine(Jump(Vector3.right * jumpDistance));
                }
                else if (x_angle > Mathf.Deg2Rad * 135f)
                {
                    StartCoroutine(Jump(-Vector3.right * jumpDistance));
                }
            }
        }
    }

    private void HandleMouseLook()
    {
        
        rotationY += Input.GetAxis("Mouse X") * sensitivity; // rotation of the y axis with the change of Mouse X
        float tempRotationX = rotationX + Input.GetAxis("Mouse Y") * (-1) * sensitivity;
        if (tempRotationX<=upperLookLimit && tempRotationX >= -lowerLookLimit)
        {
            rotationX = tempRotationX;
        }

        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        playerCamera.transform.localEulerAngles = new Vector3(rotationX,0 , 0);
        Head.localEulerAngles = new Vector3(rotationX, 0, 0); ;
    }

    private IEnumerator Jump(Vector3 direction)
    {
        jumping = true;
        Vector3 startPoint = transform.position;
        Vector3 targetPoint = startPoint + direction;
        float time = 0;
        float jumpProgress = 0;
        float velocityY = jumpStartVelocityY;
        float height = startPoint.y;

        while (jumping)
        {
            if (!((jumpProgress > 0.7) && IsGrounded()))
            {
                jumpProgress = time / jumpDuration;

                if (jumpProgress > 1)
                {
                    jumping = false;
                    jumpProgress = 1;
                }

                Vector3 currentPos = Vector3.Lerp(startPoint, targetPoint, jumpProgress);
                currentPos.y = height;
                transform.position = currentPos;

                //Wait until next frame.
                yield return null;

                height += velocityY * Time.deltaTime;
                velocityY += Time.deltaTime * Physics.gravity.y;
                time += Time.deltaTime;
            }
            else
            {
                jumping = false;
                Debug.Log(jumpProgress);
            }
        }
        if (jumpProgress > 0.6 && jumpProgress < 0.8)  // on raft
        {
            transform.position = targetPoint + new Vector3(0, .35f , 0);
        }
        else
        {
            transform.position = targetPoint;
        }

        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == GROUND_LAYER)
        {
            rb.velocity = Vector3.zero;
        }
    }

}
