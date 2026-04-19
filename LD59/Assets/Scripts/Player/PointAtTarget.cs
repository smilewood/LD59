using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PointAtTarget : MonoBehaviour
{
   public Transform Target;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if(Target == null)
      {
         //Assume that the target was removed for reasons and that we just want this to go away
         Destroy(this.gameObject);
         return;
      }
      this.transform.up = Target.position - this.transform.position;
   }
}
