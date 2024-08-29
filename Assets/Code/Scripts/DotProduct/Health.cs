using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private Material m_Material;
    [SerializeField] private float m_HitEffectDuration = 0.1f;
    [SerializeField] private Camera m_Camera;
    
    private Color m_DefaultColor;

    private void Start()
    {
        if (m_Material == null) m_Material = GetComponent<Material>();
        if (m_Material != null) m_DefaultColor = m_Material.color;
    }

    /// <summary>
    /// Give Damage to the Player or GameObject that has this script.
    /// </summary>
    /// <param name="bullet">The Bullet reference that hit this GameObject</param>
    public void Damage(GameObject bullet)
    {
        if (bullet != null)
        {
            // Player's forward direction
            Vector3 playerDirection = this.transform.forward;
            
            // Vector pointing from player to enemy or bullet
            Vector3 bulletDirection = (bullet.transform.position - this.transform.position).normalized;
            
            float dotProduct = Vector3.Dot(playerDirection, bulletDirection);
            
            // Determine enemy or bullet position relative to player
            if (dotProduct > 0)
            {
                // Hit Front
                StopCoroutine(PlayHitEffect(Color.cyan));
                StartCoroutine(PlayHitEffect(Color.cyan));
                Debug.Log("Hit at <color=cyan>Front</color>. DotProduct Value : " + "<color=green>" + dotProduct + "</color>");
            }
            else if (dotProduct < 0)
            {
                // Hit Back
                StopCoroutine(PlayHitEffect(Color.red));
                StartCoroutine(PlayHitEffect(Color.red));
                Debug.Log("Hit at <color=red>Back</color>. DotProduct Value : " + "<color=green>" + dotProduct + "</color>");
            }
            else
            {
                // Hit Sideways
                StopCoroutine(PlayHitEffect(Color.yellow));
                StartCoroutine(PlayHitEffect(Color.yellow));
                Debug.Log("Hit at <color=yellow>Side</color>. DotProduct Value : " + "<color=green>" + dotProduct + "</color>");
            }
        }
    }

    IEnumerator PlayHitEffect(Color hitEffectColor)
    {
        if (m_Material != null)
        {
            m_Material.color = hitEffectColor;
            if (m_Camera != null)
            {
                
            }
            yield return new WaitForSeconds(m_HitEffectDuration);

            m_Material.color = m_DefaultColor;
        }

        yield return null;
    }
}
