using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
  public GameObject applePrefab;
  public GameObject melonePrefab;
  public GameObject watermelonePrefab;
  public GameObject orangePrefab;

  public GameObject bombPrefab;

  public float MinDelay = 0.2f;
  public float MaxDelay = 0.9f;
  public float AngleRangeZ = 20;
  public float LifeTime = 7f;
  public float MinForce = 15f;
  public float MaxForce = 25f;
  public float BombChance = 0.1f;

  private float spawnTimer = 0;
  private Collider spawnZone = null;
  // Start is called before the first frame update
  void Start()
  {
    SetNewSpawnTimer();
    FillComponents();
  }

  private void FillComponents()
  {
    spawnZone = GetComponent<Collider>();
  }

  // Update is called once per frame
  void Update()
  {
    SpawnTimerTick();
  }

  private void SetNewSpawnTimer()
  {
    spawnTimer = Random.Range(MinDelay, MaxDelay);
  }

  private void SpawnTimerTick()
  {
    // Уменьшаем текущую задержку на время, которое прошло с последнего кадра
    spawnTimer -= Time.deltaTime;

    // Если текущая задержка достигла нуля
    if (spawnTimer < 0)
    {
      // Генерируем случайное значение между 0 и 1
      float chance = Random.value;

      if(chance < BombChance) { SpawnBomb(); }
      else                    { SpawnFruit(); } // Подбрасываем новый фрукт

      // Устанавливаем новую задержку
      SetNewSpawnTimer();
    }
  }

  private void SpawnFruit()
  {
    SpawnObject(GetRandomFruitPrefab());
  }

  private void SpawnBomb()
  {
    SpawnObject(bombPrefab);
  }

  private void SpawnObject(GameObject spawnPrefab)
  {
    // Генерируем случайное значение для угла вращения по оси Z
    Quaternion startRotation = Quaternion.Euler(0f, 0f, Random.Range(-AngleRangeZ, AngleRangeZ));
    // Создаём новый фрукт на текущей позиции с заданным начальным положением
    GameObject newFruit = Instantiate(spawnPrefab, GetRandomSpawnPosition(), startRotation);
    // Удаляем фрукт через указанное время
    Destroy(newFruit, LifeTime);
    // Добавляем силу броска
    AddForce(newFruit);
  }

  private GameObject GetRandomFruitPrefab()
  {
    GameObject result = orangePrefab;
    // Генерируем случайное число в пределах от 0 до 3
    int fruit_index = Random.Range(0, 4);
    switch (fruit_index)
    {
      case 0: result = applePrefab; break;
      case 1: result = melonePrefab; break;
      case 2: result = watermelonePrefab; break;
    }

    return result;
  }

  private void AddForce(GameObject fruit)
  {
    // Генерируем случайное значение силы в пределах указанных значений
    float force = Random.Range(MinForce, MaxForce);
    // Бросаем фрукт в направлении вверх с указанной силой
    fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
  }

  private Vector3 GetRandomSpawnPosition()
  {
    Vector3 pos;
    pos.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
    pos.y = spawnZone.bounds.min.y;
    pos.z = spawnZone.bounds.min.z;
    return pos;
  }
}
