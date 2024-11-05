
namespace Defines
{
    public enum ItemType
    {
        None,
        Equipable,
        Consumable,
        Resource
    }

    public enum EquipType
    {
        None,
        Helmet,
        Weapon
    }

    public enum ConsumeableType
    {
        None,
        Health,
        Mana,
        Stamina,
        Hunger,
        Water
    }
    public enum  BuildType
    {
        None,
        Building,
        Environment,
        NaturalObject,
    }
    public enum BuildingType
    {
        None,
        Castle_Red,
        House_A_Red,
        Tower_A_Red,
        Wall_Straight,
        Wall_Corner_A_Inside,
        Wall_Corner_A_Outside,
        Wall_Corner_B_Inside,
        Wall_Corner_B_Outside,
        Wall_Straight_Gate,
        Wall_Corner_A_Gate,
        
        Bridge,
        Watermill_Red,
        Blacksmith_Red,

        //건설한 자원건물(자동으로 인벤으로 들어감)
        Windmill_Red,
        Lumbermill_Red,
        Quarry_Red,
        Market_Red,


    }
    public enum EnvironmentType
    {
        //타일 위에 배치되는 자연물 기능x
        None,
        Dirt,
        Mountain_A,
        Mountain_A_Grass,
        Mountain_A_Grass_Trees,
        Mountain_B,
        Mountain_B_Grass,
        Mountain_B_Grass_Trees,
        Mountain_C,
        Mountain_C_Grass,
        Mountain_C_Grass_Trees,
    }

    public enum NaturalObjectType
    {
        //자연 자원지대(가서 주워야함)
        None,
        GrainLand,
        LoggingArea_A,
        LoggingArea_B,
        MiningArea_A,
        MiningArea_B,
        MiningArea_C,
        Well,
    }

    public enum TileType
    {
        None,
        Ground,
        CantBuild
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
        Buildings,
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

    public enum MOBSTATE
    {
        MOVE,
        ATTACK,
        DEAD,
    }

    public enum MOBTYPE
    {
        WarriorMob,
        RougueMob,
        MageMob,
        END,
    }

    public enum SPAWNSTATE
    {
        WAITING,
        WORKING
    }

   
}