using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

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

   public float Firerate(float weaponBaseRate)
   {
      return (weaponBaseRate) * (1/FirerateMult);
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
   public List<PassiveBase> availablePassiveItems;
   private List<IEquipmentSlotItem> availablePassiveWeapons;
   public List<string> availablePassiveWeaponNames;

   public int NumberOfSlots;

   private List<PassiveSlot> PassiveSlots = new List<PassiveSlot>(4);

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
      availablePassiveWeapons = new List<IEquipmentSlotItem>();
      availablePassiveWeaponNames = new List<string>();
      foreach (MonoBehaviour weapon in this.gameObject.GetComponents<IPassiveWeapon>())
      {
         availablePassiveWeapons.Add(weapon as IPassiveWeapon);
         weapon.enabled = false;
         availablePassiveWeaponNames.Add(weapon.ToString());
      }

      //TODO: Adding one of each by default to test behavior, this should be removed
      AddPassive(availablePassiveItems[0]);
      AddPassive(availablePassiveWeapons[0]);
   }

   public void AddPassive(IEquipmentSlotItem item)
   {
      if(PassiveSlots.Count() < 4)
      {
         PassiveSlots.Add(new PassiveSlot { item = item, UpgradeTier = 0 });
         if(item is PassiveWeapon<WeaponUpgradeTier> weapon)
         {
            weapon.enabled = true;
         }
      }
   }

   // Update is called once per frame
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
         ShotSpeedMult = 1
      };

      foreach (PassiveSlot slot in PassiveSlots)
      {
         slot.item.UpdateModifiers(ref modifiers, slot.UpgradeTier);
      }

      this.modifiers = modifiers;
   }
}
