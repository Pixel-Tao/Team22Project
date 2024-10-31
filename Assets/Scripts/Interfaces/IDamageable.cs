
/// <summary>
/// 생명력 회복, 피해를 입는 등 생명력 관련 인터페이스
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// 데미지 받았을때 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage);
    /// <summary>
    /// 체력을 회복할때
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(int heal);
}