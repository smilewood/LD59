using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
   public EnemyHealth Enemy;
   public RectTransform HealthBarGraphic;

   private float initialWidth;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start() 
   {
      initialWidth = HealthBarGraphic.sizeDelta.x;
      Enemy.OnDamageRecieved.AddListener(UpdateUI);
      Enemy.ApplyDamage(0);
   }

   public void UpdateUI(int _, int current, int max)
   {
      HealthBarGraphic.sizeDelta = new Vector2(Mathf.Lerp(0, initialWidth, (float)current / (float)max), HealthBarGraphic.sizeDelta.y);
   }

}
