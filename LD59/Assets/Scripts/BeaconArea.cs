using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;


public class BeaconArea : MonoBehaviour
{
   public float ChargeTime;

   public float chargeAmount = 0;
   public bool playerInArea;

   private List<Vector2> collisionPoints = new List<Vector2>();

   public UnityEvent OnBeaconCharged = new UnityEvent();
   public GameObject IndicatorCircle;

   // Update is called once per frame
   void Update()
   {
      if (playerInArea)
      {
         chargeAmount += Time.deltaTime;
         IndicatorCircle.transform.localScale = Vector2.one * chargeAmount / ChargeTime;
         if(chargeAmount > ChargeTime)
         {
            //Do something here
            OnBeaconCharged.Invoke();
            Destroy(this.gameObject);
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         playerInArea = true;
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         playerInArea = false;
      }
   }
}
