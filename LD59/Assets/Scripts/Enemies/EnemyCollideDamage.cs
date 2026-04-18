using System.Collections;
using UnityEngine;

public class EnemyCollideDamage : MonoBehaviour
{
   public int ContactDamage;
   public float Cooldown;

   private bool onCooldown = false;

   private void OnCollisionStay2D(Collision2D collision)
   {
      Debug.Log($"Collided with {collision.collider.gameObject.name}");
      if (!onCooldown && collision.collider.CompareTag("Player"))
      {
         PlayerHealth.DamagePlayer.Invoke(ContactDamage);
         StartCoroutine(AttackCooldown());
      }
   }

   private IEnumerator AttackCooldown()
   {
      onCooldown = true;
      yield return new WaitForSeconds(Cooldown);
      onCooldown = false;
   }

}
