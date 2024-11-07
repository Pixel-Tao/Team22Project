using UnityEngine;

public interface IRangable
{
    /// <summary>
    /// 디텍터로 부터 전달받은 객체를 가공할 수 있습니다.
    /// </summary>
    /// <param name="obj"></param>
    void InitDetactObject(GameObject obj);
    /// <summary>
    /// 디텍터가 객체로 부터 참조할 감지 범위 입니다.
    /// </summary>
    /// <returns></returns>
    float GetDetactLength(); 
}