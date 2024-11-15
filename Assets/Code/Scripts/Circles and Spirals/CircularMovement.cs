using System;
using UnityEditor;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float m_Segments = 50;
    [SerializeField] private float m_Radius = 5;
    [SerializeField] private GameObject m_ObjectToSpawn;
    private float m_AngleStep;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float x = 0;
        float z = 0;
        
        m_AngleStep = 360 / m_Segments;

        Vector3 lastPoint = transform.position + new Vector3(m_Radius, 0, 0);

        for (int i = 0; i <= m_Segments; i++)
        {
            float angle = i * m_AngleStep;
            x = m_Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = m_Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 nextPoint = transform.position + new Vector3(x, 0, z);
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }

        if (m_ObjectToSpawn != null) Instantiate(m_ObjectToSpawn, new Vector3(x, 0, z), Quaternion.identity);
    }
}
