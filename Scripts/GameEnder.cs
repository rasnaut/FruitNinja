using UnityEngine;
using TMPro;

public class GameEnder : MonoBehaviour
{
  public Score           Score;            // Скрипт счётчика очков
  public Health          Health;           // Скрипт счётчика жизней
  public FruitSpawner    FruitSpawner;     // Скрипт подбрасывания фруктов и бомб
  public GameObject      GameScreen;       // Объект игрового экрана
  public GameObject      GameEndScreen;    // Объект экрана конца игры
  public TextMeshProUGUI GameEndScoreText; // Надпись с итоговым счётом
  public TextMeshProUGUI BestScoreText;    // Текстовое поле лучшего счёта

  void Start(){
    SwitchScreens(true); // При запуске игры показываем игровой экран
  }

  public void EndGame()
  {
    FruitSpawner.stopSpawn();               // Прекращаем появление фруктов и бомб
    SwitchScreens(false);                   // Переключаемся на экран конца игры
    RefreshScores();
  }

  public void RestartGame()
  {
    Score.Restart();        // Обнуляем счётчик очков
    Health.Restart();       // Восстанавливаем счётчик жизней
    FruitSpawner.Restart(); // Перезапускаем появление фруктов и бомб
    SwitchScreens(true);    // Возвращаемся на игровой экран
  }

  private void SwitchScreens(bool isGame)
  {
    GameScreen.SetActive(isGame);       // Включаем или выключаем игровой экран в зависимости от значения isGame
    GameEndScreen.SetActive(!isGame);   // Включаем или выключаем экран конца игры в зависимости от значения isGame
  }

  private void SetGameEndScoreText(int value) {
    GameEndScoreText.text = $"Your score {value} points!"; // Отображаем в GameEndScoreText.text итоговые очки
  }

  private void RefreshScores()
  {
    int score        = Score.GetScore();      // Получаем текущий и лучший счёт из скрипта Score
    int oldBestScore = Score.GetBestScore();

    bool isNewBestScore = CheckNewBestScore(score, oldBestScore); // Проверяем, верно ли, что текущий счёт — новый рекорд

    SetActiveGameEndScoreText(!isNewBestScore); // Решаем, объявлять ли новый рекорд, в зависимости от значения isNewBestScore

    // Если текущий счёт — новый рекорд
    if (isNewBestScore) {
      Score.SetBestScore(score);         // Сохраняем новый рекорд
      SetNewBestScoreText(score);        // Устанавливаем текстовое поле нового рекорда
    } else { 
      SetGameEndScoreText(score);        // Устанавливаем текстовое поле счёта в конце игры
      SetOldBestScoreText(oldBestScore); // Устанавливаем текстовое поле лучшего счёта
    }
  }

  // Возвращаем результат проверки того, что текущий счёт выше лучшего (true или false)
  private bool CheckNewBestScore(int score, int oldBestScore) { return score > oldBestScore; }

  // Обновляем надпись лучшего счёта
  private void SetOldBestScoreText(int value) { BestScoreText.text = $"Лучший результат: {value}"; }

  // Обновляем надпись нового рекорда
  private void SetNewBestScoreText(int value) { BestScoreText.text = $"Новый рекорд: {value}!"; }

  // Устанавливаем активность текстового поля счёта в конце игры в зависимости от значения value
  private void SetActiveGameEndScoreText(bool value) { GameEndScoreText.gameObject.SetActive(value); }
}
