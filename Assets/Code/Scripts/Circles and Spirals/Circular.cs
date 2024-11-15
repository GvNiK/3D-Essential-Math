using UnityEngine;

public class Circular : MonoBehaviour
{
    [SerializeField] private float m_Radius = 5;
    [SerializeField] private float m_NumberOfBalls = 10;
    [SerializeField] private GameObject m_ObjectToSpawn;
    [SerializeField] private CircularMethod m_CircularMethod;
    private float m_AngleStep;
    private GameObject m_SpawnedGroup;

    public void DrawObjectsOnEdgeOfCircle()
    {
        // Dividing the Number of Balls with the complete trip around the edge of a Circle, i.e., 360 degrees.
        m_AngleStep = 360 / m_NumberOfBalls;
        
        // Create a parent for the Spawned Objects.
        m_SpawnedGroup = new GameObject("Spawned Group");

        switch (m_CircularMethod)
        {
            case CircularMethod.Trigonometry:
                TrigonometryMethod();
                break;
            case CircularMethod.Quaternions:
                QuaternionMethod();
                break;
        }
    }

    private void TrigonometryMethod()
    {
        for (int i = 0; i < m_NumberOfBalls; i++)
        {
            // We get an incrementing angle: 0, 72, 108...360.
            float angle = i * m_AngleStep;
            
            // x=radius×cos(angle)
            float x = m_Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            
            // y=radius×sin(angle)
            float y = m_Radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 position = new Vector3(x, y, 0);
            
            // Instantiate or move each ball to the calculate position.
            SpawnObjects(position);
        }
    }

    private void QuaternionMethod()
    {
        for (int i = 0; i < m_NumberOfBalls; i++)
        {
            // Position the first ball at the radius directly above the center.
            // Rotate it around the center point by a 36-degree increment.
            Vector3 position = Quaternion.Euler(0, 0, i * m_AngleStep) * Vector3.up * m_Radius;
            
            // Instantiate or move each ball to the calculate position.
            SpawnObjects(position);
        }
    }

    private void SpawnObjects(Vector3 position)
    {
        if (m_ObjectToSpawn != null) Instantiate(m_ObjectToSpawn, position, Quaternion.identity, m_SpawnedGroup.transform);
        else Debug.Log("WARNING! GameObject not assigned in " + m_ObjectToSpawn + " field. Please assign one.");
    }
}

public enum CircularMethod
{
    Trigonometry,
    Quaternions
}

