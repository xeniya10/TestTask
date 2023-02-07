public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver();
    void NotifyObserver(ControlType type);
}
