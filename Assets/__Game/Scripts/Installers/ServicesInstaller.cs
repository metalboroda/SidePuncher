using Assets.__Game.Scripts.PoolManager;
using Assets.__Game.Scripts.Services;
using Zenject;

namespace Assets.__Game.Scripts.Installers
{
  public class ServicesInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<EventBus>().AsSingle();
      Container.Bind<ObjectPoolManager>().AsSingle();
    }
  }
}