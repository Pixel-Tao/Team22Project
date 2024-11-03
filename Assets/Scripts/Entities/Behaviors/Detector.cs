using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal class Detector : MonoBehaviour
    {
        [SerializeField] string tagName;
        public string TagName { get { return tagName; } }

        private void Start()
        {
            GetComponent<SphereCollider>().radius = transform.parent.GetComponent<IRangable>().GetDetactLength();
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag(tagName))
            {
                transform.parent.GetComponent<IRangable>().InitDetactObject(other.gameObject);
            }
        }
    }
}
