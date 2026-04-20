using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
   InputAction moveAction;
   private PlayerUpgradeSystem upgrades;
   private float ModifiedSpeed => upgrades.CurrentModifiers.Speed(BaseSpeed);
   public float BaseSpeed;

   private Vector2 currentDirection = Vector2.right;
   public Vector2 CurrentDirection
   {
      get => currentDirection;
      private set
      {
         if(value != Vector2.zero)
         {
            currentDirection = value;
         }
      }
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      upgrades = this.gameObject.GetComponent<PlayerUpgradeSystem>();
      moveAction = InputSystem.actions.FindAction("Move");
   }

   // Update is called once per frame
   void Update()
   {
      CurrentDirection = moveAction.ReadValue<Vector2>().normalized;
      Vector2 delta = moveAction.ReadValue<Vector2>().normalized * ModifiedSpeed * Time.deltaTime;
      this.transform.position = this.transform.position + new Vector3(delta.x, delta.y, this.transform.position.z);
   }
}
