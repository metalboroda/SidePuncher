using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Level
{
  [Serializable]
  public class Wave
  {
    public GameObject regularEnemy;
    public GameObject toughEnemy;

    [Space]
    public GameObject[] spawnPoints;

    [Header("Param's")]
    public float spawnRate;
    public int regularEnemyPerWave;
    public int toughEnemyPerWave;
  }
}