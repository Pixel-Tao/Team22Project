using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractableObject, IInteractable
{
    private float followSpeed;
    public float FollowSpeed { set { followSpeed = value; } }
    private Transform target;
    private Rigidbody rigidbody;
    private Collider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target == null) return;

        Vector3 position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        Vector3 direction = (position - transform.position).normalized * followSpeed * Time.deltaTime;
        float distance = Vector3.Distance(position, target.position);
        transform.position += direction;
        if (distance < 0.1f)
        {
            CharacterManager.Instance.AddItem(data as ItemSO);
            // TODO : 풀링 필요
            Destroy(gameObject);
        }
    }

    public void Flash()
    {
        // 아이템은 굳이 깜빡일 필요가 없음.
    }

    public string GetInteractPrompt()
    {
        return $"{data.displayName}\n{data.description}";
    }

    public void OnInteract(Transform target)
    {
        this.target = target;
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }
}
