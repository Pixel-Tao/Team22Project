using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
        ResourceManager.Instance.Instantiate("WorldMap");
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}