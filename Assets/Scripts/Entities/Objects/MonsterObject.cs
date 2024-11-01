using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum MOBSTATE
{
    MOVE,
    ATTACK,
}

public class MonsterObject : MonoBehaviour, IDamageable
{
    int standAnimId;
    int attackAnimId;
    int damagedAnimId;
    int deadAnimId;

    public MonsterSO data;
    NavMeshAgent agent;
    Animator animator;
    CircleCollider2D circleCollider;
    MOBSTATE state = MOBSTATE.MOVE;

    private GameObject targetObject;//현재 주시되는
    private GameObject detectObject;//현재 감지된 
    
    //PP
    public GameObject DetectObject
    {
        set 
        {
            if(detectObject == null)
                detectObject = value; 
        }
    }
    
    private float attackTimer = 0;
   
    //FOR TEST
    public GameObject testPlayer;
    public GameObject goalObject;//최종 목적지 //반드시 NULL이 아니어야함!

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        MobInit();
    }

    private void Update()
    {
        switch(state)
        {
            case MOBSTATE.MOVE:
                animator.SetBool(standAnimId, false);
                UpdateMove();
                break;
            case MOBSTATE.ATTACK:
                animator.SetBool(standAnimId, true);
                UpdateAttack();
                break;
        }
    }

    #region MOB BEHAVIOR SEQUENCE
    private void MobInit()
    {
        SetTarget(goalObject.transform);
        attackTimer = data.attackDelay;
        agent.speed = data.speed;

        standAnimId = Animator.StringToHash("isStand");
        attackAnimId = Animator.StringToHash("isAttack");
        damagedAnimId = Animator.StringToHash("isDamaged");
        deadAnimId = Animator.StringToHash("isDead");

        //TODO : goal, player
    }
    private void UpdateMove()
    {
        float playerLength = (testPlayer.transform.position - transform.position).magnitude;

        if (detectObject == null) SetTarget(playerLength < data.detectiveLength ? testPlayer.transform : goalObject.transform);
        else SetTarget(detectObject.transform);

        float xFixable = targetObject.TryGetComponent<BoxCollider>(out BoxCollider temp) ? temp.size.x / 2 : 0;
        if (GetDestLength() < data.attackRange + xFixable) SetState(MOBSTATE.ATTACK);
    }
    private void UpdateAttack()
    {
        float xFixable = targetObject.TryGetComponent<BoxCollider>(out BoxCollider temp) ? temp.size.x / 2 : 0;
        if (GetDestLength() > data.attackRange + xFixable) SetState(MOBSTATE.MOVE);
        
        attackTimer += Time.deltaTime;
        if(attackTimer > data.attackDelay)
        {
            animator.SetTrigger(attackAnimId);
            AttackToTarget();
            attackTimer = 0;
        }
    }
    #endregion

    private Vector3 GetDestPos()
    {
        return targetObject.transform.position;
    }
    private Vector3 GetDestDir()
    {
        return (targetObject.gameObject.transform.position - transform.position).normalized;
    }
    private float GetDestLength()
    {
        if(targetObject != null)
            return (targetObject.transform.position - transform.position).magnitude;

        return float.MaxValue;
    }

    private void SetTarget(Transform target)
    {
        targetObject = target.transform.gameObject; 
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }
    private void SetState(MOBSTATE type)
    {
        agent.isStopped = true;
        state = type;
    }
    private void AttackToTarget()
    {
        Debug.Log("몬스터가" + targetObject.name + "에게 공격을함!!");
        //TODO : ATTACK FRAME
    }

    public void TakeDamage(int damage)
    {
        data.health = Math.Max(0, data.health - damage);
    }

    public void Heal(int heal)
    {
        throw new System.NotImplementedException();
    }

    public void KnockBack(Transform dest)
    {
        throw new NotImplementedException();
    }
}
