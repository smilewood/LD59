using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class PassiveWeapon : MonoBehaviour
{
   [Tooltip("Time between shots")]
   public float FireCooldown;


   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public virtual void Start()
   {
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      FireCooldown *= upgradeStatus.FirerateEffects[upgradeStatus.FirerateLevel].Effect;
      StartCoroutine(AutoFire());
   }

   public IEnumerator AutoFire()
   {
      while (true)
      {
         FireWeapon();
         yield return new WaitForSeconds(CooldownToNextShot());
      }
   }

   public abstract void FireWeapon();
   public abstract float CooldownToNextShot();
}
