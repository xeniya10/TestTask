using UnityEngine;
using VContainer.Unity;

public class GameController : IInitializable, IObserver
{
    private readonly Player _player;
    private readonly RoadManager _road;
    private readonly CameraController _cameraController;
    private readonly PauseScreen _pauseScreen;
    private readonly PlayScreen _playScreen;

    public GameController(Player player, RoadManager road, CameraController cameraController, PauseScreen pauseScreen, PlayScreen playScreen)
    {
        _player = player;
        _road = road;
        _cameraController = cameraController;
        _pauseScreen = pauseScreen;
        _playScreen = playScreen;
    }

    public void Initialize()
    {
        _cameraController.Inject(_player);
        _playScreen.SwipeEvent += _player.OnSwipe;
        
        _pauseScreen.AddObserver(this);
        _pauseScreen.CreateDropdown();
        _playScreen.SubscribeToButton(Pause);
        _pauseScreen.SubscribeToButton(Play);
    }

    private void Pause()
    {
        _pauseScreen.SetActive(true);
        _playScreen.SetActive(false);
        _player.SetActive(false);
        _road.SetActive(false);
        Time.timeScale = 0;
    }

    private void Play()
    {
        _pauseScreen.SetActive(false);
        _playScreen.SetActive(true);
        _player.SetActive(true);
        _road.SetActive(true);
        Time.timeScale = 1;
    }

    public void ChangeControl(ControlType type)
    {
        _player.SelectedType = type;
        
        if (type == ControlType.Swipe)
        {
            _playScreen.SwipeIsActive = true;
        }
        else
        {
            _playScreen.SwipeIsActive = false;
        }
    }
}
