using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
   public float SpawnRadius;
   public Transform EnemyParentObject;
   public GameObject basicPrefab;
   public float basicCooldown;
   private float cooldowntimer;

   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(this.transform.position, SpawnRadius);
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      cooldowntimer -= Time.deltaTime;
      if(cooldowntimer <= 0)
      {
         Vector2 SpawnLocation = Random.onUnitCircle * SpawnRadius;
         Instantiate(basicPrefab, this.transform.position + (Vector3)SpawnLocation, Quaternion.identity, EnemyParentObject);
         cooldowntimer = basicCooldown;
      }
   }
}
