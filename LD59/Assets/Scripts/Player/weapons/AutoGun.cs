using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class AutoGunUpgradeTier : ShotFiringWeaponUpgradeTier
{
   [Header("AutoGun Settings")]
   public int DirectionsToShoot;
   public float ShotDelay;
}

[RequireComponent(typeof(PlayerMove))]
public class AutoGun : PassiveWeapon<AutoGunUpgradeTier>
{
   public Transform ShotParent;

   public int Shots
   {
      get
      {
         return Modifiers.ShotsAdd + Values.DirectionsToShoot;
      }
   }

   private float ShotDelay
   {
      get
      {
         return Modifiers.Firerate(Values.ShotDelay);
      }
   }

   private PlayerMove playerFace;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      playerFace = this.gameObject.GetComponent<PlayerMove>();

      base.Start();
   }

   public override float CooldownToNextShot()
   {
      return FireCooldown + (ShotDelay * (Shots - 1));
   }

   public override void FireWeapon()
   {
      StartCoroutine(FireInDirections());
   }

   private IEnumerator FireInDirections()
   {
      float AnglePerShot = (2f * Mathf.PI) / (float)Shots;

      Quaternion rotation = Quaternion.FromToRotation(Vector2.right, playerFace.CurrentDirection);


      for (int i = 0; i < Shots; i++)
      {
         GameObject newShot = Instantiate(Values.ShotPrefab, this.transform.position, Quaternion.identity, ShotParent);

         Vector2 shotDirection = (rotation * new Vector2(Mathf.Cos(AnglePerShot * i), Mathf.Sin(AnglePerShot * i))).normalized;

         newShot.GetComponent<DirectionShot>().InitializeShot(shotDirection, Values.ShotSpeed * Modifiers.ShotSpeedMult, Damage, Values.ShotPierce + Modifiers.PierceAdd);

         yield return new WaitForSeconds(ShotDelay);
      }
   }
}
