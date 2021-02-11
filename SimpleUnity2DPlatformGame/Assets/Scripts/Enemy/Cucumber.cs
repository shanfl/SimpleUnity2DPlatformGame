using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cucumber : Enemy,IDamageable
{
    // Start is called before the first frame update

    public override void SkillAction()
    {
        Debug.Log("Cucumber skill action");
        this.anim.SetTrigger("skill");
    }

    // anim event
    public void SetOff()  
    {
        if (targetPoint && targetPoint.CompareTag("Bomb"))
        {
            //anim.pl
            //targetPoint.GetComponent<Bomb>().TurnOff();
        }
    }

    public void GetHit(float damage)
    {
        
        health -= damage;
        if(health < 0)
        {
            health = 0;
            isdeath = true;
        }
        GetComponent<Animator>().SetTrigger("hit");
    }
}
