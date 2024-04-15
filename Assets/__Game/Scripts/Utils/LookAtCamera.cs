using UnityEngine;

namespace Assets.__Game.Scripts.Utils
{
  public class LookAtCamera : MonoBehaviour
  {
    private Camera _camera;

    private void Awake()
    {
      _camera = Camera.main;
    }

    private void LateUpdate()
    {
      transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
        _camera.transform.rotation * Vector3.up);
    }
  }
}