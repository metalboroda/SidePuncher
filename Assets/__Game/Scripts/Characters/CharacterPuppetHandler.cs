using RootMotion.Dynamics;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public class CharacterPuppetHandler : MonoBehaviour
  {
    [SerializeField] private PuppetMaster puppetMaster;

    public void EnableRagdoll(float delay = 0)
    {
      StartCoroutine(DoEnableRagdoll(delay));
    }

    private IEnumerator DoEnableRagdoll(float delay)
    {
      yield return new WaitForSeconds(delay);

      puppetMaster.state = PuppetMaster.State.Dead;
    }
  }
}