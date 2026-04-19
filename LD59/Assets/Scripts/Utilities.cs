using Unity.VisualScripting;
using UnityEngine;

public static class Utilities
{
   public static Vector3 V3(this Vector2 vector)
   {
      return new Vector3(vector.x, vector.y, 0);
   }
}
