public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver();
    void NotifyObservers(ControlType type);
}
