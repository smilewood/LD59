using UnityEngine;

public class SimpleEnemyFollow : MonoBehaviour
{
   private Transform target;
   public float speed;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      target = GameObject.Find("PlayerRoot").transform;
   }

   // Update is called once per frame
   void Update()
   {
      Vector3 move = (target.position - this.transform.position).normalized * speed * Time.deltaTime;
      this.transform.position += move;
   }
}
