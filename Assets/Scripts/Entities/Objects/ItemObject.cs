using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractableObject, IInteractable
{
    [SerializeField] public float followSpeed = 10f;

    private Transform target;

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
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
    }
}
