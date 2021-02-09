using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enmey)
    {
        Debug.Log("Attack state 发现敌人");
        enmey.animState = 2;
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.attackList.Count == 0)
            enemy.TransitionToState(enemy.patrolState);

        if(enemy.attackList.Count >= 1)
        {
            enemy.targetPoint = enemy.attackList[0];
           

            for (int i = 1;i < enemy.attackList.Count; i++)
            {
                float d = Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x);
                float t = Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x);
                if (d < t)
                {
                    enemy.targetPoint = enemy.attackList[i];
                }

            }

            Debug.Log("tag:" + enemy.targetPoint.tag);
            if (enemy.targetPoint.CompareTag("Bomb"))
            {
                Debug.Log("skill action ---------->");
                enemy.SkillAction();
            }

            if (enemy.targetPoint.CompareTag("Player"))
            {
                enemy.AttackAction();
                //return;
            }

            enemy.MoveToTarget();
        }
    }
}
