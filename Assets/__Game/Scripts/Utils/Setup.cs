using UnityEngine;

namespace Assets.__Game.Scripts.Utils
{
  public class Setup : MonoBehaviour
  {
    void Awake()
    {
      QualitySettings.vSyncCount = 1;
      Application.targetFrameRate = 120;
    }
  }
}