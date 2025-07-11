using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMover))]
public class PlayerMoverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerMover playerMover = (PlayerMover)target;
        
        if (GUILayout.Button("Set new setting"))
        {
            playerMover.SetSettings();
        }
    }
}