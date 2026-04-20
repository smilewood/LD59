using System;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentSlotItem
{
   public void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier);

   public bool HasUpgrade(int currentTier);

   public GameObject GetUpgradeButton(int tier);

   public void ApplyUpgradeTier(int newTier);
}

[Serializable]
public struct PassiveSlot
{
   public IEquipmentSlotItem item;
   public int UpgradeTier;
}

public abstract class PassiveBase : ScriptableObject, IEquipmentSlotItem
{
   public Sprite Icon;

   public abstract void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier);

   public abstract bool HasUpgrade(int currentTier);

   public abstract GameObject GetUpgradeButton(int currentTier);

   public void ApplyUpgradeTier(int newTier)
   {
      //Passive Items don't really need to do anything. Maybe in the future this will let them change out a visual thing? 
   }
}
