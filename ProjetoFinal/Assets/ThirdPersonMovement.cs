using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public CharacterController controller;

    Vector3 velocity;
    Vector3 lastMoveDir;
    Vector3 dashVelocity;
    public float speed = 6f;
    public float chargedSpeed = 1f;
    public float dashSpeed = 0f;
    float dashingTime = 0f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    bool isGrounded;
    bool canDouble = true;
    bool canAirDash = true;
    bool dashing = false;
    bool airDashing = false;

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

        if (isGrounded && (!canDouble || !canAirDash))
        {
            canDouble = true;
            canAirDash = true;
            airDashing = false;
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight *-2*gravity); 
        }

        if (!isGrounded && Input.GetButtonDown("Jump") && canDouble)
        {
            velocity.y = Mathf.Sqrt(jumpHeight  * -2*gravity);
            canDouble = false;
        }

        //gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //andar
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

        if (isGrounded && Input.GetButton("Dash"))
        {
            if (!dashing)
            {
                dashing = true;
            }
            if (chargedSpeed < (speed*2))
            {
                chargedSpeed += speed/30f;
            }
            Debug.Log(chargedSpeed);
        }

        if (isGrounded && Input.GetButtonUp("Dash"))
        {
            dashSpeed = chargedSpeed;
            chargedSpeed = 1f;
            Debug.Log(dashSpeed);
        }

        if (!isGrounded && Input.GetButtonDown("Dash") && canAirDash)
        {
            if (!airDashing)
            {
                airDashing = true;
                canAirDash = false;
            }
            velocity.y = 0f;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            lastMoveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
        }

        if(direction.magnitude >= 0.1f){
            //se entrar aqui ta recebendo input


            //
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;

            //realiza movimento
            if (!dashing && !airDashing)
            {
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else if (dashing){
                controller.Move(moveDir.normalized * dashSpeed * Time.deltaTime);
                if (dashSpeed > 0f)
                    dashingTime += Time.deltaTime;
                if (dashingTime >= 3){
                    dashing = false;
                    dashingTime = 0f;
                    dashSpeed = 0f;
                }
            }
            else if (airDashing)
            {
                controller.Move(lastMoveDir.normalized * (speed*1.5f) * Time.deltaTime);
            }
        }
        else
        { 
            if (dashing){
                dashing = false;
                dashingTime = 0f;
                dashSpeed = 0f;
            }
        }
    }
}
