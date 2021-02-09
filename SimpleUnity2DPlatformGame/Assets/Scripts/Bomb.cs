using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public float startTime;
    public float waitTime;

    [Header("Check")]
    public float radius;
    public Vector3 offset;
    public LayerMask targetLayerMask;
    public float bombForce = 3;
    public float time = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if (Time.time > startTime + waitTime)
        {
            anim.Play("bomb_explotion");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }

    public void Explotion()
    {
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerMask);

        foreach(var item in aroundObjects)
        {
            Vector3 pos = item.transform.position -transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((pos+ Vector3.up)*bombForce, ForceMode2D.Impulse);
        }
    }

    public void OnDestroyWhenFrameEnd()
    {
        Destroy(this.gameObject);
    }


    public void TurnOff()
    {
        anim.Play("bomb_off");
    }
}
