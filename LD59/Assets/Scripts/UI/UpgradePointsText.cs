using TMPro;
using UnityEngine;

public class UpgradePointsText : MonoBehaviour
{
   public TMP_Text pointsText;
   public string PointsMessage;
   public PlayerUpgrades upgrades;

   private void Start()
   {
      upgrades.PointsUpdated.AddListener(UpdateText);   
   }

   private void OnEnable()
   {
      UpdateText(upgrades.SignalPoints);
   }

   public void UpdateText(int newPoints)
   {
      pointsText.text = string.Format(PointsMessage, newPoints);
   }
}
