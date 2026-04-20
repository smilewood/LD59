using UnityEngine;

public class DirectionShot : MonoBehaviour
{
   public float Speed;
   public Vector2 direction;
   public float InitialLifetime;
   public float Pierce;
   public int Damage;

   private float remainingLifetime;
   private float remainingPierce;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      remainingLifetime = InitialLifetime;
      remainingPierce = Pierce;
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Enemy"))
      {
         collision.gameObject.SendMessageUpwards("ApplyDamage", Damage);

         if (--remainingPierce == 0)
         {
            Destroy(this.gameObject);
         }
      }
   }

   // Update is called once per frame
   void Update()
   {
      this.transform.position += (Vector3)direction * Speed * Time.deltaTime;
      remainingLifetime -= Time.deltaTime;
      if(remainingLifetime <= 0)
      {
         Destroy(this.gameObject);
      }
   }

   public void InitializeShot(Vector2 target, float speed, int damage, int pierce)
   {
      direction = target;
      Speed = speed;
      Damage = damage;
      Pierce = remainingPierce = pierce;
   }
}
