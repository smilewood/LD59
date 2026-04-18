using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
   InputAction moveAction;

   //This has to be here or the system sometimes can't find the object. super annoyingly inconsistant but this is a game jam so here Unity have this. 
   public PlayerUpgrades upgradeStatus;
   
   public float speed = 1;

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
      upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      speed = upgradeStatus.SpeedBoostEffects[upgradeStatus.SpeedBoostLevel].Effect;
      moveAction = InputSystem.actions.FindAction("Move");
   }

   // Update is called once per frame
   void Update()
   {
      CurrentDirection = moveAction.ReadValue<Vector2>().normalized;
      Vector2 delta = moveAction.ReadValue<Vector2>().normalized * speed * Time.deltaTime;
      this.transform.position = this.transform.position + new Vector3(delta.x, delta.y, this.transform.position.z);
   }
}
