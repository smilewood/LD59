using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunDamageUpgrade", menuName = "Scriptable Objects/PassiveUpgrades/GunDamageUpgrade")]
public class GunDamageUpgrade : PassiveBase
{
   [Serializable]
   public struct DamageUpgrade
   {
      public float DamageMult;
      public int PierceAdd;
      [TextArea]
      public string UpgradeText;
   }

   public List<DamageUpgrade> upgradeTiers;

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
      modifiers.DamageMult += upgradeTiers[currentTier].DamageMult;
      modifiers.PierceAdd += upgradeTiers[currentTier].PierceAdd;
   }
}
