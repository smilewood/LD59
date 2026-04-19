using System.Collections.Generic;
using UnityEngine;

public class SpawnBeacons : MonoBehaviour
{
   public List<GameObject> beacons;
   public float SpawnRadius;
   private Transform parentObject;
   private Transform playerRoot;
   public GameObject PointerPrefab;
   public bool SpawnOnStart;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      parentObject = GameObject.Find("WorldTilemap").transform;
      playerRoot = GameObject.Find("PlayerRoot").transform;
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
         GameObject newBeacon = Instantiate(beacon, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, parentObject);
         GameObject pointer = Instantiate(PointerPrefab, playerRoot);
         pointer.GetComponent<PointAtTarget>().Target = newBeacon.transform;
      }
      Destroy(this);
   }

}
