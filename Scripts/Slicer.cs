
using UnityEngine;

public class Slicer : MonoBehaviour
{
  public float SliceForce = 65;                // Сила, с которой будут разрезаться фрукты

  private const float MinSlicingMove = 0.01f;  // Минимальное значение для проверки, двигался ли резак

  private Collider _slicerTrigger;             // Коллайдер, который фиксирует столкновение резака с фруктами

  // Основная камера в сцене
  private Camera _mainCamera;

  // Направление движения резака
  private Vector3 _direction;
  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  private void Init()
  {
    // Подключаем коллайдер резака
    _slicerTrigger = GetComponent<Collider>();

    // Подключаем главную камеру из класса Camera
    _mainCamera = Camera.main;

    // Выключаем режим нарезки
    SetSlicing(false);
  }

  // Update is called once per frame
  void Update()
  {
    Slicing();
  }

  private void Slicing()
  {
    // Если нажата левая кнопка мыши
    if (Input.GetMouseButton(0))
    {
      // Обновляем режим нарезки
      RefreshSlicing();
    }
    // Если отпущена левая кнопка мыши
    if (Input.GetMouseButtonUp(0))
    {
      // Выключаем режим нарезки
      SetSlicing(false);
    }
  }

  private void SetSlicing(bool value)
  {
    _slicerTrigger.enabled = value;  // Включаем или выключаем коллайдер в зависимости от value
  }

  private void RefreshSlicing()
  {
    // Получаем позицию, куда направлен курсор
    Vector3 targetPosition = GetTargetPosition();

    // Обновляем направление движения резака
    RefreshDirection(targetPosition);

    // Двигаем резак в сторону курсора
    MoveSlicer(targetPosition);

    // Проверяем, хватает ли сдвига для нарезки
    bool isSlicing = CheckMoreThenMinMove(_direction);

    // Делаем нарезку активной или неактивной
    SetSlicing(isSlicing);
  }

  private Vector3 GetTargetPosition()
  {
    Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition); // Преобразуем позицию курсора на экране в мировые координаты и сохраняем её в переменную targetPosition

    targetPosition.z = 0;  // Задаём значение Z-координаты цели равным 0, чтобы цель была на одной плоскости с резаком фруктов

    return targetPosition; // Возвращаем полученную позицию цели
  }

  // Обновляем направление движения курсора
  private void RefreshDirection(Vector3 targetPosition)
  {
    _direction = targetPosition - transform.position; // Вычисляем вектор направления, который указывает на цель (позицию курсора) от текущей позиции резака
  }

  // Сдвигаем резак в заданную позицию
  private void MoveSlicer(Vector3 targetPosition)
  {
    transform.position = targetPosition; // Делаем новую позицию резака равной позиции курсора
  }

  private bool CheckMoreThenMinMove(Vector3 direction)
  {
    float slicingSpeed = direction.magnitude / Time.deltaTime;  // Вычисляем скорость перемещения резака по направлению, которое указано вектором direction

    return slicingSpeed >= MinSlicingMove;  // Проверяем, превышает ли скорость перемещения заданный минимальный порог для разрезания
  }

  private void OnTriggerEnter(Collider other)
  {
    FruitBehaviour fruit = other.GetComponent<FruitBehaviour>();  // Создаём переменную для фрукта, которого мы коснулись

    // Проверяем, является ли объект фруктом
    // Здесь также можно написать if (!fruit)
    if (fruit == null) {
      return;// Если объект — не фрукт, выходим из метода
    }

    // Режем фрукт в заданном направлении с учётом позиции курсора и силы разрезания
    fruit.Slice(_direction, transform.position, SliceForce);
  }
}
