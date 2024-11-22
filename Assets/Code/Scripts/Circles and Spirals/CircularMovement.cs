using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private int m_Segments = 50;
    [SerializeField] private float m_Radius = 3;
    [SerializeField] private float m_RadiusStep = 0.5f;
    [SerializeField] private float m_MaxRadiusValue = 20f;
    [SerializeField] private float m_AngularSpeed = 90f;
    [SerializeField] private float m_CurrentAngle = 0;
    [SerializeField] private GameObject m_ObjectToRotate;
    private float m_AngleStep;
    private Vector3 m_NewPosition;
    private float m_DefaultRadius;
    
    private void OnDrawGizmos()
    {
        // Increment Radius
        // if (m_Radius != m_MaxRadiusValue) m_Radius += m_RadiusStep * Time.deltaTime;
        // else m_Radius = m_DefaultRadius;
        
        // Reset the angle value if it goes beyond 360 value.
        // Increment the Angular Speed with the 
        if (m_CurrentAngle > 360) m_CurrentAngle = 0;
        else m_CurrentAngle += m_AngularSpeed * Time.deltaTime;
        
        // Convert angle to radians for trigonometric calculations
        float angleInRadians = m_CurrentAngle * Mathf.Deg2Rad;
        
        // Calculate the object's new position
        float x = m_Radius * Mathf.Cos(angleInRadians);
        float y = m_Radius * Mathf.Sin(angleInRadians);

        m_NewPosition = new Vector3(x, m_ObjectToRotate.transform.position.y, y);
        
        if (m_ObjectToRotate != null) m_ObjectToRotate.transform.position = m_NewPosition;
    }

    private void OnDrawGizmosSelected()
    {
        DrawCircle();
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_NewPosition, 0.25f);
    }

    private void DrawCircle()
    {
        if (m_Segments <= 0) return;

        Gizmos.color = Color.blue;
        m_AngleStep = 360 / m_Segments;

        Vector3 lastPoint = transform.position + new Vector3(m_Radius, 0, 0);

        for (int i = 0; i <= m_Segments; i++)
        {
            float angle = i * m_AngleStep * Mathf.Deg2Rad;
            float x = m_Radius * Mathf.Cos(angle);
            float z = m_Radius * Mathf.Sin(angle);
            Vector3 nextPoint = transform.position + new Vector3(x, 0, z);
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(100, 50)), "Play"))
        {
            Debug.Log("Focus Scene View.");
        }
    }

    private void OnValidate()
    {
        if (m_Segments < 3) m_Segments = 3;
        if (m_Radius <= 0) m_Radius = 1;
        m_DefaultRadius = m_Radius;
    }
}
