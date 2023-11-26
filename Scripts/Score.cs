using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
  // Переменная для текста, в котором будут отображаться очки
  private TextMeshProUGUI _scoreText;

  // Переменная для хранения текущего количества очков
  private int _score;
  // Start is called before the first frame update
  void Start()
  {
    FillComponents();
    SetScore(0);      // Вызываем метод SetScore() с начальным значением 0
  }
  
  private void FillComponents()
  {
    // Находим компонент TextMeshProUGUI у дочерних объектов того объекта, на котором висит скрипт (у нас это будет Score), и присваиваем значение компонента переменной _scoreText
    _scoreText = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void AddScore(int value) { SetScore(_score + value); } // Вызываем метод SetScore() с обновлённым значением очков

  private void SetScore(int value)
  {
    _score = value;       // Присваиваем переменной _score указанное значение

    SetScoreText(value);  // Вызываем метод SetScoreText() с этим значением
  }

  private void SetScoreText(int value)
  {
    _scoreText.text = "Score: " + value; // Отображаем в компоненте _scoreText.text текущие очки
  }
}
