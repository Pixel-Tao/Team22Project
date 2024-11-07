using Assets.Scripts.Entities.Objects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable, IRangable
{
    //VAL
    private int     attackAnimId;
    private int     damagedAnimId;
    private int     deadAnimId;
    
    private float   health;
    private float   attackTimer = 0f;
    private float   xFixable    = 0f;

    //Components
    [SerializeField] MonsterSO data;
    private Rigidbody rb;
    public NavMeshAgent agent;
    private Animator animator;
    private CircleCollider2D circleCollider;
    private Defines.MOBSTATE state = Defines.MOBSTATE.MOVE;

    //OBJECTS
    private GameObject targetObject;
    private GameObject detectObject;
    private GameObject playerObject;
    private GameObject destObject;

    //PP
    public MonsterSO Data
    {
        get { return data; }
    }
    public float Health { get { return health; } }

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
    /// <summary>
    /// 몬스터의 데이터를 초기화 합니다.
    /// </summary>
    private void MobInit()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        SetTarget(GameManager.Instance.Goal.transform);
        attackTimer     = data.attackDelay;
        agent.speed     = data.speed;

        attackAnimId    = Animator.StringToHash("isAttack");
        damagedAnimId   = Animator.StringToHash("isDamaged");
        deadAnimId      = Animator.StringToHash("isDead");

        playerObject    = CharacterManager.Instance.Player.gameObject;
        destObject      = GameManager.Instance.Goal.gameObject;
        health          = data.health;
    }
    private void UpdateMove()
    {
        float playerLength = (playerObject.transform.position - transform.position).magnitude;

        if (detectObject == null || !detectObject.activeSelf) SetTarget(playerLength < data.detectiveLength ? playerObject.transform : destObject.transform);
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
        if (detectObject != null && !detectObject.activeSelf)
        {
            targetObject = null;
            detectObject = null;
        }
        
        if (targetObject == null)
        {
            SetState(Defines.MOBSTATE.MOVE);
            return;
        }
        else if (targetObject != null)
        {
            if (GetDestLength() > data.attackRange + xFixable) SetState(Defines.MOBSTATE.MOVE);
            rb.transform.LookAt(targetObject.transform);
        }

        attackTimer += Time.deltaTime;
        if (attackTimer > data.attackDelay)
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
        if (targetObject != null)
            return (targetObject.transform.position - transform.position).magnitude;

        return float.MaxValue;
    }

    /// <summary>
    /// 목표 지점을 설정하고 이동 명령을 수행합니다.
    /// </summary>
    /// <param name="target"></param>
    private void SetTarget(Transform target)
    {
        targetObject = target.transform.gameObject;
        if (!agent.enabled) agent.enabled = true;
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }
    /// <summary>
    /// 현재 객체의 상태를 변화 시킵니다 (이동, 공격)
    /// </summary>
    /// <param name="type"></param>
    private void SetState(Defines.MOBSTATE type)
    {
        agent.isStopped = true;
        state = type;
    }
    /// <summary>
    /// 현재 타켓 오브젝트를 공격합니다, 타겟이 없으면 작동하지 않습니다.
    /// </summary>
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
    /// <summary>
    /// 몬스터 객체를 풀로 반환합니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DespawnObject()
    {
        SetState(Defines.MOBSTATE.DEAD);
        animator.SetTrigger(deadAnimId);
        yield return new WaitForSeconds(2f);
        
        SetState(Defines.MOBSTATE.MOVE);
        health = data.health;
        detectObject = null;
        agent.enabled = false;
        PoolManager.Instance.Despawn(this.gameObject);
    }

    #region INTERFACES
    public void TakeDamage(float damage)
    {
        if (health > 0)
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
    public void InitDetactObject(GameObject obj)
    {
        if (detectObject == null && !obj.TryGetComponent<Player>(out Player temp))
            detectObject = obj;
    }
    public float GetDetactLength()
    {
        return data.detectiveLength;
    }
    #endregion
}
