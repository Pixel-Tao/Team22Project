using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerJobChangeBox : Editor
{
    int index;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            string[] jobs = System.Enum.GetNames(typeof(Defines.JobType));
            
            for(int i = 0; i < jobs.Length; i++)
            {
                Defines.JobType jobType = (Defines.JobType)i;
                if (jobType == Defines.JobType.None) continue;
                if (GUILayout.Button($"{jobType} Change"))
                {
                    CharacterManager.Instance.JobChange(jobType);
                }
            }
            
            if (GUILayout.Button("Axe Equip Weapon"))
            {
                EquipWeapon(GetItemSO("SO_Item_Axe"));
            }
            if (GUILayout.Button("Crossbow Equip Weapon"))
            {
                EquipWeapon(GetItemSO("SO_Item_Crossbow"));
            }
            if (GUILayout.Button("Dagger Equip Weapon"))
            {
                EquipWeapon(GetItemSO("SO_Item_Dagger"));
            }
            if (GUILayout.Button("Staff Equip Weapon"))
            {
                EquipWeapon(GetItemSO("SO_Item_Staff"));
            }
            if (GUILayout.Button("Sword Equip Weapon"))
            {
                EquipWeapon(GetItemSO("SO_Item_Sword"));
            }
        }
    }

    private void EquipWeapon(ItemSO itemSO)
    {
        CharacterManager.Instance.Player.EquipWeapon(itemSO);
    }

    private ItemSO GetItemSO(string name)
    {
        return ResourceManager.Instance.GetSOItemData<ItemSO>(name, Defines.SOItemDataType.Weapon);
    }
}
