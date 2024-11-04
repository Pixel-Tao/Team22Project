using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    public int poolDefaultCapacity = 20;
    public int poolMaxSize = 100;

    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        CharacterManager.Instance.Init();
        GameManager.Instance.Init();
        PoolManager.Instance.Init(poolDefaultCapacity, poolMaxSize);

        CharacterManager.Instance.LoadPlayer(Defines.JobType.Knight);
        ResourceManager.Instance.Instantiate("WorldMap");
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
        UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
        UIManager.Instance.CloseAllPopupUI();
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}