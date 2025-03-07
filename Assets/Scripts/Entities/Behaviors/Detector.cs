using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] string[] tagNames;
    public string[] TagNames { get { return tagNames; } }

    private void Start()
    {
        GetComponent<SphereCollider>().radius = transform.parent.GetComponent<IRangable>().GetDetactLength();
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (string tag in tagNames)
        {
            if (other.CompareTag(tag) && other.gameObject.activeSelf)
            {
                transform.parent.GetComponent<IRangable>().InitDetactObject(other.gameObject);
            }
        }

    }
}
