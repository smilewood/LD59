using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
   //This is a (less bad) hack for a game jam and I feel bad about it, but I don't have time to set up events properly anymore
   public TMP_Text levelText;
   public string LevelMessage;
   public int lastLevel;

   float initialDelta;
   private void Start()
   {
      initialDelta = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
   }

   public void UpdateBar(int current, int max, int level = -1)
   {
      RectTransform bar = this.gameObject.GetComponent<RectTransform>();

      bar.sizeDelta = new Vector2(Mathf.Lerp(0, initialDelta, (float)current / (float)max), bar.sizeDelta.y);

      if (level >= 0)
      {
         levelText.text = string.Format(LevelMessage, level);
         lastLevel = level;
      }
      else
      {
         levelText.text = string.Format(LevelMessage, lastLevel + 1);
      }
   }
}
