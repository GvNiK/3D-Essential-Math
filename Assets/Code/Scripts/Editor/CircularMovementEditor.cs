using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircularMovement))]
public class CircularMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CircularMovement circularMovement = (CircularMovement)target;
    }

    private void OnSceneGUI()
    {
        // Set the GUI style for better visibility
        Handles.BeginGUI();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        // Draw a button at a fixed position in the Scene View
        Rect buttonRect = new Rect(Screen.width - 600, Screen.height - 100, 150, 30); // Position and size of the button
        if (GUI.Button(buttonRect, "Play", buttonStyle))
        {
            // Switch to View Tool
            Tools.current = Tool.View;
            
            // Bring Scene View into focus
            if (SceneView.lastActiveSceneView != null)
            {
                SceneView.lastActiveSceneView.Focus();
                Debug.Log("Scene View is now focused.");
            }
            else
            {
                Debug.LogWarning("No active Scene View found.");
            }
        }
        Handles.EndGUI();
    }
}