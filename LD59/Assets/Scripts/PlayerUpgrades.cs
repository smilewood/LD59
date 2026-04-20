using System;
using UnityEngine;

//TODO: This is going to need a little reworking since the upgrades are going to persist in the editor
//For now I am going to just reset the levels on launch. 

[CreateAssetMenu(fileName = "PlayerUpgrades", menuName = "Scriptable Objects/PlayerUpgrades")]
public class PlayerUpgrades : ScriptableObject
{
   [Serializable]
   public abstract class Upgrade
   {
      public int Cost;
   }

   [Serializable]
   public class HealthUpgrade : Upgrade
   {
      public int MaxHealth;
      public int HealthRegen;
   }

   [NonSerialized]
   public int HealthBoostLevel = 0;
   public HealthUpgrade[] HealthBoostEffects;


   [Serializable]
   public class SpeedUpgrade : Upgrade
   {
      public float SpeedMultiplier;
   }

   [NonSerialized]
   public int SpeedBoostLevel = 0;
   public SpeedUpgrade[] SpeedBoostEffects;


   [Serializable]
   public class FirerateUpgrade : Upgrade
   {
      public float FirerateMultiplier;
   }

   [NonSerialized]
   public int FirerateLevel = 0;
   public FirerateUpgrade[] FirerateEffects;

   [Serializable]
   public class DamageUpgrade : Upgrade
   {
      public float DamageMultiplier;
      public int AddedDamage;
   }

   [NonSerialized]
   public int DamageLevel = 0;
   public DamageUpgrade[] DamageEffects;

   [Serializable]
   public class PierceUpgrade : Upgrade
   {
      public int AdditionalPierce;
   }

   [NonSerialized]
   public int PierceLevel = 0;
   public PierceUpgrade[] PierceEffects;

   private void OnEnable()
   {
      HealthBoostLevel = SpeedBoostLevel = FirerateLevel = DamageLevel = PierceLevel = 0;
   }
}
