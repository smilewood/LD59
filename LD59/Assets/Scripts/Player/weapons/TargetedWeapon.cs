using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class TargetedWeapon<T> : Weapon<T> where T : WeaponUpgradeTier
{
   private InputAction fireAction;
   private InputAction mouseAction;
   private bool AttackHeld = false;
   
   private bool ReadyToFire = true;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Start()
   {
      fireAction = InputSystem.actions.FindAction("Attack");
      mouseAction = InputSystem.actions.FindAction("Mouse");
      fireAction.performed += AttackClicked;
      fireAction.canceled += AttackReleased;
      base.Start();
   }

   private void AttackClicked(InputAction.CallbackContext context)
   {
      AttackHeld = true;
      if (ReadyToFire)
      {
         StartCoroutine(OpenFire());
      }
   }

   private void AttackReleased(InputAction.CallbackContext context)
   {
      AttackHeld = false;
   }

   private IEnumerator OpenFire()
   {
      do
      {
         ReadyToFire = false;
         Vector2 vectorToMouse = Camera.main.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>()) - this.transform.position;
         FireAtTarget(vectorToMouse);
         yield return new WaitForSeconds(CooldownToNextShot());
         ReadyToFire = true;
      }while(AttackHeld);
   }

   public abstract void FireAtTarget(Vector2 target);

}
