using UnityEngine;

public class MenuReferences : MonoBehaviour
{
   public static MenuReferences Instance;
   private void Start()
   {
      Instance = this;
   }

   public GameObject EquipmentSelectionMenu;
}
