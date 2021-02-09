﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        // 
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            Debug.Log("TransitionToState patrolState");
            //enemy.SwitchPoint();
            enemy.animState = 0;
            enemy.anim.SetInteger("state", enemy.animState);
            enemy.TransitionToState(enemy.patrolState);
        }

        if (enemy.attackList.Count > 0)
            enemy.TransitionToState(enemy.attackState);
    }
}
