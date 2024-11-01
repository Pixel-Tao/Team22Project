using UnityEngine;

public class GameScene : SceneBase
{
    public int poolDefaultCapacity = 20;
    public int poolMaxSize = 100;

    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        PoolManager.Instance.Init(poolDefaultCapacity, poolMaxSize);
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
        UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
        UIManager.Instance.CloseAllPopupUI();
        ResourceManager.Instance.Instantiate("WorldMap");
        CharacterManager.Instance.LoadPlayer(Defines.JobType.Knight);
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}