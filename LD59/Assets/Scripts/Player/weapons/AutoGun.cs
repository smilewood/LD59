using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(PlayerMove))]
//TODO: Autogun upgrades should change how much it fires. 
public class AutoGun : PassiveWeapon
{
   public int DirectionsToShoot;
   public float ShotDelay;

   public GameObject shotPrefab;
   public Transform shotParent;
   private PlayerMove playerFace;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      playerFace = this.gameObject.GetComponent<PlayerMove>();
     
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      ShotDelay *= upgradeStatus.FirerateEffects[upgradeStatus.FirerateLevel].Effect;

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
         GameObject newShot = Instantiate(shotPrefab, this.transform.position, Quaternion.identity, shotParent);

         Vector2 shotDirection = (rotation * new Vector2 (Mathf.Cos(AnglePerShot * i), Mathf.Sin(AnglePerShot * i))).normalized;
         Debug.Log(shotDirection);

         newShot.GetComponent<DirectionShot>().direction = shotDirection;
         
         yield return new WaitForSeconds(ShotDelay);
      }
   }
}
