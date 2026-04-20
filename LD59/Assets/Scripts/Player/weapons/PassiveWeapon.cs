using System.Collections;
using System.Linq;
using UnityEngine;

public interface IPassiveWeapon : IEquipmentSlotItem
{

}

public abstract class PassiveWeapon<T> : Weapon<T>, IPassiveWeapon where T : WeaponUpgradeTier
{
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      base.Start();
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
}
