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
      public float FirerateMult, SpeedMult;
      public int PierceAdd;
   }

   public List<CoolingUpgrade> upgradeTiers;

   public override GameObject GetUpgradeButton(int currentTier)
   {
      return null;
   }

   public override bool HasUpgrade(int currentTier)
   {
      return currentTier < upgradeTiers.Count - 1;
   }

   public override void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier)
   {
      modifiers.FirerateMult += upgradeTiers[currentTier].FirerateMult;
      modifiers.ShotSpeedMult += upgradeTiers[currentTier].SpeedMult;
      modifiers.PierceAdd += upgradeTiers[currentTier].PierceAdd;
   }
}
