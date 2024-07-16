using System.Collections.Generic;
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

    protected List<Collider> _colliders = new List<Collider>();

    private void Awake() {
      AddCollidersToList();
    }

    private void Start() {
      DisableAttackTriggers();
    }

    private void AddCollidersToList() {
      _colliders.Add(leftHandCollider);
      _colliders.Add(rightHandCollider);
      _colliders.Add(leftFootCollider);
      _colliders.Add(rightFootCollider);
    }

    // Left it. Maybe needs for code through colliders activasion
    public void EnableAttackTriggers(int value) {
      DisableAttackTriggers();

      switch (value) {
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

    public void DisableAttackTriggers() {
      foreach (Collider collider in _colliders) {
        collider.enabled = false;
      }
    }
  }
}