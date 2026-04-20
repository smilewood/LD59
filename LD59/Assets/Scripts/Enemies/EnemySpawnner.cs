using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
   public float SpawnRadius;
   public Transform EnemyParentObject;
   public GameObject basicPrefab;
   public float basicCooldown;
   private float cooldowntimer;

   public EnemyScaling EnemySource;
   private List<EnemyType> ActiveEnemies;

   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(this.transform.position, SpawnRadius);
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      ActiveEnemies = new List<EnemyType>();
      AddEnemyType(EnemySource.EnemyTypes[Random.Range(0, EnemySource.EnemyTypes.Count)]);
   }

   public void AddEnemyType(EnemyType enemyType)
   {
      ActiveEnemies.Add(enemyType);
   }

   // Update is called once per frame
   void Update()
   {
      cooldowntimer -= Time.deltaTime;
      if(cooldowntimer <= 0)
      {
         Vector2 SpawnLocation = Random.onUnitCircle * SpawnRadius;
         Instantiate(ActiveEnemies[Random.Range(0, ActiveEnemies.Count)].EnemyPrefab, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, EnemyParentObject);
         cooldowntimer = basicCooldown;
      }
   }

   public IEnumerable<EnemyType> AvailableEnemies()
   {
      //Allow adding the same enemy to the list multiple tims, increasing the odds of any given type. 
      return EnemySource.EnemyTypes;//.Where(e => !ActiveEnemies.Contains(e));
   }
}
