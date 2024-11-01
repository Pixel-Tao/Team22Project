
public class CharacterManager : Singleton<CharacterManager>
{
    private Player player;
    public Player Player { get { return player; } }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void AddItem(ItemSO itemSO)
    {
        if (itemSO.itemType == Defines.ItemType.Resource)
        {
            // 자원 증가
        }
        else
        {
            Player.AddItem(itemSO);
        }
    }
}

