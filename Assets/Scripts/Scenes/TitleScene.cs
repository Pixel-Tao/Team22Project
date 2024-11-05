using UnityEngine;

public class TitleScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        Debug.Log("TitleScene OnSceneLoad");

        UIManager.Instance.ShowSceneUI<TitleSceneUI>();
        GameObject mapParent = new GameObject("MapParent");
        mapParent.transform.position = Vector3.zero;
        GameObject map = ResourceManager.Instance.Instantiate("WorldMap", mapParent.transform);
        map.transform.localPosition = Vector3.zero;
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("TitleScene OnSceneUnloaded");
    }
}