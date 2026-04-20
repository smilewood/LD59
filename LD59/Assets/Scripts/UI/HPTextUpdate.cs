using TMPro;
using UnityEngine;

public class HPTextUpdate : MonoBehaviour
{
   public string DisplayString;
   private TMP_Text text;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Awake()
   {
      text = GetComponent<TMP_Text>();
      PlayerHealth.OnPlayerHealthChanged.AddListener(UpdateText);
   }

   void UpdateText(int current, int max)
   {
      text.text = string.Format(DisplayString, current, max);
   }
}
