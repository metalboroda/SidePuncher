namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyAudioHandler : CharacterAudioHandler
  {
    private EnemyController _enemyController;

    protected override void Awake()
    {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
      _enemyController.EnemyHandler.EnemyDead += PlayRandomDeathSound;
      _enemyController.EnemyAttackHandler.AttackTriggered += PlayRandomAttackSound;
    }

    private void OnDisable()
    {
      _enemyController.EnemyHandler.EnemyDead -= PlayRandomDeathSound;
      _enemyController.EnemyAttackHandler.AttackTriggered -= PlayRandomAttackSound;
    }

    public override void PlayRandomAttackSound()
    {
      base.PlayRandomAttackSound();
    }

    public override void PlayRandomDamageSound()
    {
      base.PlayRandomDamageSound();
    }

    public override void PlayRandomDeathSound()
    {
      base.PlayRandomDeathSound();
    }
  }
}