using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController playerctl;
    void Start()
    {
        anim        = this.GetComponent<Animator>();
        rb          = this.GetComponent<Rigidbody2D>();
        playerctl   = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isJumping", playerctl.IsJumping());
        anim.SetFloat("vspeed", rb.velocity.y);

    }
}
