using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//Damage Amount, Current HP, Remaining HP
public class EnemyDamagedEvent : UnityEvent<int, int, int>{}
public class EnemyHealth : MonoBehaviour
{
   public EnemyDamagedEvent OnDamageRecieved;
   public int BaseHealth;

   private int currentHealth;
   private int maxHealth;

   [Header("Experience")]
   public GameObject ExpPickup;
   public int ExpAmount;
   public float ScatterRadius;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      OnDamageRecieved = new EnemyDamagedEvent();

      EnemyScaling scaleStatus = Resources.FindObjectsOfTypeAll<EnemyScaling>().First();

      currentHealth = maxHealth = scaleStatus.GetHealthForTime(BaseHealth);
   }

   public void ApplyDamage(int damageAmount)
   {
      currentHealth -= damageAmount;
      OnDamageRecieved.Invoke(damageAmount, currentHealth, maxHealth);
      if (currentHealth < 0)
      {
         for(int i = 0; i < ExpAmount; ++i)
         {
            GameObject pickup = Instantiate(ExpPickup, this.transform.position, Quaternion.identity, this.transform.parent);
            pickup.GetComponent<ExperiencePickup>().Scatter(Random.insideUnitCircle * ScatterRadius);
         }
         Destroy(this.gameObject);
      }
   }

}
