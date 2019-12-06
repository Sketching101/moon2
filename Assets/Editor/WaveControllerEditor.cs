using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(WaveController))]
public class WaveControllerEditor : Editor
{
    string name = "";

    public WaveController current
    {
        get
        {
            return (WaveController)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Create WaveData", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Wave Name");
        GUILayout.FlexibleSpace();
        name = EditorGUILayout.TextField(name);
        GUILayout.BeginVertical();
        if (GUILayout.Button("Create WaveData Object"))
        {
            current.CreateWaveData(name);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}
