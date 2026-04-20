using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
   float initialDelta;
   private void Start()
   {
      initialDelta = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
   }

   public void UpdateBar(int current, int max)
   {
      RectTransform bar = this.gameObject.GetComponent<RectTransform>();

      bar.sizeDelta = new Vector2(Mathf.Lerp(0, initialDelta, (float)current / (float)max), bar.sizeDelta.y);
   }


}
