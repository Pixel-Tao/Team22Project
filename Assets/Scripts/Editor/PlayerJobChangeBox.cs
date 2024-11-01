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
        }
    }
}
