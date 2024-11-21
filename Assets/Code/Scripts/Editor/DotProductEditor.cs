using System;
using UnityEngine;
using UnityEditor;

public class DotProductEditor : EditorWindow
{
    public Vector3 m_p0;
    public Vector3 m_p1;
    public Vector3 m_c;
    
    // To display the above properties in the Window (Inspector).
    private SerializedObject obj;
    private SerializedProperty propP0;
    private SerializedProperty propP1;
    private SerializedProperty propC;
    private GUIStyle guiStyle = new GUIStyle();
    
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
            m_c = Vector3.zero;
        }
        
        // To Display the class properties in the Editor Window (Inspector).
        obj = new SerializedObject(this);
        propP0 = obj.FindProperty("m_p0");
        propP1 = obj.FindProperty("m_p1");
        propC = obj.FindProperty("m_c");

        guiStyle.fontSize = 35;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;

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
        
        DrawLabel(p0, p1, c);
    }

    void DrawLabel(Vector3 p0, Vector3 p1, Vector3 c)
    {
        Handles.Label(c, DotProduct(p0, p1, c).ToString("F1"), guiStyle);
        Handles.color = Color.black;

        Vector3 cLef = WorldRotation(p0, c, new Vector3(0f, 1f, 0f));
        Vector3 cRig = WorldRotation(p0, c, new Vector3(0f, -1f, 0f)); 
        
        Handles.DrawAAPolyLine(3f, p0, c);
        Handles.DrawAAPolyLine(3f, p1, c);
        Handles.DrawAAPolyLine(3f, c, cLef);
        Handles.DrawAAPolyLine(3f, c, cRig);
    }
    
    /// <summary>
    /// R = C + HP
    /// H - corresponds to the Quaternion rotation of the angle resulting from calculating the arctangent 
    /// of the direction of the vector « p0 – c »
    /// </summary>
    /// <param name="p"> </param>
    /// <param name="c"> Refers to the vector we defined as the central point.</param>
    /// <param name="pos"> A Vector with a magnitude equal to the distance between « C » and « P ».</param>
    /// <returns></returns>
    Vector3 WorldRotation(Vector3 p, Vector3 c, Vector3 pos)
    {
        // Getting the direction.
        Vector2 dir = (p - c).normalized;
        
        // Getting the arctangent and converting it to angle in degrees.
        float ang = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        // Convert angle to quaternion.
        // We use Z axis considering we have designed our tool in 2 Dimensional plane.
        Quaternion rot = Quaternion.AngleAxis(ang, Vector3.forward);
        
        return c + rot * pos;
    }

    float DotProduct(Vector3 p0, Vector3 p1, Vector3 c)
    {
        Vector3 a = (p0 - c).normalized;
        Vector3 b = (p1 - c).normalized;

        return (a.x * b.x) + (a.y * b.y) * (a.z * b.z);
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
