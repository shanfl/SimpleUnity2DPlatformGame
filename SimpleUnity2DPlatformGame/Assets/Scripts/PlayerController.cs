using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    
    public float speed =3;
    public float jumpForce = 4;

    

    [Header("Ground check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("status check")]
    public bool isGround;
    private bool canJump = false;
    public bool isJumping = false;

    [Header("Fx Object")]
    public GameObject landfx;
    public GameObject jumpfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce()
        CheckInput();
    }

    void FixedUpdate()
    {
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround == true)
        {
            canJump = true;
      

        }
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");  // -1 ~ 1
        rb.velocity = new Vector2(speed*horizontalInput, rb.velocity.y);

        if(horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            isJumping = true;
            // jump fx
            jumpfx.transform.position = gameObject.transform.position + new Vector3(0, -0.45f);
            jumpfx.SetActive(true);
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius,groundLayer);

        if(isGround)
        {
            isJumping = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    
    public bool IsJumping()
    {
        return isJumping;
    }

    public void LandFx()
    {
        landfx.transform.position = transform.position + new Vector3(0, -0.72f) ;
        landfx.SetActive(true);        
    }
}
