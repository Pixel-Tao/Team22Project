
/// <summary>
/// 오브젝트 상호작용에 필요한 인터페이스
/// 상호작용이 필요한 경우에는 필수로 상속받아야 함.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// 상호작용 가능한 오브젝트 정보 텍스트
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt();
    /// <summary>
    /// 상호작용 가능한 오브젝트를 클릭했을때 실행되는 함수
    /// </summary>
    public void OnInteract();
}
