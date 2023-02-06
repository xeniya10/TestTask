using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Player _player;
    [SerializeField] private RoadManager _road;
    [SerializeField] private CameraController _camera;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private PlayScreen _playScreen;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_player).AsSelf();
        builder.RegisterInstance(_road).AsSelf();
        builder.RegisterInstance(_camera).AsSelf();
        builder.RegisterInstance(_pauseScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_playScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterEntryPoint<GameController>(Lifetime.Scoped);
    }
}
