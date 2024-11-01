using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interaction))]
public class InteractionModeToggleButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Toggle Mode"))
            {
                GameManager.Instance.ToggleBuildMode();
            }
        }
    }
}
