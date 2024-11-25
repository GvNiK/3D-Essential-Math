using UnityEngine;

public class Spiral : MonoBehaviour
{
    [Tooltip("Total number of objects.")]
    [SerializeField] private float m_TotalObjects = 10;
    [Tooltip("Angle between consecutive objects.")]
    [SerializeField] private float m_AngleStep = 20;
    [Tooltip("Step distance along the height.")]
    [SerializeField] private float m_SpiralStep = 1;
    [Tooltip("Initial radius of the spiral.")]
    [SerializeField] private float m_Radius = 5;
    [Tooltip("Radius growth per step.")]
    [SerializeField] private float m_RadiusGrowthStep = 0.1f; 
    [Tooltip("Center of the spiral. The Start point of the Spiral.")]
    [SerializeField] private Vector3 m_Center = Vector3.zero; 
    [Tooltip("GameObject to Spawn.")]
    [SerializeField] private GameObject m_ObjectToSpawn;
    private Transform m_Parent;

    public void CreateSpiral()
    {
        if (m_ObjectToSpawn == null || m_TotalObjects <= 0) return;

        m_Parent = new GameObject("Spiral").transform;
        //m_Center = transform.position;
        
        float stepOffset = 0;
        
        for (int i = 0; i < m_TotalObjects; i++)
        {
            // Increment the Offset.
            stepOffset += m_SpiralStep;
            
            // Increment the Radius per step.
            float currentRadius = m_Radius * i * m_RadiusGrowthStep;
            
            // Spawn Position. (Creates a New Vector in 3D Space)
            Vector3 position = m_Center + Quaternion.Euler(0, i * m_AngleStep, 0) * Vector3.forward * currentRadius;
            
            // Apply the step offset to the Height.
            position.y = stepOffset;
            
            // Spawn the Object.
            Instantiate(m_ObjectToSpawn, position, Quaternion.identity, m_Parent);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_Center, 0.5f);
    }
}
