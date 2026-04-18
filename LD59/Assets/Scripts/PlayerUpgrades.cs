using System;
using UnityEngine;


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
      public int Effect;
   }

   [NonSerialized]
   public int HealthBoostLevel = 0;
   public HealthUpgrade[] HealthBoostEffects;


   [Serializable]
   public class SpeedUpgrade : Upgrade
   {
      public float Effect;
   }

   [NonSerialized]
   public int SpeedBoostLevel = 0;
   public SpeedUpgrade[] SpeedBoostEffects;


   [Serializable]
   public class FirerateUpgrade : Upgrade
   {
      public float Effect;
   }

   [NonSerialized]
   public int FirerateLevel = 0;
   public FirerateUpgrade[] FirerateEffects;
}
