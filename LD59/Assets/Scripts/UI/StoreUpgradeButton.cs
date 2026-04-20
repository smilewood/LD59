using System;
using System.Linq;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreUpgradeButton : MonoBehaviour
{
   private static UnityEvent UpgradePurchased = new UnityEvent();
   public enum UpgradeType
   {
      HealthBoost,
      SpeedBoost,
      Firerate,
      Damage,
      Pierce
   }
   private PlayerUpgrades upgradeSource;
   private Button buyButton;
   private TMP_Text displayName;
   private Image displayImage;
   public UpgradeType ShopType;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      UpgradePurchased.AddListener(UpdateShopStatus);
      upgradeSource = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      buyButton = gameObject.GetComponentInChildren<Button>();
      buyButton.onClick.AddListener(BuyUpgrade);
      displayImage = transform.Find("Image").GetComponent<Image>();
      displayName = transform.Find("Name").GetComponent<TMP_Text>();
      UpdateShopStatus();
   }

   private void UpdateShopStatus()
   {
      if (UpgradeLevel < MaxUpgradeLevel)
      {
         buyButton.interactable = upgradeSource.SignalPoints >= UpgradeForType.Cost;
         buyButton.GetComponentInChildren<TMP_Text>().text = UpgradeForType.Cost.ToString();
         displayImage.sprite = UpgradeForType.StoreImage;
         displayName.text = UpgradeForType.UpgradeName;
      }
      else
      {
         buyButton.interactable = false;
         buyButton.GetComponentInChildren<TMP_Text>().text = "Max Level";
         displayImage.sprite = UpgradeForLevel(UpgradeLevel-1).StoreImage;
         displayName.text = UpgradeForLevel(UpgradeLevel-1).UpgradeName;
      }
   }

   public void BuyUpgrade()
   {
      upgradeSource.SignalPoints -= UpgradeForType.Cost;
      UpgradeLevel = UpgradeLevel;
      UpgradePurchased.Invoke();
   }

   private int UpgradeLevel
   {
      get
      {
         int result;
         switch (ShopType)
         {
            case UpgradeType.HealthBoost:
            {
               result = upgradeSource.HealthBoostLevel;
               break;
            }
            case UpgradeType.SpeedBoost:
            {
               result = upgradeSource.SpeedBoostLevel;
               break;
            }
            case UpgradeType.Firerate:
            {
               result = upgradeSource.FirerateLevel;
               break;
            }
            case UpgradeType.Damage:
            {
               result = upgradeSource.DamageLevel;
               break;
            }
            case UpgradeType.Pierce:
            {
               result = upgradeSource.PierceLevel;
               break;
            }
            default:
            {
               result = -1;
               break;
            }
         }
         return result + 1;
      }
      set
      {
         switch (ShopType)
         {
            case UpgradeType.HealthBoost:
            {
               upgradeSource.HealthBoostLevel = value;
               break;
            }
            case UpgradeType.SpeedBoost:
            {
               upgradeSource.SpeedBoostLevel = value;
               break;
            }
            case UpgradeType.Firerate:
            {
               upgradeSource.FirerateLevel = value;
               break;
            }
            case UpgradeType.Damage:
            {
               upgradeSource.DamageLevel = value;
               break;
            }
            case UpgradeType.Pierce:
            {
               upgradeSource.PierceLevel = value;
               break;
            }
         }
      }
   }

   private int MaxUpgradeLevel
   {
      get
      {
         switch (ShopType)
         {
            case UpgradeType.HealthBoost:
            {
               return upgradeSource.HealthBoostEffects.Length;
            }
            case UpgradeType.SpeedBoost:
            {
               return upgradeSource.SpeedBoostEffects.Length;
            }
            case UpgradeType.Firerate:
            {
               return upgradeSource.FirerateEffects.Length;
            }
            case UpgradeType.Damage:
            {
               return upgradeSource.DamageEffects.Length;
            }
            case UpgradeType.Pierce:
            {
               return upgradeSource.PierceEffects.Length;
            }
            default:
            {
               return -1;
            }
         }
      }
   }

   private PlayerUpgrades.Upgrade UpgradeForType
   {
      get
      {
         return UpgradeForLevel(UpgradeLevel);
      }
   }

   private PlayerUpgrades.Upgrade UpgradeForLevel(int upgradeLevel)
   {
      switch (ShopType)
      {
         case UpgradeType.HealthBoost:
         {
            return upgradeSource.HealthBoostEffects[upgradeLevel];
         }
         case UpgradeType.SpeedBoost:
         {
            return upgradeSource.SpeedBoostEffects[upgradeLevel];
         }
         case UpgradeType.Firerate:
         {
            return upgradeSource.FirerateEffects[upgradeLevel];
         }
         case UpgradeType.Damage:
         {
            return upgradeSource.DamageEffects[upgradeLevel];
         }
         case UpgradeType.Pierce:
         {
            return upgradeSource.PierceEffects[upgradeLevel];
         }
         default:
         {
            return null;
         }
      }
   }

   // Update is called once per frame
   void Update()
   {

   }
}
