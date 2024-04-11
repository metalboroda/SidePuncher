using Assets.__Game.Scripts.Services;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAttackHandler : MonoBehaviour
  {
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float allowAttackTime = 0.15f;

    private bool _canAttack = true;

    private InputService _inputService;
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
      _eventBus = eventBus;
    }

    private void Awake()
    {
      _inputService = new InputService();
    }

    private void OnEnable()
    {
      _inputService.LeftAttackTriggered += LeftAttack;
      _inputService.RightAttackTriggered += RightAttack;
    }

    private void OnDisable()
    {
      _inputService.LeftAttackTriggered -= LeftAttack;
      _inputService.RightAttackTriggered -= RightAttack;
    }

    private void OnDestroy()
    {
      _inputService.Dispose();
    }

    private void LeftAttack()
    {
      if (_canAttack == false) return;

      StartCoroutine(DoSmoothRotateY(-90));
      OnAttack();
    }

    private void RightAttack()
    {
      if (_canAttack == false) return;

      StartCoroutine(DoSmoothRotateY(90));
      OnAttack();
    }

    private void OnAttack()
    {
      _eventBus.RaiseAttackTriggered();
      _canAttack = false;

      StartCoroutine(DoAllowAttack());
    }

    private IEnumerator DoSmoothRotateY(float y)
    {
      Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);
      float elapsedTime = 0f;

      while (elapsedTime < rotationSpeed)
      {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / rotationSpeed);
        elapsedTime += Time.deltaTime;

        yield return null;
      }

      transform.rotation = targetRotation;
    }

    private IEnumerator DoAllowAttack()
    {
      yield return new WaitForSeconds(allowAttackTime);

      _canAttack = true;
    }
  }
}