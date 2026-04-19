using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Blaster : TargetedWeapon
{
   [Header("Blaster Settings")]
   public int MaxCharges;
   public float ChargedCooldown;
   private int CurrentCharges;

   [Header("Shot Settings")]
   public int ShotPierce;
   public float ShotSpeed;
   public GameObject ShotPrefab;
   public Transform ShotParent;

   public override void Start()
   {
      ShotParent = GameObject.Find("PlayerShotsParent").transform;
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      ShotPierce += upgradeStatus.PierceEffects[upgradeStatus.PierceLevel].Effect;
      base.Start();
      StartCoroutine(BlasterCharge());
   }

   public override float CooldownToNextShot()
   {
      return CurrentCharges > 0 ? ChargedCooldown : FireCooldown;
   }

   private IEnumerator BlasterCharge()
   {
      while (true)
      {
         yield return new WaitForSeconds(FireCooldown);
         CurrentCharges += CurrentCharges < MaxCharges ? 1 : 0;
      }
   }

   public override void FireAtTarget(Vector2 target)
   {
      --CurrentCharges;
      GameObject newShot = Instantiate(ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);
      newShot.GetComponent<DirectionShot>().InitializeShot(target.normalized, ShotSpeed, Damage, ShotPierce);
   }
}
