using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
   public float SpawnRadius;
   public Transform EnemyParentObject;

   public EnemyScaling EnemySource;
   private List<EnemyType> ActiveEnemies;

   public float spawnPoints;
   public float PointsPerSec;
   public float PointsPerSecGrowth;

   private EnemyType nextEnemy;
   private float NextCost
   {
      get
      {
         float cost = nextEnemy.PointsCost;
         for (int i = 1; i < ActiveEnemies.Count(enemy => ReferenceEquals(enemy, nextEnemy)); i++)
         {
            cost /= 2;
         }
         return cost;
      }
   }



   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(this.transform.position, SpawnRadius);
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      ActiveEnemies = new List<EnemyType>();
      AddEnemyType(EnemySource.EnemyTypes[Random.Range(0, EnemySource.EnemyTypes.Count)]);
      nextEnemy = ActiveEnemies[Random.Range(0, ActiveEnemies.Count)];
   }

   public void AddEnemyType(EnemyType enemyType)
   {
      ActiveEnemies.Add(enemyType);
   }

   // Update is called once per frame
   void Update()
   {
      spawnPoints += PointsPerSec * Time.deltaTime;
      PointsPerSec += PointsPerSecGrowth * Time.deltaTime;
      if (spawnPoints > NextCost)
      {
         Vector2 SpawnLocation = Random.onUnitCircle * SpawnRadius;
         Instantiate(nextEnemy.EnemyPrefab, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, EnemyParentObject);
         spawnPoints -= nextEnemy.PointsCost;
         nextEnemy = ActiveEnemies[Random.Range(0, ActiveEnemies.Count)];
      }
   }

   public IEnumerable<EnemyType> AvailableEnemies()
   {
      //Allow adding the same enemy to the list multiple tims, increasing the odds of any given type. 
      return EnemySource.EnemyTypes;//.Where(e => !ActiveEnemies.Contains(e));
   }
}
