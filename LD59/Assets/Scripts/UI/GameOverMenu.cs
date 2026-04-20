using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
   private int totalKills;
   private int InitialSignalPoints;
   public GameObject GameOverPanel;

   public string KillsMessage;
   public TMP_Text KillsText;

   public string PointsMessage;
   public PlayerUpgrades Upgrades;
   public TMP_Text PointsText;


   private void Start()
   {
      EnemyHealth.OnEnemyKilled.AddListener(() => ++totalKills);
      PlayerHealth.OnPlayerHealthChanged.AddListener((int hp, int _) => { if (hp <= 0) { ShowGameOver(); } });
      InitialSignalPoints = Upgrades.SignalPoints;
      GameOverPanel.SetActive(false);
   }

   public void ShowGameOver()
   {
      GameOverPanel.SetActive(true);
      Time.timeScale = 0;
      KillsText.text = string.Format(KillsMessage, totalKills);
      PointsText.text = string.Format(PointsMessage, Upgrades.SignalPoints - InitialSignalPoints);
   }

   public void Continue()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene("MainMenu");
   }

}
