using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class EquipmentSelectionChoice : MonoBehaviour
{
   public ChoicePanelSetup Panel1, Panel2, Panel3;

   public PlayerUpgradeSystem playerUpgrades;
   public EnemySpawnner EnemySystem;

   private void Start()
   {
      this.gameObject.SetActive(false);
   }

   public void SetupChoice()
   {
      Time.timeScale = 0;

      List<IEquipmentSlotItem> equipment = playerUpgrades.GetAvailablePassiveEquipment().OrderBy(e => Random.value).ToList();
      List<EnemyType> enemies = EnemySystem.AvailableEnemies().OrderBy(e => Random.value).ToList();

      Panel1.SetupPanel(equipment[0], enemies[0], ChoiceMade);
      Panel2.SetupPanel(equipment[1], enemies[1], ChoiceMade);
      Panel3.SetupPanel(equipment[2], enemies[2], ChoiceMade);
   }

   public void ChoiceMade(IEquipmentSlotItem item, EnemyType enemyType)
   {
      playerUpgrades.AddPassive(item);
      EnemySystem.AddEnemyType(enemyType);
      this.gameObject.SetActive(false);
      Time.timeScale = 1;
   }


}
