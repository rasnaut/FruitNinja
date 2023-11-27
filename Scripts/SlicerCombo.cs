using UnityEngine;
using TMPro;

public class SlicerCombo : MonoBehaviour
{
  // Объект, который отображает множитель комбо (например, «x3»)
  public GameObject ComboMultiplierRootGO;

  // Временной интервал между разрезаниями фруктов, за который надо разрезать следующий фрукт, чтобы комбо набиралось
  public float ComboIncreaseInterval = 1.1f;

  // Число шагов, через которое будет увеличиваться множитель комбо
  public int ComboMultiplierIncreseStep = 3;

  // Текстовый объект для отображения множителя комбо
  private TextMeshProUGUI _comboMultiplierText;

  // Таймер для отслеживания времени, которое прошло с тех пор, когда множитель комбо увеличился в последний раз
  private float _comboTimer;

  // Шаг комбо (количество фруктов, разрезанных подряд)
  private int _comboStep;

  // Множитель комбо
  private int _comboMultiplier;
  // Start is called before the first frame update
  private void Start()
  {
    // Заполняем ссылки на компоненты
    FillComponents();

    // Сбрасываем таймер комбо
    DropComboTimer();

    // Рассчитываем множитель комбо для начального шага
    CalculateComboMultiplier(0);
  }

  private void FillComponents()
  {
    // Получаем текстовый объект для отображения множителя комбо
    _comboMultiplierText = ComboMultiplierRootGO.GetComponentInChildren<TextMeshProUGUI>();
  }

  // Update is called once per frame
  private void Update()
  {
    // Обновляем таймер комбо
    MoveTimer();
  }

  private void MoveTimer()
  {
    // Увеличиваем таймер комбо на время Time.deltaTime (в секундах), которое прошло с последнего кадра игры
    _comboTimer += Time.deltaTime;

    // Если таймер комбо превысил заданный интервал
    if (_comboTimer >= ComboIncreaseInterval)
    {
      // Останавливаем комбо
      StopCombo();
    }
  }

  // Увеличиваем шаг комбо
  public void IncreaseComboStep()
  {
    // Вызываем метод SetComboStep() со значением _comboStep + 1
    SetComboStep(_comboStep + 1);
  }

  // Получаем множитель комбо
  public int GetComboMultiplier()
  {
    // Возвращаем значение _comboMultiplier
    return _comboMultiplier;
  }

  // Останавливаем комбо
  public void StopCombo()
  {
    // Вызываем метод SetComboStep() со значением 0
    SetComboStep(0);
  }

  private void SetComboStep(int value)
  {
    // Делаем значение шага комбо равным value
    _comboStep = value;

    // Рассчитываем новый множитель комбо
    CalculateComboMultiplier(value);

    // Сбрасываем таймер комбо
    DropComboTimer();
  }

  private void DropComboTimer()
  {
    // Обнуляем значение таймера
    _comboTimer = 0;
  }

  private void CalculateComboMultiplier(int comboStep)
  {
    // Рассчитываем множитель комбо по формуле: 1 + шаг комбо / число шагов, через которое будет увеличиваться множитель комбо
    _comboMultiplier = 1 + comboStep / ComboMultiplierIncreseStep;

    // Устанавливаем надпись множителя комбо
    SetComboMultiplierText(_comboMultiplier);

    // Отображаем объект со множителем комбо
    SetComboMultiplierShow(_comboMultiplier);
  }

  private void SetComboMultiplierText(int value)
  {
    // Устанавливаем текст вида "xМножитель"
    _comboMultiplierText.text = $"x{value}";
  }

  private void SetComboMultiplierShow(int value)
  {
    // Определяем, нужно ли отображать объект со множителем комбо (логическая переменная needShow станет равна true, если мы разрезали два и более фрукта)
    bool needShow = value > 1;

    // Активируем или деактивируем объект со множителем комбо в зависимости от значения переменной needShow
    ComboMultiplierRootGO.SetActive(needShow);
  }
}
