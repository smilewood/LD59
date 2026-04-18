using System.Collections;
using UnityEngine;

public class EnemyCollideDamage : MonoBehaviour
{
   public int ContactDamage;
   public float Cooldown;

   private bool onCooldown = false;
   private void OnTriggerStay2D(Collider2D collision)
   {
      if (!onCooldown && collision.CompareTag("Player"))
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
