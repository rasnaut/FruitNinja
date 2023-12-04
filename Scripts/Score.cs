using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

  private const string BestScoreKey = "BestScore"; // Константа с ключом лучшего счёта — так игра сможет быстрее находить его
  private TextMeshProUGUI _scoreText;              // Переменная для текста, в котором будут отображаться очки
  
  private int _score;                              // Переменная для хранения текущего количества очков
  private int _bestScore;                          // Переменная для хранения лучшего счёта
  private bool _isNewBestScore;                    // Переменная для проверки, что текущий счёт — новый рекорд

  void Start()
  {
    FillComponents();
    SetScore(0);      // Вызываем метод SetScore() с начальным значением 0
    LoadBestScore();
  }

  // Находим компонент TextMeshProUGUI у дочерних объектов того объекта, на котором висит скрипт (у нас это будет Score), и присваиваем значение компонента переменной _scoreText
  private void FillComponents() {
    _scoreText = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void AddScore(int value) { SetScore(_score + value); } // Вызываем метод SetScore() с обновлённым значением очков

  private void SetScore(int value)
  {
    _score = value;       // Присваиваем переменной _score указанное значение

    SetScoreText(value);  // Вызываем метод SetScoreText() с этим значением
  }

  private void SetScoreText(int value) { _scoreText.text = "Score: " + value; } // Отображаем в компоненте _scoreText.text текущие очки

  public int  GetScore    () { return _score;     }  // Получаем итоговый счёт
  public int  GetBestScore() { return _bestScore; }  // Получаем значение лучшего счёта
  public void Restart     () { SetScore(0);       }  // Обнуляем счётчик очков

  public void SetBestScore(int value) {
    _bestScore = value;     // Присваиваем переменной _bestScore значение value
    SaveBestScore(value);   // Вызываем метод для сохранения лучшего счёта
  }

  // Получаем значение лучшего счёта из памяти с помощью метода PlayerPrefs.GetInt() с ключом BestScoreKey
  private void LoadBestScore() {
    _bestScore = PlayerPrefs.GetInt(BestScoreKey);
  }

  // Сохраняем лучший счёт с помощью метода PlayerPrefs.SetInt() с ключом BestScoreKey и значением value
  private void SaveBestScore(int value) {
    PlayerPrefs.SetInt(BestScoreKey, value);
  }
}
