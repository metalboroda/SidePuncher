using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public class CharacterAttackHandlerBase : MonoBehaviour
  {
    [Space]
    [SerializeField] private Collider leftHandCollider;
    [SerializeField] private Collider rightHandCollider;
    [SerializeField] private Collider leftFootCollider;
    [SerializeField] private Collider rightFootCollider;

    private void Start()
    {
      DisableAttackTriggers();
    }

    public void EnableAttackTriggers(int value)
    {
      switch (value)
      {
        case 0:
          leftHandCollider.enabled = true;
          break;
        case 1:
          rightHandCollider.enabled = true;
          break;
        case 2:
          leftFootCollider.enabled = true;
          break;
        case 3:
          rightFootCollider.enabled = true;
          break;
      }
    }

    public void DisableAttackTriggers()
    {
      leftHandCollider.enabled = false;
      rightHandCollider.enabled = false;
      leftFootCollider.enabled = false;
      rightFootCollider.enabled = false;
    }
  }
}