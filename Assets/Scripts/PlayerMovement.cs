using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 7.0f;
    [SerializeField] float gravity = -15.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.05f;
    [SerializeField] float jumpHeight = 2.0f;
    [SerializeField] float sprintMultiplier = 1.5f;
    float velocityY = 0.0f;
    bool isJumping = false;
    bool isSprinting = false;
    CharacterController controller = null;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isJumping = Input.GetButtonDown("Jump");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector2 targetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
        {
            if(isJumping)
            {
                velocityY = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            else
            {
                velocityY = 0.0f;
            }
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        if(isSprinting)
        {
            velocity *= sprintMultiplier;
        }

        controller.Move(velocity * Time.deltaTime);        
    }
}
