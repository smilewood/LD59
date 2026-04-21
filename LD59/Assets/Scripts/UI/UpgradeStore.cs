using TMPro;
using UnityEngine;

public class UpgradeStore : MonoBehaviour
{
   public TMP_Text pointsText;
   public string PointsMessage;
   public PlayerUpgrades upgrades;
   private void OnEnable()
   {
      pointsText.text = string.Format(PointsMessage, upgrades.SignalPoints);
   }
}
