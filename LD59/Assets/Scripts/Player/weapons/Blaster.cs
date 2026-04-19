using System.Linq;
using UnityEngine;

public class Blaster : TargetedWeapon
{
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
   }

   public override float CooldownToNextShot()
   {
      return FireCooldown;
   }

   public override void FireAtTarget(Vector2 target)
   {
      GameObject newShot = Instantiate(ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);
      newShot.GetComponent<DirectionShot>().InitializeShot(target.normalized, ShotSpeed, Damage, ShotPierce);
   }
}
