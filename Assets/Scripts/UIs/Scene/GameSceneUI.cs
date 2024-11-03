
using UnityEngine;

public class GameSceneUI : UIScene
{
    // 게임씬에서 사용할 고정 UI

    [SerializeField] private Transform quickSlots;

    private void Start()
    {
        InitQuickSlots();
    }

    private void InitQuickSlots()
    {
        for(int i = 0; i < CharacterManager.Instance.QuickSlotCount; i++)
        {

        }

    }
}
