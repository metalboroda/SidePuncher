using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Level
{
  [Serializable]
  public class Wave
  {
    public WaveEnemy[] WaveEnemies;

    [Space]
    public float SpawnRate;
  }

  [Serializable]
  public class WaveEnemy
  {
    public GameObject Enemy;

    [Space]
    public int amount;
  }
}