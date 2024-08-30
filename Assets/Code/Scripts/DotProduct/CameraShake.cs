 using UnityEngine;
 using System.Collections;

 public class CameraShake : MonoBehaviour
 {
     public IEnumerator Shake(float duration, float magnitude)
     {
         float elapsedTime = 0;

         Vector3 originalPosition = transform.localPosition;

         while (elapsedTime < duration)
         {
             // Generate a random position within the magnitude
             float offsetX = Random.Range(-1f, 1f) * magnitude;
             float offsetY = Random.Range(-1f, 1f) * magnitude;

             transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

             elapsedTime += Time.deltaTime;
             
             // Wait until the next frame before continuing the loop
             yield return null;
         }
        
         // Reset the camera back to its original position
         transform.localPosition = originalPosition;

     }
 }
