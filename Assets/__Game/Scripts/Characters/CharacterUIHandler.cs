using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Scripts.Characters
{
  public class CharacterUIHandler : MonoBehaviour
  {
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private Image healthFill;

    private CharacterHandlerBase _characterHandlerBase;

    private void Awake()
    {
      _characterHandlerBase = GetComponent<CharacterHandlerBase>();
    }

    private void OnEnable()
    {
      _characterHandlerBase.HealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
      _characterHandlerBase.HealthChanged -= DisplayHealth;
    }

    private void DisplayHealth(int value)
    {
      float fillAmount = (float)value / _characterHandlerBase.MaxHealth;

      healthFill.fillAmount = fillAmount;

      if (healthFill.fillAmount <= 0)
        healthBarObject.SetActive(false);
    }

    public void ResetParams()
    {
      healthFill.fillAmount = 1f;
      healthBarObject.SetActive(true);
    }
  }
}