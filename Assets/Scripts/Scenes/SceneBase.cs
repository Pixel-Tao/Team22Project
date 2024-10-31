using UnityEngine;

/// <summary>
/// 모든 씬에서 필수로 상속받아 사용
/// </summary>
public abstract class SceneBase : MonoBehaviour
{
    private void Start()
    {
        OnSceneLoad();
    }
    private void OnDestroy()
    {
        OnSceneUnloaded();
    }

    /// <summary>
    /// 씬에 로드할 로직을 수행
    /// </summary>
    protected abstract void OnSceneLoad();
    /// <summary>
    /// Scene이 OnDestroy 될 때 호출
    /// </summary>
    protected abstract void OnSceneUnloaded();

}
