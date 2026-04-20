using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AuraUpgradeTier : WeaponUpgradeTier
{
   [Tooltip("Ticks per second")]
   public float TickRate;
   public float Radius;
}

public class DamageAura : PassiveWeapon<AuraUpgradeTier>
{
   public GameObject AuraImage;
   private CircleCollider2D damageCollider;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      damageCollider = gameObject.AddComponent<CircleCollider2D>();
      damageCollider.isTrigger = true;
      damageCollider.enabled = false;
      damageCollider.excludeLayers |= LayerMask.GetMask("InteractionArea");

      base.Start();
      ApplyUpgradeTier(0);
   }

   private void OnEnable()
   {
      AuraImage.SetActive(true);
   }

   public override float CooldownToNextShot()
   {
      return FireCooldown;
   }

   public override void FireWeapon()
   {
      StartCoroutine(PulseAura());
   }
   private IEnumerator PulseAura()
   {
      damageCollider.enabled = true;
      yield return new WaitForFixedUpdate();
      damageCollider.enabled = false;
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (enabled && collision.gameObject.CompareTag("Enemy"))
      {
         EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
         if (health != null)
         {
            health.ApplyDamage(Damage);
         }
      }
   }

   public override void ApplyUpgradeTier(int newTier)
   {
      currentTier = newTier;
      if (Values.TickRate == 0)
      {
         Values.TickRate = 1;
      }
      Values.FireCooldown = (1f / Values.TickRate);
      damageCollider.radius = Values.Radius;
      AuraImage.transform.localScale = Vector3.one * Values.Radius * 2;

   }
}
