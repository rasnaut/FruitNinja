using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
  public GameObject WholeFruit = null;
  public GameObject SlicedFruit = null;
  public Rigidbody TopPartRigidbody;
  public Rigidbody BottomPartRigidbody;

  // Приватные переменные для Rigidbody (физических свойств) целого фрукта и коллайдера для разрезания
  private Rigidbody mainRigidbody;
  private Collider sliceTrigger;
  // Start is called before the first frame update
  void Start()
  {
    FillComponents();
  }

  private void FillComponents()
  {
    mainRigidbody = GetComponent<Rigidbody>(); // Получаем Rigidbody целого фрукта
    sliceTrigger = GetComponent<Collider>();  // Получаем коллайдер для разрезания
  }

  public void Slice(Vector3 direction, Vector3 position, float force)
  {
    // Вызываем метод, который меняет состояние фрукта с целого на разрезанное
    SetSliced();

    // Вызываем метод, который поворачивает половинки фрукта в заданном направлении
    RotateBySliceDirection(direction);

    // Вызываем метод, который добавляет силу броска к верхней и нижней половинкам разрезанного фрукта
    AddForce(TopPartRigidbody, direction, position, force);
    AddForce(BottomPartRigidbody, direction, position, force);
  }

  private void SetSliced()
  {
    
    WholeFruit.SetActive(false);   // Делаем целый фрукт неактивным
    SlicedFruit.SetActive(true);   // Делаем половинки фрукта активными
    sliceTrigger.enabled = false;  // 
  }

  private void RotateBySliceDirection(Vector3 direction)
  {
    // Вычисляем угол поворота из направления разрезания
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    // Применяем поворот к половинкам фрукта
    SlicedFruit.transform.rotation = Quaternion.Euler(0f, 0f, angle);
  }

  private void AddForce(Rigidbody sliceRigidbody, Vector3 direction, Vector3 position, float force)
  {
    // Копируем линейную и угловую скорость целого фрукта
    sliceRigidbody.velocity = mainRigidbody.velocity;
    sliceRigidbody.angularVelocity = mainRigidbody.angularVelocity;

    // Прикладываем силу в заданных направлении и позиции
    sliceRigidbody.AddForceAtPosition(direction * force, position);
  }
}
