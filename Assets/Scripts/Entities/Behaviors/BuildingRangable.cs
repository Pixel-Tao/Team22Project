using Assets.Scripts.Entities.Objects;
using UnityEngine;

namespace Assets.Scripts.Entities.Behaviors
{
    internal class BuildingRangable : MonoBehaviour, IRangable
    {
        private GameObject currentTarget;
        public float height;
        public string projectileName;
        
        private float localTimer = 0f;
        private float attackDelay;
        private float attackRange;
        private float attackDamage;

        private void Awake()
        {
            BuildingSO data = GetComponent<BuildingObject>().BuildingSO;
            if (data == null) return;

            attackDelay = data.attackDelay;
            attackRange = data.attackRange;
            attackDamage = data.attackPower;
        }

        private void Update()
        {
            if(currentTarget != null && currentTarget.activeSelf)
            {
                if (currentTarget.TryGetComponent<Monster>(out Monster mob) && mob.Health <= 0) return;

                localTimer += Time.deltaTime;
                if (localTimer > attackDelay && 
                    ((currentTarget.transform.position - this.gameObject.transform.position).magnitude <= attackRange))
                {
                    localTimer = 0f;
                    Vector3 thisObject = this.gameObject.transform.position;
                    GameObject spawnObj = PoolManager.Instance.SpawnProjectile(projectileName, this.transform);
                    spawnObj.transform.position = new Vector3(thisObject.x, height, thisObject.z);
                    if(spawnObj.TryGetComponent<ProjectileController>(out ProjectileController obj))
                    {
                        obj.Init(currentTarget, GetComponentInChildren<Detector>().TagNames, attackDamage);
                    }
                }
                else if((currentTarget.transform.position - this.gameObject.transform.position).magnitude > attackRange)
                {
                    currentTarget = null;
                }
            }        
        }

        public float GetDetactLength()
        {
            return attackRange;
        }

        public void InitDetactObject(GameObject obj)
        {
            if(currentTarget == null || !currentTarget.activeSelf)
            {
                currentTarget = obj;
            }
        }
    }
}
