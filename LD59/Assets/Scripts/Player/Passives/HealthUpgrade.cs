using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Scriptable Objects/PassiveUpgrades/HealthUpgrade")]
public class HealthUpgrade : PassiveBase
{
   [Serializable]
   public struct HealthUpgradeTier
   {
      public int BonusHealth;
      public float HealthRegen;
      [TextArea]
      public string UpgradeText;
   }

   public List<HealthUpgradeTier> upgradeTiers;

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
      modifiers.HealthAdd += upgradeTiers[currentTier].BonusHealth;
      modifiers.HealthRegen += upgradeTiers[currentTier].HealthRegen;
   }
}
