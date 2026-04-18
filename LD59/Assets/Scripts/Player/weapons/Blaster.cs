using UnityEngine;

public class Blaster : TargetedWeapon
{
   public GameObject ShotPrefab;
   private Transform ShotParent;

   protected override void Start()
   {
      base.Start();
      ShotParent = GameObject.Find("PlayerShotsParent").transform;   
   }

   public override float CooldownToNextShot()
   {
      return FireCooldown;
   }

   public override void FireAtTarget(Vector2 target)
   {
      GameObject newShot = Instantiate(ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);
      newShot.GetComponent<DirectionShot>().direction = target.normalized;
   }
}
