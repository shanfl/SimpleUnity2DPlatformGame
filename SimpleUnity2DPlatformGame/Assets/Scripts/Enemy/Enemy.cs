using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("AttackSetting")]
    public float nextAttack = 0;
    public float rateAttack = 1;
    public float attackRange, skillRange;  // 攻击距离 普通攻击+技能攻击


    [Header("EnmeyState")]
    public int health = 5;
    public bool isdeath = false;
    
    [Header("Movement")]
    public float speed = 1;
    public Transform PointA, PointB;
    public Transform targetPoint;
    public List<Transform> attackList = new List<Transform>();
    //
    public int animState = 0;
    public Animator anim;
    EnemyBaseState currentState;
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    // Start is called before the first frame update

    GameObject alarmSign = null;
    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;

    }

    public void Awake()
    {
        Debug.Log("Awake");
        Init();
    }
    void Start()
    {
        Debug.Log("start .............");
        this.TransitionToState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isdeath", isdeath);

        if (isdeath)
            return;

        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FlipDirection();
    }

    // 对玩家的攻击
    public virtual void AttackAction()
    {
        
        if(Vector2.Distance(transform.position,targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                Debug.Log("普通攻击");
                anim.SetTrigger("attack");
                nextAttack = Time.time + rateAttack;
            }                
        }
    }

    // 对炸弹使用技能
    public virtual void SkillAction()
    {
        
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                Debug.Log("技能攻击");
                nextAttack = Time.time + rateAttack;
            }
        }
    }

    public void FlipDirection()
    {
        if (transform.position.x > targetPoint.position.x)
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0f,180f,0f));
    }

    public void SwitchPoint()
    {
        if(Mathf.Abs(PointA.position.x - transform.position.x) > Mathf.Abs(transform.position.x - PointB.position.x))
        {
            targetPoint = PointA;
        }
        else
        {
            targetPoint = PointB;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!attackList.Contains(collision.transform))
        {
            attackList.Add(collision.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        attackList.Remove(coll.transform);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        StartCoroutine(OnAlarm());
    }

    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(1, 2, 1));
    }
    
 

}
