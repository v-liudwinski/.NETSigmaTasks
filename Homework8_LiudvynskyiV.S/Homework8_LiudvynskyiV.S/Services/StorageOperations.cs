namespace Homework8_LiudvynskyiV.S.Services;

public class StorageOperations
{
    public Action OrderOperation { get; set; }
    public event Action ExecuteOrdering;

    public virtual void OnExecuteOrdering()
    {
        ExecuteOrdering?.Invoke();
    }
}