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
  
  // Start is called before the first frame update
  void Start(){
    SwitchScreens(true); // При запуске игры показываем игровой экран
  }

  public void EndGame()
  {
    FruitSpawner.stopSpawn();               // Прекращаем появление фруктов и бомб
    SetGameEndScoreText(Score.GetScore());  // Получаем и устанавливаем итоговый счёт
    SwitchScreens(false);                   // Переключаемся на экран конца игры
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
}
