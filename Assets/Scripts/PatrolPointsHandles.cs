using UnityEngine;
using UnityEditor;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(KnightPatrol))]
public class PatrolPointsHandles : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        var t = target as KnightPatrol;
        var tr = t.transform;
        var pos = tr.position;
        // display an orange disc where the object is
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(pos, tr.up, 1.0f);
        // display object "value" in scene
        GUI.color = color;
    }
}
