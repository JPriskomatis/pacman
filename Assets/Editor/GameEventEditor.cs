using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();

        GameEvent gameEvent = (GameEvent)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Raise Event"))
        {
            gameEvent.Raise();
        }
    }
}
