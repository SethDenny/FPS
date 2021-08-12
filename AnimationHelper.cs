using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public Animator animator;
    private Vector3 moveDelta;

    private void Update()
    {
        ProcessInputs();

        animator.SetFloat("Horizontal", moveDelta.x);
        animator.SetFloat("Vertical", moveDelta.y);
        animator.SetFloat("Speed", moveDelta.sqrMagnitude);

        
        if (Input.GetButton("Crouch"))
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
        if (Input.GetButton("Run"))
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
        
    }
    void FixedUpdate()
    {
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector2(moveX, moveY).normalized;
    }
}
