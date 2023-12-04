
using UnityEngine;

public class Slicer : MonoBehaviour
{
  public float SliceForce = 65;                // Сила, с которой будут разрезаться фрукты
  public Score Score;                          // Скрипт счётчика очков
  public Health Health;                        // Скрипт счётчика жизней
  public FruitSpawner FruitSpawner;
  public GameEnder GameEnder;
  public SlicerCombo SlicerComboChecker;

  private const float MinSlicingMove = 0.01f;  // Минимальное значение для проверки, двигался ли резак
  private Collider _slicerTrigger;             // Коллайдер, который фиксирует столкновение резака с фруктами
  private Camera _mainCamera;                  // Основная камера в сцене
  private Vector3 _direction;                  // Направление движения резака

  void Start() { Init(); }

  private void Init()
  {
    _slicerTrigger = GetComponent<Collider>(); // Подключаем коллайдер резака
    _mainCamera    = Camera.main;              // Подключаем главную камеру из класса Camera

    SetSlicing(false);                          // Выключаем режим нарезки
  }

  // Update is called once per frame
  void Update() {
    Slicing();
  }

  private void Slicing()
  {
    // Если нажата левая кнопка мыши
    if (Input.GetMouseButton(0)) {
      RefreshSlicing(); // Обновляем режим нарезки
    }
    // Если отпущена левая кнопка мыши
    if (Input.GetMouseButtonUp(0)) {
      SetSlicing(false); // Выключаем режим нарезки
    }
  }

  private void SetSlicing(bool value) { _slicerTrigger.enabled = value; } // Включаем или выключаем коллайдер в зависимости от value

  private void RefreshSlicing()
  {
    Vector3 targetPosition = GetTargetPosition(); // Получаем позицию, куда направлен курсор

    RefreshDirection(targetPosition);   // Обновляем направление движения резака
    MoveSlicer      (targetPosition);   // Двигаем резак в сторону курсора

    bool isSlicing = CheckMoreThenMinMove(_direction);  // Проверяем, хватает ли сдвига для нарезки
    SetSlicing(isSlicing);                              // Делаем нарезку активной или неактивной
  }

  private Vector3 GetTargetPosition()
  {
    Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition); // Преобразуем позицию курсора на экране в мировые координаты и сохраняем её в переменную targetPosition

    targetPosition.z = 0;  // Задаём значение Z-координаты цели равным 0, чтобы цель была на одной плоскости с резаком фруктов

    return targetPosition; // Возвращаем полученную позицию цели
  }

  // Обновляем направление движения курсора
  private void RefreshDirection(Vector3 targetPosition) {
    _direction = targetPosition - transform.position; // Вычисляем вектор направления, который указывает на цель (позицию курсора) от текущей позиции резака
  }

  // Сдвигаем резак в заданную позицию
  private void MoveSlicer(Vector3 targetPosition) {
    transform.position = targetPosition; // Делаем новую позицию резака равной позиции курсора
  }

  private bool CheckMoreThenMinMove(Vector3 direction)
  {
    float slicingSpeed = direction.magnitude / Time.deltaTime;  // Вычисляем скорость перемещения резака по направлению, которое указано вектором direction

    return slicingSpeed >= MinSlicingMove;  // Проверяем, превышает ли скорость перемещения заданный минимальный порог для разрезания
  }

  // Метод OnTriggerEnter() вызывается, когда объект с коллайдером пересекает другой коллайдер
  private void OnTriggerEnter(Collider other)
  {
    CheckFriut(other);   // Проверяем, является ли объект фруктом
    CheckBomb(other);    // Проверяем, является ли объект бомбой
  }

  // Метод для проверки, является ли объект фруктом
  private void CheckFriut(Collider other)
  {
    FruitBehaviour fruit = other.GetComponent<FruitBehaviour>();  // Создаём переменную для фрукта, которого мы коснулись

    // Проверяем, является ли объект фруктом
    // Здесь также можно написать if (!fruit)
    if (fruit == null) { return; } // Если объект — не фрукт, выходим из метода

    fruit.Slice(_direction, transform.position, SliceForce);  // Режем фрукт в заданном направлении с учётом позиции курсора и силы разрезания
   
    // прибавляем при разрезании фруктов не одно очко, а столько, сколько фруктов нам удалось разрезать одновременно
    SlicerComboChecker.IncreaseComboStep();
    int scoreByFruit = 1 * SlicerComboChecker.GetComboMultiplier();
    Score.AddScore(scoreByFruit);
  }

  // Метод для проверки, является ли объект бомбой
  private void CheckBomb(Collider other)
  {
    Bomb bomb = other.GetComponent<Bomb>();  // Создаём переменную для бомбы, которой мы коснулись

    // Проверяем, является ли объект бомбой
    // Здесь также можно написать if (!bomb)
    if (bomb == null) { return; }  // Если объект — не бомба, выходим из метода
    
    Destroy(bomb.gameObject);   // Уничтожаем игровой объект бомбы
    Health.RemoveHealth();      // Теряем одну жизнь
    CheckHealthEnd(Health.GetCurrentHealth());

    SlicerComboChecker.StopCombo();//  сбрасываем комбо при разрезании бомбы
  }

  // Метод для проверки количества жизней
  private void CheckHealthEnd(int health)
  {
    if (health > 0) { return; }

    StopGame();  // Иначе вызываем метод StopGame()
  }

  // Метод для остановки игры
  private void StopGame() {
    GameEnder.EndGame(); // Вызываем метод EndGame() из скрипта GameEnder
  }
}
