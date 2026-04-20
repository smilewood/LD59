using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Scriptable Objects/PassiveUpgrades/SpeedUpgrade")]
public class SpeedUpgrade : PassiveBase
{
   [Serializable]
   public struct SpeedUpgradeTier
   {
      public float SpeedAdd, AreaAdd;
      [TextArea]
      public string UpgradeText;
   }

   public List<SpeedUpgradeTier> upgradeTiers;

   public override string GetUpgradeText(int currentTier)
   {
      return upgradeTiers[currentTier].UpgradeText;
   }

   public override bool HasUpgrade(int currentTier)
   {
      return currentTier < upgradeTiers.Count - 1;
   }

   public override void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier)
   {
      modifiers.SpeedAdd += upgradeTiers[currentTier].SpeedAdd;
      modifiers.AreaAdd += upgradeTiers[currentTier].AreaAdd;
   }
}
