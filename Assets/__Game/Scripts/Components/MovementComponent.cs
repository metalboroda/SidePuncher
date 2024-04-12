using UnityEngine;

namespace Assets.__Game.Scripts.Components
{
  public class MovementComponent
  {
    public void MoveForward(float movementSpeed, Transform transform)
    {
      Vector3 currentPosition = transform.position;
      Vector3 newPosition = currentPosition + transform.forward * movementSpeed * Time.deltaTime;

      transform.position = newPosition;
    }
  }
}