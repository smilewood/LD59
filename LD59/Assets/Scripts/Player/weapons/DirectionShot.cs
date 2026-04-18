using UnityEngine;

public class DirectionShot : MonoBehaviour
{
   public float Speed;
   public Vector2 direction;
   public float InitialLifetime;
   public float Pierce;

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
         //TODO: Damage the enemy with damage
         Destroy(collision.gameObject);

         if(--remainingPierce == 0)
         {
            Destroy(this.gameObject);
         }
      }
   }

   // Update is called once per frame
   void Update()
   {
      this.transform.position += new Vector3(direction.x, direction.y) * Speed * Time.deltaTime;
      remainingLifetime -= Time.deltaTime;
      if(remainingLifetime <= 0)
      {
         Destroy(this.gameObject);
      }
   }
}
