using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
  public int StartHealth = 3;

  private TextMeshProUGUI _healthText;
  private int _currentHealth;
  // Start is called before the first frame update
  void Start()
  {
    FillComponents();
    SetHealth(StartHealth);
  }

  private void FillComponents()
  {
    _healthText = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void RemoveHealth() { SetHealth(_currentHealth-1); }

  private void SetHealth(int value)
  {
    _currentHealth = value;
    SetHealthText(value);
  }

  public void SetHealthText(int value)
  {
    _healthText.text = "Health: " + value.ToString();
  }
}
