using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spawner spawner = (Spawner)target;
        if(GUILayout.Button("Spawn Objects")) spawner.SpawnObjects();
    }

}