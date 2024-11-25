using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spiral))]
public class SpiralEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spiral spawner = (Spiral)target;
        if(GUILayout.Button("Draw Spiral")) spawner.CreateSpiral();
    }

}