using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cucumber : Enemy
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

    }
}
