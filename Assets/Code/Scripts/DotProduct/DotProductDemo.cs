using UnityEngine;

public class DotProductDemo : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Transform m_SpawnPositionFront;
    [SerializeField] private Transform m_SpawnPositionBack;
    [SerializeField] private float m_ForceMultiplier = 3f;
    public enum Direction { Front, Back}

    GameObject obj = null;
    Rigidbody rb = null;

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
    }

    private void SpawnBullet(Direction direction)
    {
        if (m_Bullet != null)
        {
            switch (direction)
            {
                case Direction.Front:
                    obj = Instantiate(m_Bullet, m_SpawnPositionFront.position, Quaternion.identity);
                    break;
                case Direction.Back:
                    obj = Instantiate(m_Bullet, m_SpawnPositionBack.position, Quaternion.identity);
                    break;
            }
            rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.forward * m_ForceMultiplier, ForceMode.Force);
            }
        }
    }
}
