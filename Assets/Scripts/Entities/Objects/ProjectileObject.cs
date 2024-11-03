using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Objects
{
    public class ProjectileObject : MonoBehaviour
    {
        public float speed;
        public int damage;
        public string tagName;
        private GameObject currentTarget;
        Vector3 dirToTarget;
        
        public void Init(GameObject target, string name)
        { 
            currentTarget = target;
            tagName = name;
            dirToTarget = currentTarget.transform.position - this.transform.position;
        }

        public void Update()
        {
            UpdateProjectile();
        }

        void UpdateProjectile()
        {
            if (currentTarget != null)
            {
                Quaternion targetRot = Quaternion.LookRotation(dirToTarget.normalized);
                transform.rotation = targetRot;

                this.transform.position += dirToTarget.normalized * speed * Time.deltaTime;
            }
            
            if (this.transform.position.y < 0 || currentTarget == null) DestroySelf();
        }

        void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(tagName))
            {
                DestroySelf();
                other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
                other.gameObject.GetComponent<IDamageable>().KnockBack(this.transform);
            }
        }

    }
}
