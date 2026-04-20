using UnityEngine;
using UnityEngine.Events;

public class ExperienceTracker : MonoBehaviour
{
   public static UnityEvent ExperiencePickup = new UnityEvent();

   public int CurrentLevel;
   public int currentEXP;
   public int targetExp;
   public int ExpLevelScaler;

   public GameObject LevelUpPanel;


   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      ExperiencePickup.AddListener(OnGetExp);
   }

   public void OnGetExp()
   {
      ++currentEXP;
      if(currentEXP >= targetExp)
      {
         LevelUp();
      }
   }
   public void LevelUp()
   {
      ++CurrentLevel;
      currentEXP = 0;
      targetExp += (int)Mathf.Pow(ExpLevelScaler, Mathf.Log10(CurrentLevel));
      LevelUpPanel.SetActive(true);
      LevelUpPanel.GetComponent<LevelUpChoice>().RunLevelChoice(CurrentLevel);
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("EXP"))
      {
         collision.gameObject.GetComponent<ExperiencePickup>().StartPickup(this.transform);
      }
   }
}
