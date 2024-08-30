using UnityEngine;

public class DotProductDemo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_Bullet;
    
    [Header("Spawn Points")]
    [SerializeField] private Transform m_SpawnPositionFront;
    [SerializeField] private Transform m_SpawnPositionBack;
    [SerializeField] private Transform m_SpawnPositionLeft;
    [SerializeField] private Transform m_SpawnPositionRight;

    [Header("Settings")]
    [SerializeField] private float m_ForceMultiplier = 3f;
    [SerializeField] private bool m_ToggleSideWays; 
    public enum Direction { Front, Back, Left, Right }

    GameObject obj;
    Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet(Direction.Front);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            SpawnBullet(Direction.Back);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            SpawnBullet(m_ToggleSideWays ? Direction.Right : Direction.Left);
        }
    }

    private void SpawnBullet(Direction direction)
    {
        if (m_Bullet != null)
        {
            switch (direction)
            {
                case Direction.Front:
                    obj = Instantiate(m_Bullet, m_SpawnPositionFront.position, Quaternion.identity);
                    AddForce(m_SpawnPositionFront.forward);
                    break;
                case Direction.Back:
                    obj = Instantiate(m_Bullet, m_SpawnPositionBack.position, Quaternion.identity);
                    AddForce(m_SpawnPositionBack.forward);
                    break;
                case Direction.Left:
                    obj = Instantiate(m_Bullet, m_SpawnPositionLeft.position, Quaternion.identity);
                    AddForce(m_SpawnPositionLeft.forward);
                    m_ToggleSideWays = true;
                    break;
                case Direction.Right:
                    obj = Instantiate(m_Bullet, m_SpawnPositionRight.position, Quaternion.identity);
                    AddForce(m_SpawnPositionRight.forward);
                    m_ToggleSideWays = false;
                    break;
            }
        }
    }

    private void AddForce(Vector3 forward)
    {
        if (obj != null)
        {
            rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(forward * m_ForceMultiplier, ForceMode.Force);
            }
        }
    }
}
