using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Circular))]
public class CircularEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Circular circular = (Circular)target; 
        
        // Call the function.
        if(GUILayout.Button("Draw Objects")) circular.DrawObjectsOnEdgeOfCircle();
    }
}
