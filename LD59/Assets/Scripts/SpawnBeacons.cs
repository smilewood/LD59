using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeacons : MonoBehaviour
{
   [Serializable]
   public struct BeaconSpawn
   {
      public GameObject BeaconPrefab;
      public GameObject PointerPrefab;
      public float Distance;
   }
   public List<BeaconSpawn> beacons;
   private Transform parentObject;
   private Transform playerRoot;
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
      foreach (BeaconSpawn beacon in beacons)
      {
         Vector2 SpawnLocation = UnityEngine.Random.onUnitCircle * beacon.Distance;
         GameObject newBeacon = Instantiate(beacon.BeaconPrefab, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, parentObject);
         GameObject pointer = Instantiate(beacon.PointerPrefab, playerRoot);
         pointer.GetComponent<PointAtTarget>().Target = newBeacon.transform;
      }
      Destroy(this);
   }

}
