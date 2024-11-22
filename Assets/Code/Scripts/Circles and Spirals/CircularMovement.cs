using UnityEngine;

/// <summary>
/// Task : Circular Movement with Changing Radius
/// Imagine you have a single object that you want to move in a circle, but the circle’s radius gradually increases over time.
/// How would you update the object’s position frame by frame to achieve this effect?
/// </summary>
public class CircularMovement : MonoBehaviour
{
    [SerializeField] private int m_Segments = 50;
    [SerializeField] private float m_Radius = 3;
    [SerializeField] private float m_RadiusStep = 0.5f;
    [SerializeField] private float m_MaxRadiusValue = 10f;
    [SerializeField] private float m_AngularSpeed = 90f;
    [SerializeField] private float m_CurrentAngle = 0;
    [SerializeField] private GameObject m_ObjectToRotate;
    [SerializeField] private Vector3 m_Offset;
    private float m_AngleStep;
    private Vector3 m_NewPosition;
    private float m_DefaultRadius;
    private Vector3 m_OffsetPosition;


    private void Start()
    {
        // Store the Original Radius value as Default value.
        m_DefaultRadius = m_Radius;
    }

    private void OnDrawGizmos()
    {
        // Increment Radius and Reset it if it exceeds the defined Maximum Radius amount.
        if (m_Radius < m_MaxRadiusValue) m_Radius += m_RadiusStep * Time.deltaTime;
        else m_Radius = m_DefaultRadius;
        
        // Reset the angle value if it goes beyond 360 value.
        // Increment the Angular Speed with the 
        if (m_CurrentAngle > 360) m_CurrentAngle = 0;
        else m_CurrentAngle += m_AngularSpeed * Time.deltaTime;
        // An alternate approach (below line).
        // m_CurrentAngle = (m_CurrentAngle + m_AngularSpeed * Time.deltaTime) % 360f;
        
        // Convert angle to radians for trigonometric calculations
        float angleInRadians = m_CurrentAngle * Mathf.Deg2Rad;
        
        // Calculate the object's new position
        float x = m_Radius * Mathf.Cos(angleInRadians);
        float y = m_Radius * Mathf.Sin(angleInRadians);

        m_NewPosition = new Vector3(x, m_ObjectToRotate.transform.position.y, y);
        m_OffsetPosition = m_NewPosition + m_Offset;
        
        if (m_ObjectToRotate != null) m_ObjectToRotate.transform.position = m_NewPosition;
    }

    private void OnDrawGizmosSelected()
    {
        DrawCircle();
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_NewPosition, 0.25f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_OffsetPosition, 0.25f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(m_OffsetPosition, m_NewPosition);
    }

    private void DrawCircle()
    {
        if (m_Segments <= 0) return;

        Gizmos.color = Color.blue;
        m_AngleStep = 360 / m_Segments;
        
        // Start from the position based upon the distance from the center, that is the Radius.
        Vector3 lastPoint = transform.position + new Vector3(m_Radius, 0, 0);

        for (int i = 0; i <= m_Segments; i++)
        {
            // Calculate Angle and respectively get the X and Y position on the arc.
            float angle = i * m_AngleStep * Mathf.Deg2Rad;
            float x = m_Radius * Mathf.Cos(angle);
            float z = m_Radius * Mathf.Sin(angle);
            Vector3 nextPoint = transform.position + new Vector3(x, 0, z);
            
            // Draw Line from last to next point.
            Gizmos.DrawLine(lastPoint, nextPoint);
            
            // Override the last point with next point to start drawing from next point.
            lastPoint = nextPoint;
        }
    }

    private void OnValidate()
    {
        if (m_Segments < 3) m_Segments = 3;
        if (m_Radius <= 0) m_Radius = 1;
        m_DefaultRadius = m_Radius;
    }
}
