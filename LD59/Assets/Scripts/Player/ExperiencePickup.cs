using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
   public float PickupTime;
   private float elapsedTime;

   private Vector2 startPos;
   private Transform target;
   private bool collecing;

   // Update is called once per frame
   void Update()
   {
      if (collecing)
      {
         elapsedTime += Time.deltaTime;
         float t = elapsedTime / PickupTime;
         this.gameObject.transform.position = (Vector3)Vector2.Lerp(startPos, target.position, t*t);
      }
      if (elapsedTime >= PickupTime)
      {
         ExperienceTracker.ExperiencePickup.Invoke();
         Destroy(this.gameObject);
      }

   }

   public void StartPickup(Transform target)
   {
      this.startPos = this.gameObject.transform.position;
      this.target = target;
      collecing = true;
   }
}
