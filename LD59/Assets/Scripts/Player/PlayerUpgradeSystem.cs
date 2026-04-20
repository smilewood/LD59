using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public struct PlayerEquipmentModifiers
{
   public float SpeedMult, SpeedAdd;
   public int DamageAdd;
   public float DamageMult;
   public float FirerateMult;
   public int ShotsAdd, PierceAdd;
   public int HealthAdd;
   public float HealthRegen;
   public float ShotSpeedMult;
   public float AreaAdd;

   public float Firerate(float weaponBaseRate)
   {
      return (weaponBaseRate) * (1 / FirerateMult);
   }

   public int Damage(int baseDamage)
   {
      return (int)((baseDamage + DamageAdd) * DamageMult);
   }

   public float Speed(float baseSpeed)
   {
      return (baseSpeed + SpeedAdd) * SpeedMult;
   }
}

public class PlayerUpgradeSystem : MonoBehaviour
{
   public static UnityEvent ModifiresUpdated = new UnityEvent();
   public List<PassiveBase> availablePassiveItems;
   private List<IEquipmentSlotItem> availablePassiveWeapons;
   public List<string> availablePassiveWeaponNames;

   public int NumberOfSlots;

   private List<PassiveSlot> PassiveSlots = new List<PassiveSlot>(4);
   private IEquipmentSlotItem ActiveItem;

   private PlayerUpgrades upgradeStatus;

   private PlayerEquipmentModifiers modifiers;
   public PlayerEquipmentModifiers CurrentModifiers
   {
      get => modifiers;
   }

   void Awake()
   {
      upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      
      UpdateModifiers();
   }

   private void Start()
   {
      //I am only going to have time for one main weapon for the jam, this should be adjustable in theory.
      ActiveItem = gameObject.GetComponent<Blaster>();

      availablePassiveWeapons = new List<IEquipmentSlotItem>();
      availablePassiveWeaponNames = new List<string>();
      foreach (MonoBehaviour weapon in this.gameObject.GetComponents<IPassiveWeapon>())
      {
         availablePassiveWeapons.Add(weapon as IPassiveWeapon);
         weapon.enabled = false;
         availablePassiveWeaponNames.Add(weapon.ToString());
      }
      

      //TODO: Adding one of each by default to test behavior, this should be removed
      //AddPassive(availablePassiveItems[0]);
      //AddPassive(availablePassiveWeapons[0]);
   }

   public void AddPassive(IEquipmentSlotItem item)
   {
      if (PassiveSlots.Count() < 4)
      {
         PassiveSlots.Add(new PassiveSlot { item = item, CurrentTier = 0 });
         if (item is IPassiveWeapon weapon)
         {
            (weapon as MonoBehaviour).enabled = true;
         }
      }
   }

   public void UpdateModifiers()
   {
      PlayerEquipmentModifiers modifiers = new PlayerEquipmentModifiers
      {
         DamageAdd = upgradeStatus.DamageEffects[upgradeStatus.DamageLevel].AddedDamage,
         DamageMult = upgradeStatus.DamageEffects[upgradeStatus.DamageLevel].DamageMultiplier,
         SpeedAdd = 0,
         SpeedMult = upgradeStatus.SpeedBoostEffects[upgradeStatus.SpeedBoostLevel].SpeedMultiplier,
         FirerateMult = upgradeStatus.FirerateEffects[upgradeStatus.FirerateLevel].FirerateMultiplier,
         ShotsAdd = 0,
         PierceAdd = upgradeStatus.PierceEffects[upgradeStatus.PierceLevel].AdditionalPierce,
         HealthAdd = upgradeStatus.HealthBoostEffects[upgradeStatus.HealthBoostLevel].MaxHealth,
         HealthRegen = upgradeStatus.HealthBoostEffects[upgradeStatus.HealthBoostLevel].HealthRegen,
         ShotSpeedMult = 1,
         AreaAdd = 0
      };

      foreach (PassiveSlot slot in PassiveSlots)
      {
         slot.item.UpdateModifiers(ref modifiers, slot.CurrentTier);
      }

      this.modifiers = modifiers;

      ModifiresUpdated.Invoke();
   }

   public (IEquipmentSlotItem, IEquipmentSlotItem) GetUpgradeChoices()
   {
      int choice1 = Random.Range(-1, PassiveSlots.Count());
      int choice2 = -2;
      if (PassiveSlots.Any())
      {
         do
         {
            choice2 = Random.Range(-1, PassiveSlots.Count());
         } while (choice2 == choice1);
      }

      return (itemAtIndex(choice1), choice2 == -2 ? null : itemAtIndex(choice2));

      IEquipmentSlotItem itemAtIndex(int index)
      {
         return index == -1 ? ActiveItem : PassiveSlots[index];
      }
   }

   public IEnumerable<IEquipmentSlotItem> GetAvailablePassiveEquipment()
   {
      IEnumerable<IEquipmentSlotItem> availableWeapons = availablePassiveWeapons.Where(w => !PassiveSlots.Select(s => s.item).Contains(w));
      IEnumerable<IEquipmentSlotItem> availableItems = availablePassiveItems.Where(i => !PassiveSlots.Select(s => s.item).Contains(i));
      return availableItems.Concat(availableWeapons);
   }


}
