using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public CharacterController controller;

    Vector3 velocity;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    bool isGrounded;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    // Update is called once per frame
    void Update()
    {

        //pulo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y<0){
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight *-2*gravity); 
        }

        //gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //andar
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

        if(direction.magnitude >= 0.1f){
            //se entrar aqui ta recebendo input


            //
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

            //realiza movimento
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
