using Assets.Scripts.Entities.Objects;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable, IRangable
{
    private int attackAnimId;
    private int damagedAnimId;
    private int deadAnimId;
    private float health;
    private float attackTimer = 0;
    private float xFixable = 0f;

    [SerializeField] MonsterSO data;
    private NavMeshAgent agent;
    private Animator animator;
    private CircleCollider2D circleCollider;
    private Rigidbody rb;
    private Defines.MOBSTATE state = Defines.MOBSTATE.MOVE;

    private GameObject targetObject;//현재 주시되는
    private GameObject detectObject;//현재 감지된
    private GameObject playerObject;
    private GameObject destObject;//최종 목적지 //반드시 NULL이 아니어야함!

    //PP
    public MonsterSO Data
    {
        get { return data; }
    }

    #region UNITY EVENTS
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        MobInit();
    }
    private void Update()
    {
        switch (state)
        {
            case Defines.MOBSTATE.MOVE:
                UpdateMove();
                break;
            case Defines.MOBSTATE.ATTACK:
                UpdateAttack();
                break;
            case Defines.MOBSTATE.DEAD:
                //TODO : AFTER DEAD
                break;
        }
    }
    #endregion
    #region MOB BEHAVIOR SEQUENCE
    private void MobInit()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        SetTarget(GameManager.Instance.Goal.transform);
        attackTimer = data.attackDelay;
        agent.speed = data.speed;

        attackAnimId = Animator.StringToHash("isAttack");
        damagedAnimId = Animator.StringToHash("isDamaged");
        deadAnimId = Animator.StringToHash("isDead");

        playerObject = CharacterManager.Instance.Player.gameObject;
        destObject = GameManager.Instance.Goal.gameObject;
        health = data.health;
    }
    private void UpdateMove()
    {
        float playerLength = (playerObject.transform.position - transform.position).magnitude;

        if (detectObject == null) SetTarget(playerLength < data.detectiveLength ? playerObject.transform : destObject.transform);
        else SetTarget(detectObject.transform);

        xFixable = targetObject.TryGetComponent<BoxCollider>(out BoxCollider temp) ? temp.size.x / 2 : 0;
        if (GetDestLength() <= data.attackRange + xFixable)
        {
            attackTimer = data.attackDelay;
            SetState(Defines.MOBSTATE.ATTACK);
        } 
    }
    private void UpdateAttack()
    {
        if(targetObject == null)
        {
            SetState(Defines.MOBSTATE.MOVE);
            return;
        }      
        else if(targetObject != null)
        {
            if (GetDestLength() > data.attackRange + xFixable) SetState(Defines.MOBSTATE.MOVE);
            rb.transform.LookAt(targetObject.transform);
        }

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
    private void SetState(Defines.MOBSTATE type)
    {
        agent.isStopped = true;
        state = type;
    }
    private void AttackToTarget()
    {
        if (targetObject == null) return;      
        SoundManager.Instance.PlayOneShotPoint(data.projectileSound, transform.position);
        GameObject temp = PoolManager.Instance.SpawnProjectile(data.projectileName);
        temp.transform.position = this.gameObject.transform.position + (Vector3.up * 0.4f);
        string[] tags = GetComponentInChildren<Detector>().TagNames;
        temp.GetComponent<ProjectileController>().Init
        (targetObject, 
        GetComponentInChildren<Detector>().TagNames, 
        data.attackDamage, 
        !data.isRangedWeapon);
    }
    private IEnumerator DespawnObject()
    {
        SetState(Defines.MOBSTATE.DEAD);
        animator.SetTrigger(deadAnimId);
        yield return new WaitForSeconds(2f);
        SetState(Defines.MOBSTATE.MOVE);
        health = data.health;
        detectObject = null;
        PoolManager.Instance.Despawn(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health = Math.Max(0, health - damage);
            animator.SetTrigger(damagedAnimId);
            SoundManager.Instance.PlayOneShot("Damaged");

            if (health == 0)
            {
                StartCoroutine(DespawnObject());
            }
        }     
    }
    public void Heal(int heal)
    {
        throw new System.NotImplementedException();
    }
    public void KnockBack(Transform dest)
    {
    }

    public void InitDetactObject(GameObject obj)
    {
        if(detectObject == null && !obj.TryGetComponent<Player>(out Player temp)) 
            detectObject = obj;
    }
    public float GetDetactLength()
    {
        return data.detectiveLength;
    }
}
