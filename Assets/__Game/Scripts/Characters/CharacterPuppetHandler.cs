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
    public GameObject RagdollObject;

    [Space]
    [SerializeField] private PuppetMaster puppetMaster;

    public void DisableRagdoll()
    {
      puppetMaster.state = PuppetMaster.State.Alive;
      puppetMaster.mode = PuppetMaster.Mode.Disabled;
    }

    public void EnableRagdoll()
    {
      float randRagdollDelay = Random.Range(minEnableDelay, maxEnableDelay);

      StartCoroutine(DoEnableRagdoll(randRagdollDelay));
      StartCoroutine(DoDisableRagdollObject());
    }

    private IEnumerator DoEnableRagdoll(float delay)
    {
      yield return new WaitForSeconds(delay);

      puppetMaster.state = PuppetMaster.State.Dead;
    }

    private IEnumerator DoDisableRagdollObject()
    {
      float disableDelay = 6f;

      yield return new WaitForSeconds(disableDelay);

      RagdollObject.SetActive(false);
    }
  }
}