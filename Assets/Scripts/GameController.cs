using UnityEngine;
using VContainer.Unity;

public enum ControlType { Swipe, Drag, Arrow }

public class GameController : IInitializable, IObserver
{
    private readonly Player _player;
    private readonly RoadManager _road;
    private readonly Timer _timer;
    private readonly CameraController _cameraController;
    private readonly PauseScreen _pauseScreen;
    private readonly PlayScreen _playScreen;

    public GameController(Player player, RoadManager road, Timer timer, CameraController cameraController, PauseScreen pauseScreen, PlayScreen playScreen)
    {
        _player = player;
        _road = road;
        _timer = timer;
        _cameraController = cameraController;
        _pauseScreen = pauseScreen;
        _playScreen = playScreen;
    }

    public void Initialize()
    {
        _cameraController.Inject(_player);
        _road.Inject(_timer);
        
        _playScreen.SwipeEvent += _player.OnSwipe;
        
        _pauseScreen.AddObserver(this);
        _pauseScreen.CreateDropdown();
        _playScreen.SubscribeToButton(Pause);
        _pauseScreen.SubscribeToButton(Play);
    }

    private void Pause()
    {
        SetActiveObject(false);
        Time.timeScale = 0;
    }

    private void Play()
    {
        SetActiveObject(true);
        Time.timeScale = 1;
    }

    private void SetActiveObject(bool isPlayed)
    {
        _pauseScreen.SetActive(!isPlayed);
        _playScreen.SetActive(isPlayed);
        _player.SetActive(isPlayed);
        _road.SetActive(isPlayed);
    }

    public void Update(ControlType type)
    {
        _player.SelectedType = type;
        _playScreen.SwipeIsActivated = type == ControlType.Swipe;
    }
}
