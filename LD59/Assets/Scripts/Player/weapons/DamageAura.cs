using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageAura : PassiveWeapon
{
   [Tooltip("Ticks per second")]
   public float TickRate;
   public float Radius;

   private CircleCollider2D damageCollider;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      if(TickRate == 0)
      {
         TickRate = 1;
      }
      FireCooldown = (1f / TickRate);

      damageCollider = gameObject.AddComponent<CircleCollider2D>();
      damageCollider.radius = Radius;
      damageCollider.isTrigger = true;
      damageCollider.enabled = false;
      base.Start();
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
      if (collision.gameObject.CompareTag("Enemy"))
      {
         EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
         if (health != null)
         {
            health.ApplyDamage(Damage);
         }
      }
   }
}
