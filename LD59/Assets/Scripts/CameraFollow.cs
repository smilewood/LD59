using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform target;

   //public float bounciness;

   private Vector3 InitialOffset;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      InitialOffset = this.transform.position - target.position;
   }

   // Update is called once per frame
   void Update()
   {
      this.transform.position = target.position + InitialOffset;
   }
}
