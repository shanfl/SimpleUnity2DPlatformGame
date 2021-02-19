using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public class PlayerController : MonoBehaviour,IDamageable
{
    private Rigidbody2D rb;

    private Animator anim;
    
    public float speed =3;
    public float jumpForce = 4;

    [Header("PlayerState")]
    public int health = 5;
    public bool isDeath = false;

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

    [Header("Attack Setting")]
    public float nextAttack = 0;
    public GameObject bombPrefeb;
    public float attackRate = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = GameManager.instance.LoadHealth();
        UIManager.instance.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            anim.SetBool("isdead", isDeath);
            return;
        }
        //rb.AddForce()
        CheckInput();
        anim.SetBool("isdead", isDeath);
    }

    void FixedUpdate()
    {
        if (isDeath)
        {
            rb.velocity = Vector2.zero;
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            this.Attack();
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

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            GameObject obj = Instantiate(bombPrefeb, transform.position + new Vector3(0,0.6f,0), bombPrefeb.transform.rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.1f, 1.1f), ForceMode2D.Impulse);            
            nextAttack = Time.time + attackRate;
        }
    }

    public void GetHit(int damage)
    {
        // 短暂无敌,不接受伤害
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("hit"))
        {
            return;
        }
        //
        //
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isDeath = true;
        }

        GetComponent<Animator>().SetTrigger("hit");

        UIManager.instance.UpdateHealth(health);
    }




}
