using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Player player = (Player)target;
        
        if (GUILayout.Button("Set new setting"))
        {
            player.SetSettings();
        }
    }
}