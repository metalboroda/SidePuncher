using Assets.__Game.Scripts.PoolManager;
using Zenject;

namespace Assets.__Game.Scripts.Installers
{
  public class ServicesInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<ObjectPoolManagerDI>().AsSingle();
    }
  }
}