using Assets.Scripts.Entities.Objects;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Behaviors
{
    internal class BuildingRangable : MonoBehaviour, IRangable
    {
        public float height;
        public float fireDelay;
        public GameObject projectile;
        GameObject currentTarget;
        float localTimer = 0f;

        private void Update()
        {
            if(currentTarget != null)
            {
                localTimer += Time.deltaTime;
                if (localTimer > fireDelay)
                {
                    localTimer = 0f;
                    Vector3 thisObject = transform.position;
                    Instantiate(projectile, new Vector3(thisObject.x, height, thisObject.z), Quaternion.identity).GetComponent<ProjectileObject>().Init(currentTarget, GetComponentInChildren<Detector>().TagName);
                }
            }        
        }

        public float GetDetactLength()
        {
            return 6f;
        }

        public void InitDetactObject(GameObject obj)
        {
            if(currentTarget == null)
            {
                currentTarget = obj;
            }
        }
    }
}
