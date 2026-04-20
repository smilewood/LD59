using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "GunCoolingUpgrade", menuName = "Scriptable Objects/PassiveUpgrades/CoolingUpgrade")]
public class GunCoolingUpgrade : PassiveBase
{
   [Serializable]
   public struct CoolingUpgrade
   {
      public float FirerateMult, ShotSpeedMult;
      public int ShotsAdd;
      [TextArea]
      public string UpgradeText;
   }

   public List<CoolingUpgrade> upgradeTiers;

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
      modifiers.FirerateMult += upgradeTiers[currentTier].FirerateMult;
      modifiers.ShotSpeedMult += upgradeTiers[currentTier].ShotSpeedMult;
      modifiers.ShotsAdd += upgradeTiers[currentTier].ShotsAdd;
   }
}
