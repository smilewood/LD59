using UnityEngine;
using UnityEngine.InputSystem;

public class MenuReferences : MonoBehaviour
{
   public static MenuReferences Instance;
   private void Start()
   {
      Instance = this;


      InputSystem.actions.FindAction("Menu").performed += TogglePauseMenu;
   }

   public GameObject EquipmentSelectionMenu;

   public GameObject PauseMenu;

   private void TogglePauseMenu(InputAction.CallbackContext _)
   {
      TogglePauseMenu();
   }
   public void TogglePauseMenu()
   {
      if (PauseMenu.activeSelf)
      {
         PauseMenu.SetActive(false);
         Time.timeScale = 1;
      }
      else
      {
         PauseMenu.SetActive(true);
         Time.timeScale = 0;
      }
   }
}
