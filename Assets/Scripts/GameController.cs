using UnityEngine;
using VContainer.Unity;

public class GameController : IInitializable, IObserver
{
    private readonly Player _player;
    private readonly PauseScreen _pauseScreen;
    private readonly PlayScreen _playScreen;

    public GameController(Player player, PauseScreen pauseScreen, PlayScreen playScreen)
    {
        _player = player;
        _pauseScreen = pauseScreen;
        _playScreen = playScreen;
    }

    public void Initialize()
    {
        _pauseScreen.CreateDropdown();
        _pauseScreen.AddObserver(this);
        _playScreen.SubscribeToButton(Pause);
        _pauseScreen.SubscribeToButton(Play);
    }

    private void Pause()
    {
        _pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    private void Play()
    {
        _pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeControl(ControlType type)
    {
        throw new System.NotImplementedException();
    }
}
