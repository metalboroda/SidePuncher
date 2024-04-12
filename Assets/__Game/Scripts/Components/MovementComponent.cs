using UnityEngine;

namespace Assets.__Game.Scripts.Components
{
  public class MovementComponent
  {
    public void MoveForward(bool canMove, float movementSpeed, Transform transform)
    {
      if (canMove == false) return;

      Vector3 currentPosition = transform.position;
      Vector3 newPosition = currentPosition + transform.forward * movementSpeed * Time.deltaTime;

      transform.position = newPosition;
    }
  }
}