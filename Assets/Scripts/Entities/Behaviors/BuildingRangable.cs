using Assets.Scripts.Entities.Objects;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Behaviors
{
    internal class BuildingRangable : MonoBehaviour, IRangable
    {
        public float height;
        public string projectileName;
        //public GameObject projectile;
        
        private GameObject currentTarget;  
        private float localTimer = 0f;
        private float attackDelay;
        private float attackRange;
        private float attackDamage;

        private void Awake()
        {
            BuildSO data = GetComponent<BuildingObject>().buildedSO;
            attackDelay = data.attackDelay;
            attackRange = data.attackRange;
            attackDamage = data.attackPower;
        }

        private void Update()
        {
            if(currentTarget != null && currentTarget.activeSelf)
            {
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
