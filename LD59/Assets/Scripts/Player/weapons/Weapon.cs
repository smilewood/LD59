using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WeaponUpgradeTier
{
   public int Damage;
   [Tooltip("Time between shots")]
   public float FireCooldown;
   [TextArea]
   public string UpgradeText;
}
public abstract class Weapon<T> : MonoBehaviour, IEquipmentSlotItem where T : WeaponUpgradeTier
{
   public string Name;
   public List<T> Upgrades;
   public T Values => Upgrades[currentTier];
   protected int currentTier;

   protected float FireCooldown => Modifiers.Firerate(Upgrades[currentTier].FireCooldown);
   private PlayerUpgradeSystem PlayerUpgrades;
   protected PlayerEquipmentModifiers Modifiers => PlayerUpgrades.CurrentModifiers;

   public int Damage
   {
      get
      {
         return Modifiers.Damage(Upgrades[currentTier].Damage) + UnityEngine.Random.Range(-2, 2);
      }
   }

   public int UpgradeTier
   {
      get
      {
         return currentTier;
      }
   }

   public string EquipmentName
   {
      get
      {
         return Name;
      }
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public virtual void Start()
   {
      PlayerUpgrades = this.gameObject.GetComponent<PlayerUpgradeSystem>();
   }


   public abstract float CooldownToNextShot();

   public void UpdateModifiers(ref PlayerEquipmentModifiers modifiers, int currentTier)
   {
      //Do nothing, weapons currently don't affect the overall build.
      //Maybe they could slow you down at some point? Not worth passing all the way down the chain
   }

   public virtual bool HasUpgrade(int currentTier)
   {
      return currentTier < Upgrades.Count - 1;
   }

   public virtual void ApplyUpgradeTier(int newTier)
   {
      currentTier = newTier;
   }
   public string GetUpgradeText(int tier)
   {
      return Upgrades[tier].UpgradeText;
   }
}
