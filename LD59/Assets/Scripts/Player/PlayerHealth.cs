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

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      CurrentHp = MaxHp;
      OnPlayerHealthChanged.Invoke(CurrentHp, MaxHp);
      DamagePlayer.AddListener(ApplyDamage);
   }

   public void ApplyDamage(int amount)
   {
      CurrentHp = Mathf.Max(0, Mathf.Min(CurrentHp - amount, MaxHp));
      OnPlayerHealthChanged.Invoke(CurrentHp, MaxHp);
   }


}
