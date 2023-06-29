public interface IInteract
{
    /// <summary>
    /// Runs upon interacting with this object
    /// </summary>
    void OnInteract();
}

public interface IInteractRequire<T>
{
    /// <summary>
    /// Runs upon interacting with this object
    /// </summary>
    void OnInteract(T item);
}

