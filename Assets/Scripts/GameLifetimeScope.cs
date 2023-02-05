using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Player _player;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private PlayScreen _playScreen;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // builder.Register<StorageManager>(Lifetime.Scoped).As<IStorageManager>();
        builder.RegisterInstance(_player).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_pauseScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_playScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterEntryPoint<GameController>(Lifetime.Scoped);
    }
}
