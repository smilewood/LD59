using System;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentSlotItem
{
   public void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier);

   public bool HasUpgrade(int currentTier);

   public string GetUpgradeText(int tier);

   public void ApplyUpgradeTier(int newTier);

   public string EquipmentName { get; }

   public string EquipmentDescription { get; }

   public int UpgradeTier { get; }
}

[Serializable]
public class PassiveSlot : IEquipmentSlotItem
{
   public IEquipmentSlotItem item;
   public int CurrentTier;

   public int UpgradeTier
   {
      get
      {
         return CurrentTier;
      }
   }

   public string EquipmentName
   {
      get
      {
         return item.EquipmentName;
      }
   }

   public string EquipmentDescription
   {
      get
      {
         return item.EquipmentDescription;
      }
   }

   public void ApplyUpgradeTier(int newTier)
   {
       CurrentTier = newTier;
   }

   public string GetUpgradeText(int tier)
   {
      return item.GetUpgradeText(tier);
   }

   public bool HasUpgrade(int currentTier)
   {
      return item.HasUpgrade(currentTier);
   }

   public void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier)
   {
      item.UpdateModifiers(ref modifiers, currentTier);
   }
}

public abstract class PassiveBase : ScriptableObject, IEquipmentSlotItem
{
   public Sprite Icon;

   public int UpgradeTier
   {
      get
      {
         throw new NotImplementedException();
      }
   }
   public string Name;
   public string EquipmentName
   {
      get
      {
         return Name;
      }
   }

   [TextArea]
   public string Description;
   public string EquipmentDescription
   {
      get
      {
         return Description;
      }
   }

   public abstract void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier);

   public abstract bool HasUpgrade(int currentTier);

   public abstract string GetUpgradeText(int currentTier);

   public void ApplyUpgradeTier(int newTier)
   {
      //Passive Items don't really need to do anything. Maybe in the future this will let them change out a visual thing? 
   }
}
