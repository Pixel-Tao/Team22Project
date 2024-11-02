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
                CharacterManager.Instance.Player.EquipWeapon(CharacterManager.Instance.items[0]);
            }
            if (GUILayout.Button("Crossbow Equip Weapon"))
            {
                CharacterManager.Instance.Player.EquipWeapon(CharacterManager.Instance.items[1]);
            }
            if (GUILayout.Button("Dagger Equip Weapon"))
            {
                CharacterManager.Instance.Player.EquipWeapon(CharacterManager.Instance.items[2]);
            }
            if (GUILayout.Button("Staff Equip Weapon"))
            {
                CharacterManager.Instance.Player.EquipWeapon(CharacterManager.Instance.items[3]);
            }
            if (GUILayout.Button("Sword Equip Weapon"))
            {
                CharacterManager.Instance.Player.EquipWeapon(CharacterManager.Instance.items[4]);
            }
        }
    }
}
