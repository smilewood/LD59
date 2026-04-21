using System;
using UnityEngine;
using UnityEngine.Events;

//TODO: This is going to need a little reworking since the upgrades are going to persist in the editor
//For now I am going to just reset the levels on launch. 

[CreateAssetMenu(fileName = "PlayerUpgrades", menuName = "Scriptable Objects/PlayerUpgrades")]
public class PlayerUpgrades : ScriptableObject
{
   public UnityEvent<int> PointsUpdated;

   public int SignalPoints;
   public int initialPoints;

   [Serializable]
   public abstract class Upgrade
   {
      public int Cost;
      public Sprite StoreImage;
      public string UpgradeName;
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

   [Serializable]
   public class AreaUpgrade : Upgrade
   {
      public float AdditionalArea;
   }

   [NonSerialized]
   public int AreaLevel = 0;
   public AreaUpgrade[] AreaEffects;

   private void OnEnable()
   {
      SignalPoints = initialPoints;
      HealthBoostLevel = SpeedBoostLevel = FirerateLevel = DamageLevel = PierceLevel = AreaLevel = 0;
      PointsUpdated = new UnityEvent<int>();
   }
   public void AddPoint()
   {
      SpendPoints(-1);
   }
   public void SpendPoints(int amount)
   {
      SignalPoints -= amount;
      PointsUpdated.Invoke(SignalPoints);
   }
}
