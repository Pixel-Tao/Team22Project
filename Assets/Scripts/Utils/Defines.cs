
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
        Road,
        Tree,
        Mine,
        Farm
    }

    public enum JobType
    {
        None,
        Babarian,
        Knight,
        Mage,
        Rogue
    }

    public enum ResourceType
    {
        Wood,
        Ore,
        People,
        Food
    }

    // 장비에 따라 공격모션 달리하기 위한 애니메이터 레이어 Enum
    // Animator Layer 순서와 동일함
    public enum CharacterAnimCombatLayerType
    {
        Base_Layer, // Base Layer
        // Hit Layer
        Melee_1H_Layer = 2,
        Melee_2H_Layer,
        Ranged_1H_Layer,
        Ranged_2H_Layer,
        Spellcasting_Layer
    }

    public enum CharacterMoveStepType
    {
        Idle,
        Forward,
        Backward,
        Left,
        Right,
    }

    public enum SODataType
    {
        None,
        Building,
        Job,
        Item,
        MobData,
        Tile
    }

    public enum SOItemDataType
    {
        None,
        Consumable,
        Helmet,
        Resource,
        Weapon,
    }
}