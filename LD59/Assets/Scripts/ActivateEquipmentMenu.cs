using UnityEngine;

public class ActivateEquipmentMenu : MonoBehaviour
{
   public void ActivateSelectionMenu()
   {
      MenuReferences.Instance.EquipmentSelectionMenu.SetActive(true);
      MenuReferences.Instance.EquipmentSelectionMenu.GetComponent<EquipmentSelectionChoice>().SetupChoice();
   }
}
