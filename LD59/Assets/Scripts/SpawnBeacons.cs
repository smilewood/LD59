using System.Collections.Generic;
using UnityEngine;

public class SpawnBeacons : MonoBehaviour
{
   public List<GameObject> beacons;
   public float SpawnRadius;
   private Transform parentObject;
   public bool SpawnOnStart;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {

      parentObject = GameObject.Find("WorldTilemap").transform;
      if (SpawnOnStart)
      {
         SpawnListedBeacons();
      }
   }

   public void SpawnListedBeacons()
   {
      foreach (GameObject beacon in beacons)
      {
         Vector2 SpawnLocation = Random.onUnitCircle * SpawnRadius;
         Instantiate(beacon, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, parentObject);
      }
      Destroy(this.gameObject);
   }

}
