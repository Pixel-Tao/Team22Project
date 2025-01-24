using Unity.VisualScripting;
using UnityEngine;

public class TitleScene : SceneBase
{
    private GameObject titleCameraTrack;
    private GameObject worldMap;

    protected override void OnSceneLoad()
    {
        Debug.Log("TitleScene OnSceneLoad");
        UIManager.Instance.Init();

        UIManager.Instance.ShowSceneUI<TitleSceneUI>();
        //worldMap = ResourceManager.Instance.Instantiate("WorldMap");
        // ResourceManager.Instance.Instantiate("DayAndNight");
        titleCameraTrack = ResourceManager.Instance.Instantiate("TitleCameraTrack");
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("TitleScene OnSceneUnloaded");
    }
}