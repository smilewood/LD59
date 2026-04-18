using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
   InputAction moveAction;

   public float speed = 1;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      speed = upgradeStatus.SpeedBoostEffects[upgradeStatus.SpeedBoostLevel].Effect;
      moveAction = InputSystem.actions.FindAction("Move");
   }

   // Update is called once per frame
   void Update()
   {
      Vector2 delta = moveAction.ReadValue<Vector2>().normalized * speed * Time.deltaTime;
      this.transform.position = this.transform.position + new Vector3(delta.x, delta.y, this.transform.position.z);
   }
}
