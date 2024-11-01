using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
        UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
        UIManager.Instance.CloseAllPopupUI();
        ResourceManager.Instance.Instantiate("WorldMap");
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}