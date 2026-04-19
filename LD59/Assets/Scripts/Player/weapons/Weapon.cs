using System.Linq;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
   [Tooltip("Time between shots")]
   public float FireCooldown;

   public int Damage;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public virtual void Start()
   {
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      FireCooldown *= upgradeStatus.FirerateEffects[upgradeStatus.FirerateLevel].Effect;
      Damage += upgradeStatus.DamageEffects[upgradeStatus.DamageLevel].Effect;
   }


   public abstract float CooldownToNextShot();
}
