
namespace Defines
{
    public enum ItemType
    {
        None,
        Equipable,
        Consumable,
        Resource
    }

    public enum ConsumeableType
    {
        None,
        Health,
        Mana,
        Stamina,
        Hunger,
        water
    }

    public enum BuildingType
    {
        None,
        Castle,
        House,
        Tower,
        Defence,
        NaturalResources,
        ResourceBuilding,
        Blacksmith,
        Market,
        well

        /* 세부분류
        Castle, //체력, 인구제공
        House, //체력, 인구제공
        Tower, //체력, 공격력 ,인구소모
        Wall, //체력
        Gate, //체력
        GrainTile, //자원제공
        Windmill, //자동제공
        MineTile, //자원제공
        Quarry, //자동제공
        LoggingTile, //자원제공
        Lumbermill, //자동제공
        Blacksmith, //장비제작소
        Market //배고픔을 채움
        */
    }

    public enum TileType
    {
        None,
        Ground,
        Water,
        Road
    }

    public enum ResourceType
    {
        Wood,
        Ore,
        People,
        Food
    }
}