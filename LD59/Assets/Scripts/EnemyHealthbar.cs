using TMPro;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
   public EnemyHealth Enemy;
   public RectTransform HealthBarGraphic;

   public Transform DamageTextParent;
   public GameObject DamageTextPrefab;

   private float initialWidth;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start() 
   {
      initialWidth = HealthBarGraphic.sizeDelta.x;
      Enemy.OnDamageRecieved.AddListener(UpdateUI);
      Enemy.ApplyDamage(0);
   }

   public void UpdateUI(int damageNumber, int current, int max)
   {
      HealthBarGraphic.sizeDelta = new Vector2(Mathf.Lerp(0, initialWidth, (float)current / (float)max), HealthBarGraphic.sizeDelta.y);
      if (damageNumber > 0)
      {
         GameObject damageText = Instantiate(DamageTextPrefab, DamageTextParent);
         damageText.GetComponent<TMP_Text>().text = damageNumber.ToString();
         damageText.GetComponent<Animator>().SetFloat("TextSpeedMult", Random.Range(.8f, 1.2f));
      }
   }

}
