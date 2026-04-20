using System;
using TMPro;
using UnityEngine;

public class ChoicePanelSetup : MonoBehaviour
{
   public TMP_Text EquimentName, EquipmentText;
   public TMP_Text EnemyName, EnemyText;

   private IEquipmentSlotItem selectedItem;
   private EnemyType selectedEnemy;
   private Action<IEquipmentSlotItem, EnemyType> panelSelectedCallback;

   public void SetupPanel(IEquipmentSlotItem equipment, EnemyType enemy, Action<IEquipmentSlotItem, EnemyType> panelSelected)
   {
      panelSelectedCallback = panelSelected;
      selectedItem = equipment;
      selectedEnemy = enemy;

      EquimentName.text = equipment.EquipmentName;
      EquipmentText.text = equipment.EquipmentDescription;

      EnemyName.text = enemy.EnemyName;
      EnemyText.text = enemy.EnemyDescription;
   }

   public void PanelChosen()
   {
      panelSelectedCallback.Invoke(selectedItem, selectedEnemy);
   }
}
