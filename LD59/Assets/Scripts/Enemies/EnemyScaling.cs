using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScaling", menuName = "Scriptable Objects/EnemyScaling")]
public class EnemyScaling : ScriptableObject
{
   //TODO: different scaling for diferent time sections? Slower until a given time then increase?

   [Tooltip("Time in seconds for the enemy health to double")]
   public float HealthDoubleTime;
   public int GetHealthForTime(int baseHealth)
   {
      return baseHealth + (int)((Time.time  / HealthDoubleTime) * baseHealth);
   }


   [Tooltip("Time in seconds for the enemy speed to double")]
   public float SpeedDoubleTime;
   public float GetSpeedForTime(int baseSpeed)
   {
      return baseSpeed + ((Time.time / SpeedDoubleTime) * baseSpeed);
   }

   public float DamageTimeMultiplier;

   public List<EnemyType> EnemyTypes;
}

[Serializable]
public struct EnemyType
{
   public GameObject EnemyPrefab;
   //public Sprite EnemyImage;
   public string EnemyName;
   [TextArea]
   public string EnemyDescription;
   public int PointsCost;
}
