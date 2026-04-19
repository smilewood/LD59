using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class PassiveWeapon : Weapon
{
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      StartCoroutine(AutoFire());
      base.Start();
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
