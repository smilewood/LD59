using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
   public float PickupTime;
   private float elapsedTime;

   private Vector2 startPos;
   private Transform target;

   private bool collecing;
   private bool scattering;
   private Vector2 scatterPos;
   // Update is called once per frame
   void Update()
   {
      if (collecing)
      {
         elapsedTime += Time.deltaTime;
         float t = elapsedTime / PickupTime;
         this.gameObject.transform.position = (Vector3)Vector2.Lerp(startPos, target.position, t * t);
      }
      else if (scattering)
      {
         elapsedTime += Time.deltaTime;
         float t = elapsedTime / PickupTime;
         this.gameObject.transform.position = (Vector3)Vector2.Lerp(startPos, scatterPos, .5f*Mathf.Log((99 * t) + 1));
      }

      if (elapsedTime >= PickupTime)
      {
         if (collecing)
         {
            ExperienceTracker.ExperiencePickup.Invoke();
            Destroy(this.gameObject);
         }
         else if (scattering)
         {
            scattering = false;
            elapsedTime = 0;
         }
      }

   }

   public void StartPickup(Transform target)
   {
      this.startPos = this.gameObject.transform.position;
      this.target = target;
      elapsedTime = 0;
      scattering = false;
      collecing = true;
   }

   public void Scatter(Vector2 target)
   {
      this.startPos = this.gameObject.transform.position;
      scattering = true;
      scatterPos = startPos + target;
      elapsedTime = 0;
   }
}
