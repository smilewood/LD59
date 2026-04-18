using System.Linq;
using UnityEngine;

public class test : MonoBehaviour
{
   
   public void buttonEffect()
   {
      PlayerUpgrades upgradeStatus = Resources.FindObjectsOfTypeAll<PlayerUpgrades>().First();
      upgradeStatus.SpeedBoostLevel = 1;
   }

}
