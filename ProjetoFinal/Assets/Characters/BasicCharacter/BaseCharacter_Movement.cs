using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChar_Movement : MonoBehaviour
{

    public float walkSpeed = 1.0f; // character walk script
    public float currentSpeed = 1.0f; //
    public float maxSpeed = 50.0f; // Max Run speed for character
    public float acceleration = 1.0f; // acceleration rate per update
    public float rotationSpeed = 720.0f; // character rotation speed
    public float jumpHeight = 1.5f; // value is in meters ...i think
    public float gravity = 5.0f; // https://www.youtube.com/watch?v=Gk9a5tQ_NGc
    public bool moveLock = false; // just in case we have to stun the player w/o pausing
    public bool jumpLock = false; // stops player from jumping if not on the ground

    private CharacterController controller; // player controller reference
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f; //em uns tutoriais colocam isso pra manter o personagem no chao se der problema tira pq n sei se é extritamente necessario
        }

        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);

        //handles movement
        if(moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 move =  moveDirection * currentSpeed * Time.deltaTime;
            controller.Move(move);
        }

        //handles jump
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            //calculate jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight  * gravity);
        }
        
        //aplica gravidade
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}