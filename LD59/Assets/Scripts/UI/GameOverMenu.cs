using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
   private int totalKills;
   public GameObject GameOverPanel;

   public string KillsMessage;
   public TMP_Text KillsText;


   private void Start()
   {
      EnemyHealth.OnEnemyKilled.AddListener(() => ++totalKills);
      PlayerHealth.OnPlayerHealthChanged.AddListener((int hp, int _) => { if (hp <= 0) { ShowGameOver(); } });
      GameOverPanel.SetActive(false);
   }

   public void ShowGameOver()
   {
      GameOverPanel.SetActive(true);
      Time.timeScale = 0;
      KillsText.text = string.Format(KillsMessage, totalKills);
   }

   public void Continue()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene("MainMenu");
   }

}
