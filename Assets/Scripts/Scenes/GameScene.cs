using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    public int poolDefaultCapacity = 20;
    public int poolMaxSize = 100;

    public List<ItemSO> items;

    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        CharacterManager.Instance.LoadPlayer(Defines.JobType.Knight);
        ResourceManager.Instance.Instantiate("WorldMap");
        PoolManager.Instance.Init(poolDefaultCapacity, poolMaxSize);
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
        UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
        UIManager.Instance.CloseAllPopupUI();
        CharacterManager.Instance.items = items;
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}