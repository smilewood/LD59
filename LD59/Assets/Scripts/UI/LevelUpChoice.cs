using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoice : MonoBehaviour
{
   public PlayerUpgradeSystem upgradeSystem;
   public ExpBar expBar;
   private IEquipmentSlotItem LeftChoice, RightChoice;
   public GameObject LeftPanel, RightPanel;
   public void RunLevelChoice(int level)
   {
      Time.timeScale = 0;

      (LeftChoice, RightChoice) = upgradeSystem.GetUpgradeChoices();
      SetupPanel(LeftPanel, LeftChoice);
      SetupPanel(RightPanel, RightChoice);
   }
   private void Start()
   {
      this.gameObject.SetActive(false);
   }
   private void SetupPanel(GameObject panel, IEquipmentSlotItem item)
   {
      if (item == null)
      {
         panel.transform.Find("UpgradeName").GetComponent<TMP_Text>().text = "Empty Slot";
         panel.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = "No equipment to upgrade";
         panel.transform.Find("SelectButton").GetComponentInChildren<Button>().interactable = false;
      }
      else
      {
         panel.transform.Find("UpgradeName").GetComponent<TMP_Text>().text = item.EquipmentName;
         if (item.HasUpgrade(item.UpgradeTier + 1))
         {
            panel.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = item.GetUpgradeText(item.UpgradeTier + 1);
            //TODO there should be an image for each upgrade
            //panel.transform.Find("Image").GetComponent<Image>().sprite = item.
            panel.transform.Find("SelectButton").GetComponentInChildren<Button>().interactable = true;
         }
         else
         {
            panel.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = "Max Level";
            panel.transform.Find("SelectButton").GetComponentInChildren<Button>().interactable = false;
         }
      }
   }

   public void MakeLeftChoice()
   {
      LeftChoice?.ApplyUpgradeTier(LeftChoice.UpgradeTier + 1);
      CompleteCoice();
   }

   public void MakeRightChoice()
   {
      RightChoice?.ApplyUpgradeTier(RightChoice.UpgradeTier + 1);
      CompleteCoice();
   }
   public void MakeNoChoice()
   {
      CompleteCoice();
   }

   public void CompleteCoice()
   {
      upgradeSystem.UpdateModifiers();
      Time.timeScale = 1;
      expBar.UpdateBar(0, 1);
      this.gameObject.SetActive(false);
   }
}
