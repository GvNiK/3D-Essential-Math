using System;
using UnityEngine;
using UnityEditor;

public class DotProductEditor : EditorWindow
{
    public Vector3 m_p0;
    public Vector3 m_p1;
    public Vector3 m_c;

    private SerializedObject obj;
    private SerializedProperty propP0;
    private SerializedProperty propP1;
    private SerializedProperty propC;

    [MenuItem("Tools/Dot Product")]
    public static void ShowWindow()
    {
        DotProductEditor window = (DotProductEditor)GetWindow(typeof(DotProductEditor), true, "Dot Product");
        window.Show();
    }

    private void OnEnable()
    {
        if (m_p0 == Vector3.zero && m_p1 == Vector3.zero)
        {
            m_p0 = new Vector3(0.0f, 1.0f, 0.0f);
            m_p1 = new Vector3(0.0f, 0.5f, 0.0f);
            m_p0 = Vector3.zero;
        }
        
        obj = new SerializedObject(this);
        propP0 = obj.FindProperty("m_p0");
        propP1 = obj.FindProperty("m_p1");
        propC = obj.FindProperty("m_c");
        
        SceneView.duringSceneGui += SceneGUI;
        Debug.Log("Scene View - Open.");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
        Debug.Log("Scene View - Close.");
    }

    private void OnGUI()
    {
        obj.Update();
        
        DrawBlockGUI("p0", propP0);
        DrawBlockGUI("p1", propP1);
        DrawBlockGUI("c", propC);

        if (GUILayout.Button("Reset")) ResetValues();

        if (obj.ApplyModifiedProperties())
        {
            SceneView.RepaintAll();
        }
    }

    void DrawBlockGUI(string lab, SerializedProperty prop)
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField(lab, GUILayout.Width(50));
        EditorGUILayout.PropertyField(prop, GUIContent.none);
        EditorGUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
    }
    
    void ResetValues()
    {
        m_p0 = new Vector3(0, 1, 0);
        m_p1 = new Vector3(0, 0.5f, 0);
        m_c = new Vector3(0, 0, 0);
    }

    private void SceneGUI(SceneView view)
    {
        Handles.color = Color.red;
        Vector3 p0 = SetMovePoint(m_p0);
        Handles.color = Color.green;
        Vector3 p1 = SetMovePoint(m_p1);
        Handles.color = Color.white;
        Vector3 c = SetMovePoint(m_c);
        
        // Optimization Aspect - Repaint only when the Handles are moved, i.e., when their values are updated.
        if (m_p0 != p0 || m_p1 != p1 || m_c != c)
        {
            // Update the changed values.
            m_p0 = p0;
            m_p1 = p1;
            m_c = c;
            
            // Now Repaint.
            Repaint();
            
            Debug.Log("Repainted.");
        }
    }   

    /// <summary>
    /// Sets the Handles in 3D Space, based upon the Position provided.
    /// </summary>
    /// <param name="pos">The Position to draw the Handle.</param>
    /// <returns></returns>
    Vector3 SetMovePoint(Vector3 pos)
    {
        float size = HandleUtility.GetHandleSize(Vector3.zero) * 0.15f;
        return Handles.FreeMoveHandle(pos, size, Vector3.zero, Handles.SphereHandleCap);
    }
}
