using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(PlayerMove))]
//TODO: Autogun upgrades should change how much it fires. 
public class AutoGun : PassiveWeapon
{
   [Header("AutoGun Settings")]
   public int DirectionsToShoot;
   public float ShotDelay;

   [Header("Shot Settings")]
   public int ShotPierce;
   public float ShotSpeed;
   public GameObject ShotPrefab;
   public Transform ShotParent;

   private PlayerMove playerFace;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      playerFace = this.gameObject.GetComponent<PlayerMove>();
     
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      ShotDelay *= upgradeStatus.FirerateEffects[upgradeStatus.FirerateLevel].Effect;
      ShotPierce += upgradeStatus.PierceEffects[upgradeStatus.PierceLevel].Effect;

      base.Start();
   }

   public override float CooldownToNextShot()
   {
      return FireCooldown + (ShotDelay * (DirectionsToShoot - 1));
   }

   public override void FireWeapon()
   {
      StartCoroutine(FireInDirections());
   }

   private IEnumerator FireInDirections()
   {
      float AnglePerShot = (2f * Mathf.PI) / (float)DirectionsToShoot;

      Quaternion rotation = Quaternion.FromToRotation(Vector2.right, playerFace.CurrentDirection);


      for (int i = 0; i < DirectionsToShoot; i++) 
      {
         GameObject newShot = Instantiate(ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);

         Vector2 shotDirection = (rotation * new Vector2 (Mathf.Cos(AnglePerShot * i), Mathf.Sin(AnglePerShot * i))).normalized;

         newShot.GetComponent<DirectionShot>().InitializeShot(shotDirection, ShotSpeed, Damage, ShotPierce);
         
         yield return new WaitForSeconds(ShotDelay);
      }
   }
}
