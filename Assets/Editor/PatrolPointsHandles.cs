using UnityEditor;
using UnityEngine;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(KnightPatrol))]
public class PatrolPointsHandles : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        KnightPatrol patrol = (KnightPatrol)target;
        for (int i = 0; i < patrol.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPoint = Handles.PositionHandle(patrol.Points[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(patrol, "Moved patrol point");
                patrol.Points[i] = newPoint;
            }
        }
    }
}