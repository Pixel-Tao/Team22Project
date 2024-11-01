using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal class Detector : MonoBehaviour
    {
        [SerializeField] string tagName;
        MonsterObject parentMob;
        
        private void Awake()
        {
            parentMob = transform.parent.GetComponent<MonsterObject>();
            GetComponent<SphereCollider>().radius = parentMob.data.detectiveLength;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(tagName))
            {
                transform.parent.GetComponent<MonsterObject>().DetectObject = other.gameObject;
            }
        }
    }
}
