using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Objects
{
    public class ProjectileController : MonoBehaviour
    {
        public float speed;
        public float deadTime;
        private float damage;
        private float localTimer;
        private string[] tagName;
        private GameObject currentTarget;
        Vector3 dirToTarget;
        
        public void Init(GameObject target, string[] name, float _damage)
        {
            damage = _damage;
            tagName = name;
            currentTarget = target;
            dirToTarget = currentTarget.transform.position - this.transform.position;
        }

        public void Update()
        {
            UpdateProjectile();
        }

        void UpdateProjectile()
        {
            localTimer += Time.deltaTime;
            if (currentTarget != null)
            {
                Quaternion targetRot = Quaternion.LookRotation(dirToTarget.normalized);
                transform.rotation = targetRot;

                this.transform.position += (dirToTarget + Vector3.up * 0.1f).normalized * speed * Time.deltaTime;
            }
            
            if (this.transform.position.y < 0 || !currentTarget.activeSelf || localTimer > deadTime)
            {
                DestroySelf();
            }
        }

        void DestroySelf()
        {
            localTimer = 0;
            currentTarget = null;
            PoolManager.Instance.Despawn(gameObject);
            //Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach(string str in tagName)
            {
                if (other.CompareTag(str))
                {
                    DestroySelf();
                    other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
                    other.gameObject.GetComponent<IDamageable>().KnockBack(this.transform);
                }
            }
           
        }

    }
}
