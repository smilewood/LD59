using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//Current, max
public class PlayerHealthChanged : UnityEvent<int, int>{}
public class PlayerDamageEvent : UnityEvent<int>{}

public class PlayerHealth : MonoBehaviour
{
   public static PlayerHealthChanged OnPlayerHealthChanged = new PlayerHealthChanged();
   public static PlayerDamageEvent DamagePlayer = new PlayerDamageEvent();
   public int MaxHp;
   public int CurrentHp;
   private PlayerUpgradeSystem upgrades;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      upgrades = this.gameObject.GetComponent<PlayerUpgradeSystem>();
      UpdateHealthTotals();
      CurrentHp = MaxHp;
      OnPlayerHealthChanged.Invoke(CurrentHp, MaxHp);
      DamagePlayer.AddListener(ApplyDamage);
   }

   public void ApplyDamage(int amount)
   {
      CurrentHp = Mathf.Max(0, Mathf.Min(CurrentHp - amount, MaxHp));
      OnPlayerHealthChanged.Invoke(CurrentHp, MaxHp);
   }

   public IEnumerator HealthRegen()
   {
      while (true)
      {
         if(upgrades.CurrentModifiers.HealthRegen > 0)
         {
            yield return new WaitForSeconds(1 / upgrades.CurrentModifiers.HealthRegen);
            CurrentHp += CurrentHp < MaxHp ? 1 : 0;
         }
         yield return null;
      }
   }

   public void UpdateHealthTotals()
   {
      int oldMax = MaxHp;
      MaxHp = upgrades.CurrentModifiers.HealthAdd;
      CurrentHp += MaxHp - oldMax;
   }

}
