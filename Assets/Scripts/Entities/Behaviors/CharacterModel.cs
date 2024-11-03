using UnityEngine;

/// <summary>
/// 캐릭터 모델에 장착 가능한 슬롯을 정의하고, 무기를 장착할 수 있도록 도화주는 클래스
/// </summary>
public class CharacterModel : MonoBehaviour
{
    [SerializeField] private Transform rightHandSlot;
    [SerializeField] private Transform headSlot;
    [SerializeField] private Transform leftHandSlot;

    public void InsertRightHandSlot(Transform item)
    {
        item.SetParent(rightHandSlot);
        item.transform.localPosition = Vector3.zero;
    }
    public void ClearRightHandSlot()
    {
        ClearSlot(rightHandSlot);
    }

    public void InsertHeadSlot(Transform item)
    {
        item.SetParent(headSlot);
        item.transform.localPosition = Vector3.zero;
    }
    public void ClearHeadSlot()
    {
        ClearSlot(headSlot);
    }

    public void InsertLeftHandSlot(Transform item)
    {
        item.SetParent(leftHandSlot);
        item.transform.localPosition = Vector3.zero;
    }
    public void ClearLeftHandSlot()
    {
        ClearSlot(leftHandSlot);
    }

    private void ClearSlot(Transform slot)
    {
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }
    }
    public void AllClearSlot()
    {
        ClearSlot(rightHandSlot);
        ClearSlot(headSlot);
        ClearSlot(leftHandSlot);
    }
}
