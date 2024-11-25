using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Tooltip("Total number of objects.")]
    [SerializeField] private int m_TotalObjectCount = 10;
    [Tooltip("Axis to position spawned objects.")]
    [SerializeField] private Axis m_PositionAxis;
    [Tooltip("Randomize Angles rotations on which Axis?")]
    [SerializeField] private Axis m_RotationAxis;
    [Tooltip("Position Offset from the base position.")]
    [SerializeField] private float m_Gap;
    [Tooltip("Name of the parent object of the objects spawned.")]
    [SerializeField] private string m_ParentObjectName = "Spawner Parent";
    [Tooltip("GameObject to Spawn.")]
    [SerializeField] private GameObject m_ObjectToSpawn;
    private float m_RotationOffset;
    private Transform m_Parent;

    public void SpawnObjects()
    {
        if (m_ObjectToSpawn == null) return;

        // Check if a GameObject with the specified parent name already exists
        GameObject existingParent = GameObject.Find(m_ParentObjectName);
        
        // If it does, then spawn all objects within that object. Else create a new one. 
        m_Parent = existingParent != null ? existingParent.transform : new GameObject(m_ParentObjectName).transform;
        
        float positionOffset  = 0;
            
        for (int i = 0; i < m_TotalObjectCount; i++)
        {
            positionOffset += m_Gap;
            
            // Calculate position and rotation
            Vector3 spawnPosition = CalculatePosition(positionOffset);
            Quaternion spawnRotation = CalculateRotation();
            
            Instantiate(m_ObjectToSpawn, spawnPosition, spawnRotation, m_Parent);
        }
    }

    private Vector3 CalculatePosition(float offset)
    {
        switch (m_PositionAxis)
        {
            case Axis.X: return transform.position + new Vector3(offset, 0, 0);
            case Axis.Y: return transform.position + new Vector3(0, offset, 0);
            case Axis.Z: return transform.position + new Vector3(0, 0, offset);
            // This will spawn objects diagonally in 3D space. Skip this line if this is not intended.
            case Axis.All: return transform.position + new Vector3(offset, offset, offset);
            default: return transform.position;
        }
    }

    private Quaternion CalculateRotation()
    {
        float randomAngle = Random.Range(0, 360);
        Vector3 rotationVector = Vector3.zero;

        switch (m_RotationAxis)
        {
            case Axis.X: rotationVector.x = randomAngle;
                break;
            case Axis.Y: rotationVector.y = randomAngle;
                break;
            case Axis.Z: rotationVector.z = randomAngle;
                break;
            case Axis.All: rotationVector = new Vector3(randomAngle, randomAngle, randomAngle);
                break;
        }

        return Quaternion.Euler(rotationVector);
    }
    
    private void OnValidate()
    {
        if (m_TotalObjectCount < 1) m_TotalObjectCount = 1;
        if (m_Gap < 0) m_Gap = 0;
    }
}

public enum Axis
{
    X, 
    Y, 
    Z,
    All
}
