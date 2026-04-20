using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ShotFiringWeaponUpgradeTier : WeaponUpgradeTier
{
   [Header("Shot Settings")]
   public int ShotPierce;
   public float ShotSpeed;
   public GameObject ShotPrefab;
}

[Serializable]
public class BlasterUpgradeTier : ShotFiringWeaponUpgradeTier
{
   [Header("Blaster Settings")]
   public int MaxCharges;
   public float ChargedCooldown;
}

public class Blaster : TargetedWeapon<BlasterUpgradeTier>
{
   private int CurrentCharges;

   public Transform ShotParent;


   public override void Start()
   {
      ShotParent = GameObject.Find("PlayerShotsParent").transform;
      base.Start();
      StartCoroutine(BlasterCharge());
   }

   public override float CooldownToNextShot()
   {
      return CurrentCharges > 0 ? Modifiers.Firerate(Values.ChargedCooldown) : FireCooldown;
   }

   private IEnumerator BlasterCharge()
   {
      while (true)
      {
         yield return new WaitForSeconds(FireCooldown);
         CurrentCharges += CurrentCharges < (Values.MaxCharges + Modifiers.ShotsAdd) ? 1 : 0;
      }
   }

   public override void FireAtTarget(Vector2 target)
   {
      --CurrentCharges;
      GameObject newShot = Instantiate(Values.ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);
      newShot.GetComponent<DirectionShot>().InitializeShot(target.normalized, Values.ShotSpeed * Modifiers.ShotSpeedMult, Damage, Values.ShotPierce + Modifiers.PierceAdd);
   }
}
