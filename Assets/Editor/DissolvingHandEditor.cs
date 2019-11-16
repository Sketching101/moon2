using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(DissolvingHand))]
public class DissolvingHandEditor : Editor
{
    public DissolvingHand current
    {
        get
        {
            return (DissolvingHand)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Populate Arrays"))
            current.PopulateArrays();
        if (GUILayout.Button("Disable Hands"))
            current.ClearMeshesAndColliders();
    }
}
