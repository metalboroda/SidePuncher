using RootMotion.Dynamics;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public class CharacterPuppetHandler : MonoBehaviour
  {
    [SerializeField] private float minEnableDelay = 0.25f;
    [SerializeField] private float maxEnableDelay = 0.75f;

    [Space]
    [SerializeField] private PuppetMaster puppetMaster;

    public void DisableRagdoll()
    {
      puppetMaster.state = PuppetMaster.State.Alive;
      puppetMaster.mode = PuppetMaster.Mode.Disabled;
    }

    public void EnableRagdoll()
    {
      StartCoroutine(DoEnableRagdoll(Random.Range(minEnableDelay, maxEnableDelay)));
    }

    private IEnumerator DoEnableRagdoll(float delay)
    {
      yield return new WaitForSeconds(delay);

      puppetMaster.state = PuppetMaster.State.Dead;
    }
  }
}